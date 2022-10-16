using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Flowchart fungusFlowchart;

    private AudioManager _audioManagerReference;

    private void Start()
    {
        _audioManagerReference = FindObjectOfType<AudioManager>();
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
        _audioManagerReference.TweenVolume(audioSource);
    }
}
