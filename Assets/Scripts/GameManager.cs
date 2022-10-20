using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Flowchart fungusFlowchart;

    [SerializeField] private AudioManager audioManagerReference;
    [SerializeField] private Animator UIAnimator;
    
    private void Start()
    {
        // testing anim on start
        //_UIReference.GetComponent<Animator>().Play("headphoneAnim");
    }

    private void Update()
    {
        TriggerUIAnim("headphone");
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
    private void TriggerUIAnim(string animationState)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            UIAnimator.SetTrigger(animationState);
        }
    }

}
