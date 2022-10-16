using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEditor.UI;

public class FungusInteractTest : MonoBehaviour
{
    public Flowchart fungusFlowchart;
    
    public GameObject playerRef;

    public string fungusMessage;

    private bool _playerIsNear;
    
    // Update is called once per frame - this will have to change for performance w game manager
    private void OnTriggerEnter(Collider other)
    {
        if (other == playerRef.GetComponent<Collider>())
        {
            _playerIsNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == playerRef.GetComponent<Collider>())
        {
            _playerIsNear = false;
        }
    }

    private void Update()
    {
        if (_playerIsNear)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                fungusFlowchart.SendFungusMessage(fungusMessage);
            }
        }
    }
}
