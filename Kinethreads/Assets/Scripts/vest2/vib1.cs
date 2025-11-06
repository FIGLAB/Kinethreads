using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vib1 : MonoBehaviour
{
    public comms_velo scomms;
    public bool vibrate = false;
    public float curr1 = 0.8f;
    public float curr2 = 1.0f;
    int curr1on = 0;
    public float fps;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("h")){
            vibrate = !vibrate;
        }
        if (vibrate){
            if (curr1on == 0){
                scomms.sendMessage("T " + curr1.ToString(), 1);
                curr1on += 1;
            } else if (curr1on == 1){
                curr1on += 1;
            }else if (curr1on == 2){
                scomms.sendMessage("T " + curr2.ToString(), 1);
                curr1on += 1;
            }else{
                curr1on = 0;
            }
        } else{
            scomms.off();
        }
    }

    void FixedUpdate(){
        
    }
}
