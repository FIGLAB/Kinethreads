using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class racetrack_volt : MonoBehaviour
{
    public multicomms scomms;
    public int motorIdx = 6;
    public Transform origin;
    public float current = 0.1f;
    public float speed = 3f;
    public Transform[] waypoints; // Array of waypoints for the track

    public GameObject following;
    public GameObject Ocam;

    private int currIdx = 0;
    bool racing = false;
    bool speeding = true;

    Transform target;
    Vector3 direction;
    float dist;

    void Update()
    {
        if(Input.GetKeyUp("s")){
           racing = true;
        }else if (Input.GetKeyUp("r")){
            reset();
        }

        if (racing){
            target = waypoints[currIdx];
            this.transform.LookAt(target);
            direction = target.position - this.transform.position;
            dist = speed*Time.deltaTime;
            if (direction.magnitude <= dist) { 
                currIdx++; 
                if (currIdx >= waypoints.Length) {
                    racing = false;
                    scomms.sendMessage(0.0f, motorIdx);
                }
                if (current >= 1.2f) { speeding = false; }
                if (speeding){ current += 0.1f; }
                // else if (currIdx > 15){ current -= 0.15f; }
                scomms.sendMessage(current, motorIdx);
            } else { 
                transform.Translate(direction.normalized * dist, Space.World); 
            }
            if (speeding){ speed += 0.1f; }
            // else if (currIdx > 15){ speed -= 0.1f; }
        }else{
            scomms.sendMessage(0.0f, motorIdx);
        }
        Ocam.transform.rotation = following.transform.rotation;
        Ocam.transform.position = following.transform.position;
    }

    void reset(){
        currIdx = 0;
        this.transform.position = origin.transform.position;
        transform.LookAt(waypoints[0]);
        speed = 3f;
        current = 0.1f;
        racing = false;
        speeding = true;
    }
}
