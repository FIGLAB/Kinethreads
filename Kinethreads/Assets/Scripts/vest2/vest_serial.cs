using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading.Tasks;

public class vest_serial : MonoBehaviour
{
    public bool COMMS_ON = true;

    public string Lfrontcomm;
    public string Rfrontcomm;
    public string Lhipcomm;
    public string Rhipcomm;
    public string Lshouldercomm;
    public string Rshouldercomm;
    public string headcomm;
    public string wristcomm;

    SerialPort Lfront;
    SerialPort Rfront;
    SerialPort Lhip;
    SerialPort Rhip;
    SerialPort Lshoulder;
    SerialPort Rshoulder;
    SerialPort head;
    SerialPort wrist;

    public List<int> openPorts = new List<int>();
    string[] names = {"Left front", "Right front", "Left hip", "Right hip", "Left shoulder", "Right shoulder", "Head", "Wrist"};

    // Start is called before the first frame update
    void Start()
    {
        if (COMMS_ON){
            if (Lfrontcomm != "none"){
                Lfront = new SerialPort(Lfrontcomm, 115200);
                while (!Lfront.IsOpen) { Lfront.Open(); }
                openPorts.Add(0);
            }
            if (Rfrontcomm != "none"){
                Rfront = new SerialPort(Rfrontcomm, 115200);
                while (!Rfront.IsOpen) { Rfront.Open(); }
                openPorts.Add(1);
            }
            if (Lhipcomm != "none"){
                Lhip = new SerialPort(Lhipcomm, 115200);
                while (!Lhip.IsOpen) { Lhip.Open(); }
                openPorts.Add(2);
            }
            if (Rhipcomm != "none"){
                Rhip = new SerialPort(Rhipcomm, 115200);
                while (!Rhip.IsOpen) { Rhip.Open(); }
                openPorts.Add(3);
            }
            if (Lshouldercomm != "none"){
                Lshoulder = new SerialPort(Lshouldercomm, 115200);
                while (!Lshoulder.IsOpen) { Lshoulder.Open(); }
                openPorts.Add(4);
            }
            if (Rshouldercomm != "none"){
                Rshoulder = new SerialPort(Rshouldercomm, 115200);
                while (!Rshoulder.IsOpen) { Rshoulder.Open(); }
                openPorts.Add(5);
            }
            if (headcomm != "none"){
                head = new SerialPort(headcomm, 115200);
                while (!head.IsOpen) { head.Open(); }
                openPorts.Add(6);
            }
            if (wristcomm != "none"){
                wrist = new SerialPort(wristcomm, 115200);
                while (!wrist.IsOpen) { wrist.Open(); }
                openPorts.Add(7);
            }
            var allopen = "";
            foreach (int a in openPorts){
                allopen = allopen + names[a] + " ";
            }
            Debug.Log("Open ports: " + allopen);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("n") && COMMS_ON){
            Debug.Log(" NO SLACK ");
            noslack();
        }else if (Input.GetKeyUp("b") && COMMS_ON){
            Debug.Log(" OFF ");
            off();
        }
    }
    
    public void sendMessage(float current, int idx){
        if (!COMMS_ON) { return; }
        Debug.Log("Send message: " + current.ToString("0.00") + " to port " + idx);
        if (idx == 0){ Lfront.Write(current.ToString("0.00") + "\n"); }
        else if (idx == 1){ Rfront.Write(current.ToString("0.00") + "\n"); }
        else if (idx == 2){ Lhip.Write(current.ToString("0.00") + "\n"); }
        else if (idx == 3){ Rhip.Write(current.ToString("0.00") + "\n"); }
        else if (idx == 4){ Lshoulder.Write(current.ToString("0.00") + "\n"); }
        else if (idx == 5){ Rshoulder.Write(current.ToString("0.00") + "\n"); }
        else if (idx == 6){ head.Write(current.ToString("0.00") + "\n"); }
        else if (idx == 7){ wrist.Write(current.ToString("0.00") + "\n"); }
    }
    
    public void sendMessageAll(float current){
        foreach (int a in openPorts){
            sendMessage(current, a);
        }
    }

    public void noslack(){
        foreach (int a in openPorts){
            sendMessage(0.1f, a);
        }
    }
    
    public void off(){
        foreach (int a in openPorts){
            sendMessage(0.0f, a);
        }
    }


    public void reset(){
        foreach (int a in openPorts){
            sendMessage(0.5f, a);
        }
        var t = Task.Run(async delegate
              {
                 await Task.Delay(1000);
                 return 0;
              });
        t.Wait();
        foreach (int a in openPorts){
            sendMessage(0.0f, a);
        }
    }

    void OnApplicationQuit(){
        off();
        if (Lfrontcomm != "none"){ Lfront.Close(); }
        if (Rfrontcomm != "none"){ Rfront.Close(); }
        if (Lhipcomm != "none"){ Lhip.Close(); }
        if (Rhipcomm != "none"){Rhip.Close(); }
        if (Lshouldercomm != "none"){ Lshoulder.Close(); }
        if (Rshouldercomm != "none"){ Rshoulder.Close(); }
        if (headcomm != "none"){ head.Close(); }
        if (wristcomm != "none"){wrist.Close(); }
    }

}
