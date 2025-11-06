using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shiprocking : MonoBehaviour
{
    public multicomms messenger;
    public int animate_time = 24;
    public float toggleInterval = 0.1f;
    public GameObject ship;
    float lcurrent = 0.8f;
    float rcurrent = 0f;
    bool left = true;
    bool rock = false;
    Quaternion origRot;

    // Start is called before the first frame update
    void Start()
    {        
        origRot = ship.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp("s")){
            StartCoroutine(rocking());
            rock = true;
        }

        if (rock){
            if (left){            
                ship.transform.rotation = ship.transform.rotation * Quaternion.Euler(0.02f, 0f, 0f);
            }else{
                ship.transform.rotation = ship.transform.rotation * Quaternion.Euler(-0.02f, 0f, 0f);
            }
        }
        
    }

    IEnumerator rocking(){
        float elapsedTime = 0.0f;
        while (elapsedTime < animate_time)
        {
            if (left){
                if (rcurrent < 0.8){
                    lcurrent -= 0.01f;
                    rcurrent += 0.01f;
                }else { left = false; }
            }else{
                if (lcurrent < 0.8){
                    lcurrent += 0.01f;
                    rcurrent -= 0.01f;
                }else { left = true; }
            }
            messenger.sendMessage(lcurrent, 0);
            messenger.sendMessage(rcurrent, 1);
            messenger.sendMessage(lcurrent, 2);
            messenger.sendMessage(rcurrent, 3);
            messenger.sendMessage(lcurrent, 4);
            messenger.sendMessage(rcurrent, 5);
            elapsedTime += toggleInterval;
            yield return new WaitForSeconds(toggleInterval);
        }
        messenger.noslack();
        rock = false;
        left = true;
        ship.transform.rotation = origRot;

    }
}
