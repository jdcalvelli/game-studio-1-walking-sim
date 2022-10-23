using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Flowchart fungusFlowchart;

    [SerializeField] private AudioManager audioManagerReference;
    [SerializeField] private Animator uiAnimator;
    
    private bool _areHeadphonesOn = true;
    
    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TriggerUIAnim();
        }
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
    private void TriggerUIAnim()
    {
        if (_areHeadphonesOn == true)
        {
            uiAnimator.SetTrigger("headphone");
            _areHeadphonesOn = false;
        }
        else if (_areHeadphonesOn == false)
        {
            // trigger reverse animation
            uiAnimator.SetTrigger("revHeadphone");
            //TriggerUIAnim("reverse-headphone");
            _areHeadphonesOn = true;
        }
    }

}
