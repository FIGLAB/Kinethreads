using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using System.IO.Ports;
using System.Threading.Tasks;

public class vib_comms : MonoBehaviour
{
    public bool VIB_ON = true;
    public string vibcomm;
    SerialPort vibc;
    public string[] vibmotors = {"Legs", "Lhand", "Rhand", "back", "head", "front"};

    void Start()
    {
        if (VIB_ON){
            vibc = new SerialPort(vibcomm, 115200);
            while (!vibc.IsOpen) { vibc.Open(); }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void sendMessage(int pwmidx, float pwmval){
        if (!VIB_ON) { return; }
        else { vibc.Write(pwmidx.ToString() + " " + pwmval.ToString() + "\n"); }
    }

    public void sendMessageAll(float pwmval){
        if (!VIB_ON) { return; }
        else { 
            for (int i = 0; i < vibmotors.Length; i++){
                vibc.Write(i.ToString() + " " + pwmval.ToString() + "\n");
            }
        }
    }

    public void off(){
        if (!VIB_ON) { return; }
        else { 
            for (int i = 0; i < vibmotors.Length; i++){
                vibc.Write(i.ToString() + " 0\n");
            }
        }
    }

    public void off(int pwmidx){
        if (!VIB_ON) { return; }
        else { vibc.Write(pwmidx.ToString() + " 0\n"); }
    }

    void OnApplicationQuit(){
        if (vibcomm != "none"){ vibc.Close(); }
    }



}
