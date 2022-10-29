using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    //grab references to audio sources
    //on start, set all of them to play with volume 0
    //fade volume in over time function?
    //maybe we do just do the audio volume changes in fungus?
    [SerializeField] private List<AudioSource> audioSources;
    [SerializeField] private AudioSource trainHatSound;
    [SerializeField] private AudioMixer audioMixer;

        private void Start()
    {
        foreach (var variableAudioSource in audioSources)
        {
            variableAudioSource.PlayDelayed(1);
        }
        
        trainHatSound.PlayDelayed(1);
        
    }

    public void TweenVolume(AudioSource audioSource)
    {
        audioSource.DOFade(0.8f, 1f);
    }

    public void TweenMixerGroupVolume(string mixerGroup, float endValue, float duration)
    {
        audioMixer.DOSetFloat(mixerGroup, endValue, duration);
    }
}
