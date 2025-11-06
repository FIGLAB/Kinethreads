using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tennis : MonoBehaviour
{
    public comms_velo scomms;
    public float maxCurr = 1.3f;
    int count = 0;
    bool bounce = false;
    public int numbounces = 40;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (bounce){
            count++;
            if (count > numbounces){
                scomms.noslack(1);
                bounce = false;
            }
        }
    }

    void OnCollisionEnter(Collision collision){
        if (collision.gameObject.tag == "tennisball"){ 
            scomms.sendMessage("T " + maxCurr.ToString(), 1);
            count = 0;
            bounce = true;
        }
    }
    void OnCollisionExit(Collision collision){
        // if (collision.gameObject.tag == "tennisball"){
        //     scomms.noslack(1);
        // }
    }

}
