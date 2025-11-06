using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterfall : MonoBehaviour
{
    public comms_velo scomms;
    public GameObject ball;
    bool inWater = false;
    public float minCurr = 0.15f;
    public float maxCurr = 1.2f;
    public float timeInterval = 0.005f;
    public int intervalMod = 3;
    float elapsedTime = 0.0f;
    float translation = 0.0f;


    float r = 0.0f;
    public float rumble1 = 0;
    public float rumble2 = 0.2f;
    public float frequency = 50f;

    public GameObject me;
    public float speed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inWater){
            if (elapsedTime < timeInterval){
                elapsedTime += Time.deltaTime;
            } else {
                elapsedTime = 0.0f;
                if (scomms.KINE_ON && !scomms.VIB_ON){
                    if (Random.Range(0,intervalMod) == 1) { scomms.sendMessage("T " + (Random.Range(minCurr, maxCurr)).ToString(), 0);}
                    if (Random.Range(0,intervalMod) == 1) { scomms.sendMessage("T " + (Random.Range(minCurr, maxCurr)).ToString(), 1);}
                    if (Random.Range(0,intervalMod) == 1) { scomms.sendMessage("T " + Random.Range(minCurr, maxCurr).ToString(), 6);}
                    if (Random.Range(0,intervalMod) == 1) { scomms.sendMessage("T " + Random.Range(minCurr, maxCurr).ToString(), 9);}
                    if (Random.Range(0,intervalMod) == 1) { scomms.sendMessage("T " + Random.Range(minCurr, maxCurr).ToString(), 7);}
                }else if (scomms.KINE_ON && scomms.VIB_ON){
                    if (Random.Range(0,intervalMod) == 1) { r = Random.Range(minCurr, maxCurr); scomms.sendMessage( "B " + r.ToString() + " " + (r+rumble2*2).ToString() + " " + frequency/2, 0);}
                    if (Random.Range(0,intervalMod) == 1) { r = Random.Range(minCurr, maxCurr); scomms.sendMessage( "B " + r.ToString() + " " + (r+rumble2*2).ToString() + " " + frequency/2, 1);}
                    if (Random.Range(0,intervalMod) == 1) { r = Random.Range(minCurr, maxCurr); scomms.sendMessage( "B " + r.ToString() + " " + (r+rumble2).ToString() + " " + frequency, 6);}
                    if (Random.Range(0,intervalMod) == 1) { r = Random.Range(minCurr, maxCurr); scomms.sendMessage( "B " + r.ToString() + " " + (r+rumble2).ToString() + " " + frequency, 9);}
                    if (Random.Range(0,intervalMod) == 1) { r = Random.Range(minCurr, maxCurr); scomms.sendMessage( "B " + r.ToString() + " " + (r+rumble2).ToString() + " " + frequency, 7);}
                }
            }
        } 
        translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        me.transform.Translate(0, 0, translation);
        if (Input.GetKey("g")){
            scomms.sendMessage("T 0.2", 4);
            scomms.sendMessage("T 0.2", 5);
        }
    }

    void OnTriggerEnter (Collider other){
        if (other.tag == "head"){ 
            inWater = true; 
            if (scomms.VIB_ON && !scomms.KINE_ON){
                scomms.sendMessage("B " + (rumble1*2).ToString() + " " + (rumble2*2).ToString() + " " + frequency/2, 0);
                scomms.sendMessage("B " + (rumble1*2).ToString() + " " + (rumble2*2).ToString() + " " + frequency/2, 1);
                scomms.sendMessage("B " + (rumble1*2).ToString() + " " + (rumble2*2).ToString() + " " + frequency/2, 4);
                scomms.sendMessage("B " + (rumble1*2).ToString() + " " + (rumble2*2).ToString() + " " + frequency/2, 5);
                scomms.sendMessage("B " + (rumble1).ToString() + " " + (rumble2).ToString() + " " + frequency, 6);
                scomms.sendMessage("B " + (rumble1).ToString() + " " + (rumble2).ToString() + " " + frequency, 9);
                scomms.sendMessage("B " + (rumble1).ToString() + " " + (rumble2).ToString() + " " + frequency, 7);
            }
            ball.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    void OnTriggerExit (Collider other){
        if (other.tag == "head"){ inWater = false; 
            ball.GetComponent<Renderer>().material.color = Color.black;
            scomms.off();
        }
    }
}
