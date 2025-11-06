using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Oculus.Interaction;

public class backpack : MonoBehaviour
{
    public comms_velo scomms;
    private float voltage = 0.0f;
    public float max_curr = 1.3f;
    public TMP_Text tex;
    public GameObject ball;
    bool bagAdded = false;
    public float totalVolt = 0.0f;
    float startSettle = 0.0f;
    float settleTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("r")){
            voltage = 0.0f;
            tex.text = voltage.ToString("0.00");
            scomms.noslack(0);
            scomms.noslack(1);
            scomms.off(6);
        }

        if (bagAdded){
            if (startSettle < settleTime){
                startSettle += Time.deltaTime;
                if (totalVolt < 1.0f){
                    voltage = 1.0f - (1.0f - totalVolt)*startSettle;
                    scomms.sendMessage("T " + voltage.ToString(), 6);
                }
            } else {
                bagAdded = false;
                scomms.sendMessage("T " + totalVolt.ToString(), 6);
            }
            tex.text = totalVolt.ToString("0.00");
        }
        
    }

    void OnTriggerEnter (Collider other){
        if (other.tag == "bagitem"){ 
            ball.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    void OnTriggerExit (Collider other){
        if (other.tag == "bagitem"){ 
            bagAdded = true;
            totalVolt += other.gameObject.GetComponent<GrabFreeTransformer>().voltage; 
            if (totalVolt > max_curr) { totalVolt = max_curr; }
            scomms.sendMessage("T 1.0", 6);
            ball.GetComponent<Renderer>().material.color = Color.black;
            other.gameObject.transform.position = new Vector3(-100.0f, -100.0f, -100.0f);
            startSettle = 0.0f;
        }
    }
}
