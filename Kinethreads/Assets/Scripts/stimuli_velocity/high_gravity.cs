using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class high_gravity : MonoBehaviour
{
    public comms_velo scomms;
    public TMP_Text tex;
    public GameObject lights_medium;
    public GameObject lights_high;
    public Material defaultSky;
    public Material stars;
    bool gravOn;
    float currTime = 0.0f;
    public float gravInterval = 3.0f;
    int gravType = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp("e")){
            highGravity();
        }else if (Input.GetKeyUp("d")){
            midGravity();
        }else if (Input.GetKeyUp("c")){
            lowGravity();
        }

        if (Input.GetKeyUp("s")){
            gravOn = true;
            currTime = 0.0f;
        }

        if (gravOn){
            if (currTime > gravInterval){
                gravType += 1;
                currTime = 0.0f;
                if (gravType == 0){
                lowGravity();
                }else if (gravType == 1){
                    midGravity();
                }else if (gravType == 2){
                    highGravity();
                }else if (gravType == 3){
                    gravOn = false;
                    gravType = 0;
                    lowGravity();
                }
            }else{
                currTime += Time.deltaTime;
            }
        }
        
    }
    
    void highGravity(){
        scomms.sendMessage("T 0.7", 0);
        scomms.sendMessage("T 0.7", 1);
        scomms.sendMessage("T 1", 2);
        scomms.sendMessage("T 1", 3);
        scomms.sendMessage("T 1.1", 6);
        scomms.sendMessage("T 0.4", 7);
        tex.text = "High Gravity";
        lights_high.SetActive(true);
        lights_medium.SetActive(false);
        RenderSettings.skybox = stars;
    }

    void midGravity(){
        scomms.sendMessage("T 0.25", 0);
        scomms.sendMessage("T 0.25", 1);
        scomms.sendMessage("T 0.35", 2);
        scomms.sendMessage("T 0.35", 3);
        scomms.sendMessage("T 0.6", 6);
        scomms.sendMessage("T 0.3", 7);
        tex.text = "Medium Gravity";
        lights_high.SetActive(false);
        lights_medium.SetActive(true);
        RenderSettings.skybox = stars;
    }

    void lowGravity(){
        scomms.noslack();
        tex.text = "Normal Gravity";
        lights_high.SetActive(false);
        lights_medium.SetActive(false);
        RenderSettings.skybox = defaultSky;
    }

}
