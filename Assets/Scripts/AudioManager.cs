using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    //grab references to audio sources
    //on start, set all of them to play with volume 0
    //fade volume in over time function?
    //maybe we do just do the audio volume changes in fungus?
    public List<AudioSource> audioSources;

    private void Start()
    {
        foreach (var variableAudioSource in audioSources)
        {
            variableAudioSource.PlayDelayed(1);
        }
    }

    public void TweenVolume(AudioSource audioSource)
    {
        audioSource.DOFade(0.8f, 1f);
    }
}
