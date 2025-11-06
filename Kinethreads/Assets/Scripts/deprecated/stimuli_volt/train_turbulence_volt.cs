using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class train_turbulence_volt : MonoBehaviour
{
    public multicomms messenger;
    float noslack = 0.1f;
    float current = 0.1f;
    int animate_time = 10;
    Vector3 origPos;
    bool currOn = true;
    public float speed = 20f;
    float dist;
    bool isTurbulent = false;

    public GameObject following;
    public GameObject Ocam;
    public GameObject train;
    public GameObject trainshell;

    // Start is called before the first frame update
    void Start()
    {
        origPos = train.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp("s")){
            isTurbulent = true;
            StartCoroutine(vibrate());
        }else if(Input.GetKeyUp("r")){
            reset();
        }

        if (isTurbulent){
            dist = speed*Time.deltaTime;
            train.transform.position += new Vector3(dist, 0.0f, 0.0f);
            trainshell.transform.position = new Vector3(train.transform.position.x, train.transform.position.y + Random.Range(-0.05f, 0.05f), train.transform.position.z);
        }
        
        Ocam.transform.rotation = following.transform.rotation;
        Ocam.transform.position = following.transform.position;
    }
    
    IEnumerator vibrate(){
        float toggleInterval = 0.1f; 
        float elapsedTime = 0.0f;
         while (elapsedTime < animate_time)
        {
            if (currOn){
                for (int i=0; i<7; i++){
                    current = Random.Range(0.2f, 0.8f);
                    messenger.sendMessage(current, i);
                }
                currOn = false;
            }else{
                messenger.off();
                currOn = true;
            }
            elapsedTime += toggleInterval;
            yield return new WaitForSeconds(toggleInterval);
        }
        messenger.noslack();
        isTurbulent = false;
    }

    void reset(){
        messenger.noslack();
        train.transform.position = origPos;
    }
}
