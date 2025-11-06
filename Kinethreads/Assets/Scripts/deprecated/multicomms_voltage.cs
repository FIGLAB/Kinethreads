using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading.Tasks;

public class multicomms : MonoBehaviour
{
    // this is the voltage based multicomms communication service
    public bool COMMS_ON = true;

    public string Lfrontcomm;
    public string Rfrontcomm;
    public string Lhipcomm;
    public string Rhipcomm;
    public string Lbackcomm;
    public string Rbackcomm;
    public string backcomm;
    public string Theadcomm;
    public string Bheadcomm;

    SerialPort Lfront;
    SerialPort Rfront;
    SerialPort Lhip;
    SerialPort Rhip;
    SerialPort Lback;
    SerialPort Rback;
    SerialPort back;
    SerialPort Thead;
    SerialPort Bhead;

    public List<int> openPorts = new List<int>();
    string[] names = {"Left front", "Right front", "Left hip", "Right hip", "Left back", "Right back", "Back", "Top head", "Bottom head"};

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
            if (Lbackcomm != "none"){
                Lback = new SerialPort(Lbackcomm, 115200);
                while (!Lback.IsOpen) { Lback.Open(); }
                openPorts.Add(4);
            }
            if (Rbackcomm != "none"){
                Rback = new SerialPort(Rbackcomm, 115200);
                while (!Rback.IsOpen) { Rback.Open(); }
                openPorts.Add(5);
            }
            if (backcomm != "none"){
                back = new SerialPort(backcomm, 115200);
                while (!back.IsOpen) { back.Open(); }
                openPorts.Add(6);
            }
            if (Theadcomm != "none"){
                Thead = new SerialPort(Theadcomm, 115200);
                while (!Thead.IsOpen) { Thead.Open(); }
                openPorts.Add(7);
            }
            if (Bheadcomm != "none"){
                Bhead = new SerialPort(Bheadcomm, 115200);
                while (!Bhead.IsOpen) { Bhead.Open(); }
                openPorts.Add(8);
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
    
    public void sendMessage(float voltage, int idx){
        if (!COMMS_ON) { return; }
        Debug.Log("Send message: " + voltage.ToString("0.00") + " to port " + idx);
        if (idx == 0){ Lfront.Write(voltage.ToString("0.00") + "\n"); }
        else if (idx == 1){ Rfront.Write(voltage.ToString("0.00") + "\n"); }
        else if (idx == 2){ Lhip.Write(voltage.ToString("0.00") + "\n"); }
        else if (idx == 3){ Rhip.Write(voltage.ToString("0.00") + "\n"); }
        else if (idx == 4){ Lback.Write(voltage.ToString("0.00") + "\n"); }
        else if (idx == 5){ Rback.Write(voltage.ToString("0.00") + "\n"); }
        else if (idx == 6){ back.Write(voltage.ToString("0.00") + "\n"); }
        else if (idx == 7){ Thead.Write(voltage.ToString("0.00") + "\n"); }
        else if (idx == 8){ Bhead.Write(voltage.ToString("0.00") + "\n"); }
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
        noslack();
        var t = Task.Run(async delegate
              {
                 await Task.Delay(1000);
                 return 0;
              });
        t.Wait();
        off();
        if (Lfrontcomm != "none"){ Lfront.Close(); }
        if (Rfrontcomm != "none"){ Rfront.Close(); }
        if (Lhipcomm != "none"){ Lhip.Close(); }
        if (Rhipcomm != "none"){Rhip.Close(); }
        if (Lbackcomm != "none"){ Lback.Close(); }
        if (Rbackcomm != "none"){ Rback.Close(); }
        if (backcomm != "none"){ back.Close(); }
        if (Theadcomm != "none"){Thead.Close(); }
        if (Bheadcomm != "none"){Bhead.Close(); }
    }

}
