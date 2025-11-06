using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class high_gravity_volt : MonoBehaviour
{
    public multicomms messenger;
    float noslack = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp("s")){
            messenger.sendMessage(0.7f, 0);
            messenger.sendMessage(0.7f, 1);
            messenger.sendMessage(1.2f, 2);
            messenger.sendMessage(1.2f, 3);
            messenger.sendMessage(1.2f, 4);
            messenger.sendMessage(1.2f, 5);
            messenger.sendMessage(1.2f, 6);
        }else if (Input.GetKeyUp("w")){
            messenger.noslack();
        }
        
    }

    void reset(){
        messenger.off();
    }
}
