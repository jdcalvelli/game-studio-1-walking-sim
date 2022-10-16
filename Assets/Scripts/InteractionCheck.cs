using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using Unity.VisualScripting;
using UnityEditor.UI;

public class InteractionCheck : MonoBehaviour
{
    
    [SerializeField] private string fungusMessage;
    private GameManager _gameManagerReference;

    private void Start()
    {
        _gameManagerReference = FindObjectOfType<GameManager>();

    }

    // bubble up trigger event to game manager for purpose of sending to fungus
    private void OnTriggerEnter(Collider other)
    { 
        StartCoroutine(_gameManagerReference.sendFungusMessageOnKeyPress(fungusMessage));
    }

}
