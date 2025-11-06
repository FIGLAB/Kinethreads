using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basic_shiprocking : MonoBehaviour
{
    public multicomms messenger;
    float noslack = 0.1f;
    public float lcurrent = 0.1f;
    public float rcurrent = 0.1f;
    public int animate_time = 8;
    public int toggleInterval = 1;
    float rock = 0;
    float stime;
    float currt;

    // Start is called before the first frame update
    void Start()
    {
        messenger.sendMessage(noslack, 0);
        messenger.sendMessage(noslack, 1);
        messenger.sendMessage(noslack, 2);
        messenger.sendMessage(noslack, 4);
        // messenger.Lshoulder.sendMessage(noslack, 5);
        // messenger.Rshoulder.sendMessage(noslack, 6);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp("s")){
            StartCoroutine(rocking());
        }
        
    }

    IEnumerator rocking(){
        float elapsedTime = 0.0f;
        while (elapsedTime < animate_time)
        {
            if (rock == 0){
                rock = 1;
                lcurrent = 1.0f;
                rcurrent = 0.0f;
            }else {
                rock = 0;
                lcurrent = 0.0f;
                rcurrent = 1.0f;
            }
            messenger.sendMessage(lcurrent, 0);
            messenger.sendMessage(rcurrent, 1);
            messenger.sendMessage(lcurrent, 2);
            messenger.sendMessage(rcurrent, 4);
            elapsedTime += toggleInterval;
            yield return new WaitForSeconds(toggleInterval);
        }
        messenger.sendMessage(noslack, 0);
        messenger.sendMessage(noslack, 1);
        messenger.sendMessage(noslack, 2);
        messenger.sendMessage(noslack, 4);
    }
}
