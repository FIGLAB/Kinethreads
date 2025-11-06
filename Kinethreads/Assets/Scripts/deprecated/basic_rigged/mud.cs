using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mud : MonoBehaviour
{
    public multicomms messenger;
    public float current = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        messenger.sendMessage(current, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp("w")){
            current += 0.2f;
            if (current > 1.0f){current = 1.0f;}
            messenger.sendMessage(current, 0);
        }else if (Input.GetKeyUp("s")){
            current -= 0.2f;
            if (current < 0.1f){current = 0.1f;}
            messenger.sendMessage(current, 0);
        }
        
    }

    void reset(){
        messenger.sendMessage(0.0f, 0);
    }
}
