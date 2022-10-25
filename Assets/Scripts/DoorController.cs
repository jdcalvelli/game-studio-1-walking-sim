using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    public Animator DoorAnimator;

    // Start is called before the first frame update
    void Start()
    {
        //DoorAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player") 
        {
            DoorAnimator.SetBool("isEntering", true);
            DoorAnimator.SetBool("isExiting", false);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            DoorAnimator.SetBool("isExiting", true);
            DoorAnimator.SetBool("isEntering", false);
        }       
    }
}
