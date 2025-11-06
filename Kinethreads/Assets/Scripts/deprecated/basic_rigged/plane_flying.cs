using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plane_flying : MonoBehaviour
{
    public multicomms messenger;
    float noslack = 0.1f;
    public float current = 0.1f;
    public int animate_time = 3;
    float stime;
    float currt;

    // Start is called before the first frame update
    void Start()
    {
        messenger.sendMessage(current, 0);
        messenger.sendMessage(current, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp("s")){
            Debug.Log(" VIBRATE TIME ");
            StartCoroutine(vibrate());
        }
        
    }
    
    IEnumerator vibrate(){
        float toggleInterval = 0.1f; // Interval between toggles
        float elapsedTime = 0.0f;

         while (elapsedTime < animate_time)
        {
            if (current == 0.1f){current = 0.5f;}
            else {current = 0.1f;}
            messenger.sendMessage(current, 0);
            messenger.sendMessage(current, 1);
            elapsedTime += toggleInterval;
            yield return new WaitForSeconds(toggleInterval);
        }
        messenger.sendMessage(noslack, 0);
        messenger.sendMessage(noslack, 1);
    }

    void reset(){
        messenger.sendMessage(0.0f, 0);
        messenger.sendMessage(0.0f, 1);
    }
}
