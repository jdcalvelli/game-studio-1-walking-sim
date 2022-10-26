using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Fungus;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Flowchart fungusFlowchart;

    [SerializeField] private AudioManager audioManagerReference;
    [SerializeField] private Animator uiAnimator;

    [SerializeField] private FirstPersonDrifter firstPersonDrifter;
    [SerializeField] private MouseLook mouseLookX;
    [SerializeField] private MouseLook mouseLookY;

    [SerializeField] private UILineRenderer reticle;
    public Sequence reticleAnim;

    private bool _areHeadphonesOn = true;

    private void Start()
    {
        reticleAnim = DOTween.Sequence()
            .Append(DOTween
                .To(() => reticle.Points[1].y,
                    x => reticle.Points[1].y = x,
                    -30,
                    0.25f)
            )
            .Join(DOTween
                .To(() => reticle.Points[2].y,
                    x => reticle.Points[2].y = x,
                    30,
                    0.25f)
            )
            .Append(DOTween
                .To(() => reticle.Points[1].y,
                    x => reticle.Points[1].y = x,
                    0,
                    0.25f)
            )
            .Join(DOTween
                .To(() => reticle.Points[2].y,
                    x => reticle.Points[2].y = x,
                    0,
                    0.25f)
            )
            .Append(DOTween
                .To(() => reticle.Points[1].y,
                    x => reticle.Points[1].y = x,
                    30,
                    0.25f)
            )
            .Join(DOTween
                .To(() => reticle.Points[2].y,
                    x => reticle.Points[2].y = x,
                    -30,
                    0.25f)
            )
            .Append(DOTween
                .To(() => reticle.Points[1].y,
                    x => reticle.Points[1].y = x,
                    0,
                    0.25f)
            )
            .Join(DOTween
                .To(() => reticle.Points[2].y,
                    x => reticle.Points[2].y = x,
                    0,
                    0.25f)
            )
            .SetLoops(-1, LoopType.Restart)
            .Pause();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(TriggerUIAnim());
        }
        // this is for uilinerenderer
        reticle.SetVerticesDirty();
    }

    // avoiding usage of update for performance enhancement
    public IEnumerator HandleInteraction(string fungusMessage, AudioSource audioSource)
    {
        //once we know trigger has happened, wait for e keypress
        while (!Input.GetKeyDown(KeyCode.E))
        {
            yield return null;
        }
        fungusFlowchart.SendFungusMessage(fungusMessage);
        audioManagerReference.TweenVolume(audioSource);
    }
    
    // helper functions
    private IEnumerator TriggerUIAnim()
    {
        if (_areHeadphonesOn == true)
        {
            mouseLookX.enabled = true;
            mouseLookY.enabled = true;
            firstPersonDrifter.enabled = true;
            
            uiAnimator.SetTrigger("headphone");

            yield return new WaitForSeconds(1f);
            
            audioManagerReference.TweenMixerGroupVolume("GameSoundsVol", 0f, 3f);
            audioManagerReference.TweenMixerGroupVolume("ElevatorMusicVol", -80f, 3f);

            _areHeadphonesOn = false;
        }
        else if (_areHeadphonesOn == false)
        {
            mouseLookX.enabled = false;
            mouseLookY.enabled = false;
            firstPersonDrifter.enabled = false;
            
            // trigger reverse animation
            uiAnimator.SetTrigger("revHeadphone");
            
            yield return new WaitForSeconds(0.5f);

            audioManagerReference.TweenMixerGroupVolume("GameSoundsVol", -80f, 3f);
            audioManagerReference.TweenMixerGroupVolume("ElevatorMusicVol", 0f, 3f);
            
            _areHeadphonesOn = true;
        }
    }
}
