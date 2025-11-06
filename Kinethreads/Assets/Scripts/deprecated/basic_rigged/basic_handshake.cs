using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basic_handshake : MonoBehaviour
{
    public multicomms messenger;
    float noslack = 0.1f;
    public int animate_time = 3;
    float shake = 0;
    float stime;
    float currt;

    // Start is called before the first frame update
    void Start()
    {
        messenger.sendMessage(noslack,1);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp("u")){
            Debug.Log("SHAKING ANIMATION");
            StartCoroutine(shaking());
        }
        
    }

    IEnumerator shaking(){
        float toggleInterval = 0.5f; // Interval between toggles
        float elapsedTime = 0.0f;
        while (elapsedTime < animate_time)
        {
            if (shake == 0){
                shake = 1;
                messenger.sendMessage(0.5f, 1);
            }else {
                shake = 0;
                messenger.sendMessage(0.0f, 1);
            }
            elapsedTime += toggleInterval;
            yield return new WaitForSeconds(toggleInterval);
        }
        messenger.sendMessage(noslack, 1);
    }
}
