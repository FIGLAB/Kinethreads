using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manualpack : MonoBehaviour
{
    public comms_velo scomms;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)){
            scomms.sendMessage("T 0.6", 0);
            scomms.sendMessage("O 0", 1);

        }else if (Input.GetKey(KeyCode.S)){
            scomms.sendMessage("T 1", 1);
            scomms.sendMessage("O 0", 0);
        }else if (Input.GetKey(KeyCode.D)){
            scomms.sendMessage("O 0", 1);
            scomms.sendMessage("O 0", 0);
            scomms.sendMessage("T 1", 6);
        }else if (Input.GetKey(KeyCode.E)){
            scomms.sendMessage("O 0", 1);
            scomms.sendMessage("O 0", 0);
            scomms.sendMessage("T 0.6", 6);
        }
        
    }
}
