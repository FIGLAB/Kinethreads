using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading.Tasks;

public class basic_multicomms : MonoBehaviour
{

    // public sPort wrist;
    // public sPort back;
    // public sPort Lhip;
    // public sPort Rhip;
    // public sPort Lfoot;
    // public sPort Rfoot;
    // public sPort Lshoulder;
    // public sPort Rshoulder;

    public string Lfootcomm;
    public string Rfootcomm;
    public string Lhipcomm;
    public string backcomm;
    public string Rhipcomm;
    public string Lshouldercomm;
    public string Rshouldercomm;
    public string wristcomm;

    public SerialPort Lfoot;
    public SerialPort Rfoot;
    public SerialPort Lhip;
    public SerialPort back;
    public SerialPort Rhip;
    public SerialPort Lshoulder;
    public SerialPort Rshoulder;
    public SerialPort wrist;

    List<int> openPorts = new List<int>();
    string[] names = {"Left foot", "Right foot", "Left hip", "Back", "Right hip", "Left shoulder", "Right shoulder", "Wrist"};

    // Start is called before the first frame update
    void Start()
    {
        if (Lfootcomm != "none"){
            Lfoot = new SerialPort(Lfootcomm, 115200);
            while (!Lfoot.IsOpen) { Lfoot.Open(); }
            openPorts.Add(0);
        }
        if (Rfootcomm != "none"){
            Rfoot = new SerialPort(Rfootcomm, 115200);
            while (!Rfoot.IsOpen) { Rfoot.Open(); }
            openPorts.Add(1);
        }
        if (Lhipcomm != "none"){
            Lhip = new SerialPort(Lhipcomm, 115200);
            while (!Lhip.IsOpen) { Lhip.Open(); }
            openPorts.Add(2);
        }
        if (backcomm != "none"){
            back = new SerialPort(backcomm, 115200);
            while (!back.IsOpen) { back.Open(); }
            openPorts.Add(3);
        }
        if (Rhipcomm != "none"){
            Rhip = new SerialPort(Rhipcomm, 115200);
            while (!Rhip.IsOpen) { Rhip.Open(); }
            openPorts.Add(4);
        }
        if (Lshouldercomm != "none"){
            Lshoulder = new SerialPort(Lshouldercomm, 115200);
            while (!Lshoulder.IsOpen) { Lshoulder.Open(); }
            openPorts.Add(5);
        }
        if (Rshouldercomm != "none"){
            Rshoulder = new SerialPort(Rshouldercomm, 115200);
            while (!Rshoulder.IsOpen) { Rshoulder.Open(); }
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("n")){
            Debug.Log(" NO SLACK ");
            noslack();
        }else if (Input.GetKeyUp("b")){
            Debug.Log(" OFF ");
            off();
        }
    }
    
    public void sendMessage(float current, int idx){
        Debug.Log("Send message: " + current.ToString("0.00") + " to port " + idx);
        if (idx == 0){ Lfoot.Write(current.ToString("0.00") + "\n"); }
        else if (idx == 1){ Rfoot.Write(current.ToString("0.00") + "\n"); }
        else if (idx == 2){ Lhip.Write(current.ToString("0.00") + "\n"); }
        else if (idx == 3){ back.Write(current.ToString("0.00") + "\n"); }
        else if (idx == 4){ Rhip.Write(current.ToString("0.00") + "\n"); }
        else if (idx == 5){ Lshoulder.Write(current.ToString("0.00") + "\n"); }
        else if (idx == 6){ Rshoulder.Write(current.ToString("0.00") + "\n"); }
        else if (idx == 7){ wrist.Write(current.ToString("0.00") + "\n"); }
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
        foreach (int a in openPorts){
            sendMessage(0.0f, a);
        }
        if (Lfootcomm != "none"){ Lfoot.Close(); }
        if (Rfootcomm != "none"){ Rfoot.Close(); }
        if (Lhipcomm != "none"){ Lhip.Close(); }
        if (backcomm != "none"){ back.Close(); }
        if (Rhipcomm != "none"){Rhip.Close(); }
        if (Lshouldercomm != "none"){ Lshoulder.Close(); }
        if (Rshouldercomm != "none"){ Rshoulder.Close(); }
        if (wristcomm != "none"){wrist.Close(); }
    }

}
