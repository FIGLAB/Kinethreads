using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box_step : MonoBehaviour
{
    public comms_velo scomms;
    public GameObject box1;
    public GameObject box2;
    public int legNo;
    int boxno = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("a"))
        {
            Debug.Log("Forward");
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == box1)
        {
            boxno = 1;
        }
        else if (other.gameObject == box2)
        {
            boxno = 2;
        }
    }
    
    void OnTriggerstay(Collider other){
        if (boxno == 1)
        {
            scomms.sendMessage("G 1", legNo);
        }
        else if (boxno == 2)
        {
            scomms.sendMessage("G 2", legNo);
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        scomms.sendMessage("G 0", legNo);
    }
}
