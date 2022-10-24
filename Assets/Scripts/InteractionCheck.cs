using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class InteractionCheck : MonoBehaviour
{
    
    [SerializeField] private string fungusMessage;
    [SerializeField] private AudioSource audioSource;
    private GameManager _gameManagerReference;

    private void Start()
    {
        _gameManagerReference = FindObjectOfType<GameManager>();

    }

    // bubble up trigger event to game manager for purpose of sending to fungus
    private void OnTriggerEnter(Collider other)
    {
        _gameManagerReference.reticleAnim.Play();
        StartCoroutine(_gameManagerReference.HandleInteraction(fungusMessage, audioSource));
    }

    // stop coroutines on trigger exit to prevent talking to people without being near them
    private void OnTriggerExit(Collider other)
    {
        _gameManagerReference.reticleAnim.Rewind();
        StopAllCoroutines();
    }
}
