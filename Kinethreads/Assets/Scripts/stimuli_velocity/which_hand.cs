using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class which_hand : MonoBehaviour
{
    public int handIdx = 0; // 0 is left, 1 is right
    public bool isGrabbed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter (Collider other){
        if (other.tag == "left_hand"){ 
            handIdx = 0;
            isGrabbed = true;
        } else if (other.tag == "right_hand"){ 
            handIdx = 1;
            isGrabbed = true;
        }
    } 
    void OnTriggerExit(Collider other){
        if (other.tag == "left_hand" || other.tag == "right_hand"){ 
            isGrabbed = false;
        }
    }

}
