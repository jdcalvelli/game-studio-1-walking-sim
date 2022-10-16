Shader "Unlit/SimpleWater"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

		_NoiseStrength("Noise Strength", float) = 1
		_NoiseSize("Noise Size", float) = 1
		_NoiseSpeed("Noise Speed", float) = 1
		_MainCol("Main Color", color) = (1,1,1,1)
    }

    SubShader
    {
		Tags {"RenderType" = "Transparent" "Queue" = "Transparent"}

        Pass
        {
			ZWrite Off
			
				Blend SrcAlpha OneMinusSrcAlpha
				Tags {"LightMode" = "ForwardBase"}
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"
				#include "Lighting.cginc"
			// compile shader into multiple variants, with and without shadows
			// (we don't care about any lightmaps yet, so skip these variants)
			#pragma multi_compile_fwdbase
			// shadow helper functions and macros
			#include "AutoLight.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _NoiseSize;
			float _NoiseStrength;
			float _NoiseSpeed;
			float4 _NoiseDirection;
			float4 _MainCol;

			struct v2f
			{
				float2 uv : TEXCOORD0;
				SHADOW_COORDS(1) // put shadows data into TEXCOORD1
					fixed3 diff : COLOR0;
				fixed3 ambient : COLOR1;
				float4 pos : SV_POSITION;
				float3 worldNormal : NORMAL;
				float3 viewDir : TEXCOORD1;
			};


			inline float unity_noise_randomValue(float2 uv)
			{
				return frac(sin(dot(uv, float2(12.9898, 78.233)))*43758.5453);
			}

			inline float unity_noise_interpolate(float a, float b, float t)
			{
				return (1.0 - t)*a + (t*b);
			}

			inline float unity_valueNoise(float2 uv)
			{
				float2 i = floor(uv);
				float2 f = frac(uv);
				f = f * f * (3.0 - 2.0 * f);

				uv = abs(frac(uv) - 0.5);
				float2 c0 = i + float2(0.0, 0.0);
				float2 c1 = i + float2(1.0, 0.0);
				float2 c2 = i + float2(0.0, 1.0);
				float2 c3 = i + float2(1.0, 1.0);
				float r0 = unity_noise_randomValue(c0);
				float r1 = unity_noise_randomValue(c1);
				float r2 = unity_noise_randomValue(c2);
				float r3 = unity_noise_randomValue(c3);

				float bottomOfGrid = unity_noise_interpolate(r0, r1, f.x);
				float topOfGrid = unity_noise_interpolate(r2, r3, f.x);
				float t = unity_noise_interpolate(bottomOfGrid, topOfGrid, f.y);
				return t;
			}

			float Unity_SimpleNoise_float(float2 UV, float Scale)
			{
				float t = 0.0;

				float freq = pow(2.0, float(0));
				float amp = pow(0.5, float(3 - 0));
				t += unity_valueNoise(float2(UV.x*Scale / freq, UV.y*Scale / freq))*amp;

				freq = pow(2.0, float(1));
				amp = pow(0.5, float(3 - 1));
				t += unity_valueNoise(float2(UV.x*Scale / freq, UV.y*Scale / freq))*amp;

				freq = pow(2.0, float(2));
				amp = pow(0.5, float(3 - 2));
				t += unity_valueNoise(float2(UV.x*Scale / freq, UV.y*Scale / freq))*amp;

				return t;
			}

			v2f vert(appdata_base v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord;
				float3 worldNormal = UnityObjectToWorldNormal(v.normal);
				half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
				o.diff = nl * _LightColor0.rgb;
				o.ambient = ShadeSH9(half4(worldNormal, 1));

					o.worldNormal = worldNormal;
				o.viewDir = WorldSpaceViewDir(v.vertex);

				o.pos.xyz += worldNormal * Unity_SimpleNoise_float(o.uv + (_Time.x * _NoiseSpeed), _NoiseSize) * _NoiseStrength;
				// compute shadows data
				TRANSFER_SHADOW(o)
				return o;
			}

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv) * _MainCol;

				//Do some lighting stuff    
				fixed shadow = SHADOW_ATTENUATION(i);
				fixed3 lighting = i.diff * shadow + i.ambient;
				col.rgb *= lighting;

                return col;
            }
            ENDCG
        }
    }
}
