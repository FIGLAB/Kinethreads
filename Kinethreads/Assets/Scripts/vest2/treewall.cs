using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treewall : MonoBehaviour
{
    public comms_velo scomms;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("right_hand"))
        {
            scomms.sendMessage("T 1.0", 5);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("right_hand"))
        {
            scomms.off(5);
        }
    }
}
