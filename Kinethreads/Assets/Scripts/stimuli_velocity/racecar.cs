using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class racecar : MonoBehaviour
{
    public comms_velo scomms;
    public WheelVehicle car1;
    public float maxCurr = 1.1f;
    public float head_divider = 2f;
    public float turnBoost = 2f;
    float speed_ratio;
    float turn_ratio;
    public float currtime = 0.0f;
    public float pausetime = 0.1f;
    int priorAngle = 0;
    float elapsedTime = 0.2f;
    bool resetCapBool = false;
    float resetCap;
    bool vibon = false;
    bool vib1 = true;
    float tarspeed = 0.0f;
    float turntarspeed_left = 0.0f;
    float turntarspeed_right = 0.0f;

    public float currLow = 0.0f;
    public float currHighDiv = 3f;
    public float minfreq = 80f;
    public float currAdd = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update(){
        if (Input.GetKeyUp("h")){
            vibon = false;
        }else if (Input.GetKeyUp("j")){
            vibon = true;
            pausetime = 1/minfreq;
        }else if (Input.GetKeyUp("k")){
            vibon = false;
            pausetime = 0.1f;
        }else if (Input.GetKeyUp("l")){
            vibon = true;
            pausetime = 1/minfreq;
        }

        // if (vibon){
        //     if (currtime2 > pausetime2){
        //         currtime2 = 0.0f;
        //         if (!scomms.KINE_ON){
        //             scomms.sendMessage("B " + currLow.ToString() + " " + (speed_ratio/currHighDiv).ToString() + " " + minfreq+speed_ratio*50, 2); 
        //             scomms.sendMessage("B " + currLow.ToString() + " " + (speed_ratio/currHighDiv).ToString() + " " + minfreq+speed_ratio*50, 3);
        //         }
        //         scomms.sendMessage("B " + currLow.ToString() + " " + (speed_ratio/currHighDiv).ToString() + " " + minfreq+speed_ratio*50, 4);
        //         scomms.sendMessage("B " + currLow.ToString() + " " + (speed_ratio/currHighDiv).ToString() + " " + minfreq+speed_ratio*50, 5);
        //         scomms.sendMessage("B " + currLow.ToString() + " " + (speed_ratio/currHighDiv).ToString() + " " + minfreq+speed_ratio*50, 9); //front
        //         scomms.sendMessage("B " + currLow.ToString() + " " + (speed_ratio/currHighDiv).ToString() + " " + minfreq+speed_ratio*50, 6);
        //         scomms.sendMessage("B " + currLow.ToString() + " " + (speed_ratio/currHighDiv/2).ToString() + " " + minfreq+speed_ratio*50, 7); //Thead
        //     } else {
        //         currtime2 += Time.deltaTime;
        //     }
        // }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        speed_ratio = car1.Speed/80.0f;
        if (speed_ratio > 1.0f) { speed_ratio = 1.0f; }
        else if (speed_ratio < 0) { speed_ratio = 0; }

        turn_ratio = car1.turnAngle/20.0f * Mathf.Abs(car1.Speed/80.0f) * turnBoost;
        if (turn_ratio > 1.0f) { turn_ratio = 1.0f; }
        else if (turn_ratio < -1.0f) { turn_ratio = -1.0f; }
        if (scomms.KINE_ON){ 
            // if (turn_ratio > 1.0f){
            //     scomms.sendMessage("T " + ( turn_ratio * -maxCurr/2).ToString(), 8); // left turn
            // }else{
            //     scomms.sendMessage("T " + ( turn_ratio * maxCurr/2).ToString(), 10); // right turn
            // }
            // scomms.sendMessage("T " + ( turn_ratio * -maxCurr).ToString(), 8);  // left/right head
            if (!scomms.VIB_ON){
                    scomms.sendMessage("T " + (Mathf.Max(turn_ratio, 0)).ToString(), 2); // left hip
                    scomms.sendMessage("T " + (Mathf.Min(turn_ratio, 0)).ToString(), 3); // right hip
            }
            
        }
        
        if (currtime > pausetime){
            if (!scomms.VIB_ON){
                scomms.sendMessage("T " + (speed_ratio*maxCurr).ToString(), 6); // back
                scomms.sendMessage("T " + ((speed_ratio*maxCurr)/head_divider).ToString(), 7); // top of head
            } else if (!scomms.KINE_ON) { 
                if (vib1){
                    tarspeed = currLow;
                    vib1 = !vib1;
                }else{ 
                    tarspeed = speed_ratio/currHighDiv;
                    vib1 = !vib1;
                }
                scomms.sendMessage("T " + tarspeed.ToString(), 2); 
                scomms.sendMessage("T " + tarspeed.ToString(), 3); 
                scomms.sendMessage("T " + tarspeed.ToString(), 4); 
                scomms.sendMessage("T " + tarspeed.ToString(), 5); 
                scomms.sendMessage("T " + tarspeed.ToString(), 9); 
                scomms.sendMessage("T " + (tarspeed * maxCurr).ToString(), 6); //back
                scomms.sendMessage("T " + (tarspeed*maxCurr/head_divider).ToString(), 7); //Thead
            } else {
                if (vib1){
                    tarspeed = currLow;
                    turntarspeed_left = Mathf.Max(turn_ratio, 0) * maxCurr;
                    turntarspeed_right = Mathf.Min(turn_ratio, 0) * -maxCurr;
                    vib1 = !vib1;
                }else{ 
                    tarspeed = speed_ratio/currHighDiv;
                    turntarspeed_left = Mathf.Max(turn_ratio, 0) * (maxCurr-currAdd);
                    turntarspeed_right = Mathf.Min(turn_ratio, 0) * (-maxCurr+currAdd);
                    vib1 = !vib1;
                }
                scomms.sendMessage("T " + turntarspeed_left.ToString(), 2);
                scomms.sendMessage("T " + turntarspeed_right.ToString(), 3);
                scomms.sendMessage("T " + tarspeed.ToString(), 4); 
                scomms.sendMessage("T " + tarspeed.ToString(), 5); 
                scomms.sendMessage("T " + tarspeed.ToString(), 9); 
                scomms.sendMessage("T " + (tarspeed * maxCurr).ToString(), 6); //back
                scomms.sendMessage("T " + (tarspeed*maxCurr/head_divider).ToString(), 7); //Thead
                // scomms.sendMessage("B " + currLow.ToString() + " " + (speed_ratio/currHighDiv).ToString() + " " + minfreq+speed_ratio*50, 4);
                // scomms.sendMessage("B " + currLow.ToString() + " " + (speed_ratio/currHighDiv).ToString() + " " + minfreq+speed_ratio*50, 5);
                // scomms.sendMessage("B " + currLow.ToString() + " " + (speed_ratio/currHighDiv).ToString() + " " + minfreq+speed_ratio*50, 9); //front
                // scomms.sendMessage("B " + currLow.ToString() + " " + (speed_ratio/currHighDiv).ToString() + " " + minfreq+speed_ratio*50, 6);
                // scomms.sendMessage("B " + currLow.ToString() + " " + (speed_ratio/currHighDiv/2).ToString() + " " + minfreq+speed_ratio*50, 7); //Thead
            }
            currtime = 0.0f;
        } else {
            currtime += Time.deltaTime;
        }
        

    }
}
