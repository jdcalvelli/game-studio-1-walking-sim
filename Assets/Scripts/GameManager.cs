using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Flowchart fungusFlowchart;

    // avoiding usage of update for performance enhancement
    public IEnumerator sendFungusMessageOnKeyPress(string fungusMessage)
    {
        while (!Input.GetKeyDown(KeyCode.E))
        {
            yield return null;
        }
        fungusFlowchart.SendFungusMessage(fungusMessage);
    }
}
