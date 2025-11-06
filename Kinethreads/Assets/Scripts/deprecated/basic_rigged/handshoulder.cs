using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handshoulder : MonoBehaviour
{
    public multicomms messenger;
    float noslack = 0.1f;
    public float current = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        messenger.sendMessage(noslack, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp("w")){
            current += 0.1f;
            if (current > 1.0f){current = 1.0f;}
            messenger.sendMessage(current, 1);
        }else if (Input.GetKeyUp("s")){
            current -= 0.1f;
            if (current < 0.1f){current = 0.1f;}
            messenger.sendMessage(current, 1);
        }
        
    }

    void reset(){
        messenger.sendMessage(0.0f, 1);
    }
}
