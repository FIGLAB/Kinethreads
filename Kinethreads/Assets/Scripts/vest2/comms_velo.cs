using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading.Tasks;

public class comms_velo : MonoBehaviour
{
    // This is the speed control multicomms communication service
    public bool KINE_ON = true;
    public bool VIB_ON = false;
    public bool WIRELESS_ON = false;
    public string serverIP = "192.168.0.178";
    public int port = 5088;
    private TcpClient client;
    private NetworkStream stream;
    string sidx = "a";
    bool isOn = false;

    public string Lfrontcomm;
    public string Rfrontcomm;
    public string Lhipcomm;
    public string Rhipcomm;
    public string Lbackcomm;
    public string Rbackcomm;
    public string backcomm;
    public string Theadcomm;
    public string Rheadcomm;
    public string frontcomm;
    public string Lheadcomm;

    SerialPort Lfront;
    SerialPort Rfront;
    SerialPort Lhip;
    SerialPort Rhip;
    SerialPort Lback;
    SerialPort Rback;
    SerialPort back;
    SerialPort Thead;
    SerialPort Lhead;
    SerialPort front;
    SerialPort Rhead;

    public List<int> openPorts = new List<int>();
    string[] names = {"L_front", "R_front", "L_hip", "R_hip", "L_back", "R_back", "Back", "Top_head", "Left_head", "front", "Right_head",};

    // Start is called before the first frame update
    void Start()
    {
        if (WIRELESS_ON){
            try
            {
                client = new TcpClient(serverIP, port);
                client.NoDelay = true;
                stream = client.GetStream();
                Debug.Log("[Unity] Connected to server.");
            }
            catch (Exception e)
            {
                Debug.LogError($"[Unity] Connection failed: {e.Message}");
            }
            openPorts.Add(0);
            openPorts.Add(1);
            openPorts.Add(2);
            openPorts.Add(3);
            openPorts.Add(4);
            openPorts.Add(5);
            openPorts.Add(6);
            openPorts.Add(7);
            openPorts.Add(8);
            openPorts.Add(9);
            openPorts.Add(10);
        }
        else if (KINE_ON){
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
            if (Lheadcomm != "none"){
                Lhead = new SerialPort(Lheadcomm, 115200);
                while (!Lhead.IsOpen) { Lhead.Open(); }
                openPorts.Add(8);
            }
            if (frontcomm != "none"){
                front = new SerialPort(frontcomm, 115200);
                while (!front.IsOpen) { front.Open(); }
                openPorts.Add(9);
            }
            if (Rheadcomm != "none"){
                Rhead = new SerialPort(Rheadcomm, 115200);
                while (!Rhead.IsOpen) { Rhead.Open(); }
                openPorts.Add(10);
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
        if (Input.GetKeyUp("n")){
            Debug.Log(" NO SLACK ");
            noslack();
        }else if (Input.GetKeyUp("b")){
            Debug.Log(" OFF ");
            off();
        }
        // else if (Input.GetKeyUp("m")){
        //     Debug.Log(" ALWAYS REEL (a little bit) ");
        //     alwaysReel();
        // }
        else if (Input.GetKeyUp("m")){
            Debug.Log(" RESET ");
            reset();
        }
        // else if (Input.GetKeyUp("h")){
        //     Debug.Log("NO HAPTICS");
        //     off();
        //     KINE_ON = false;
        //     VIB_ON = false;
        // }else if (Input.GetKeyUp("j")){
        //     Debug.Log("VIBRATION");
        //     off();
        //     KINE_ON = false;
        //     VIB_ON = true;
        // }else if (Input.GetKeyUp("k")){
        //     Debug.Log("STRING HAPTICS");
        //     off();
        //     KINE_ON = true;
        //     VIB_ON = false;
        // }else if (Input.GetKeyUp("l")){
        //     Debug.Log("BOTH");
        //     off();
        //     KINE_ON = true;
        //     VIB_ON = true;
        // }

        else if(Input.GetKeyUp("g")){
            isOn = !isOn;
            if (isOn){
                sendMessage("ME T 0.3", 7);
            }else{
                sendMessage("O 0", 0);
            }
        }
    }
    
    public void sendMessage(string command, int idx){
        Debug.Log("Send message: " + command + " to port " + idx);
        if(WIRELESS_ON){
            if(idx == 0){ sidx = "a"; }
            else if(idx == 1){ sidx = "b"; }
            else if(idx == 2){ sidx = "c"; }
            else if(idx == 3){ sidx = "d"; }
            else if(idx == 4){ sidx = "e"; }
            else if(idx == 5){ sidx = "f"; }
            else if(idx == 6){ sidx = "g"; }
            else if(idx == 7){ sidx = "h"; }
            else if(idx == 8){ sidx = "i"; }
            else if(idx == 9){ sidx = "j"; }
            stream.Write(Encoding.ASCII.GetBytes(sidx + command + "\n"));  
            stream.Flush();
        }
        if (!KINE_ON && !VIB_ON || WIRELESS_ON) { return; }
        else if (idx == 0){ Lfront.Write(command + "\n"); }
        else if (idx == 1){ Rfront.Write( command + "\n"); }
        else if (idx == 2){ Lhip.Write( command + "\n"); }
        else if (idx == 3){ Rhip.Write( command + "\n"); }
        else if (idx == 4){ Lback.Write( command + "\n"); }
        else if (idx == 5){ Rback.Write( command + "\n"); }
        else if (idx == 6){ back.Write( command + "\n"); }
        else if (idx == 7){ Thead.Write( command + "\n"); }
        else if (idx == 8){ Lhead.Write( command + "\n"); }
        else if (idx == 9){ front.Write( command + "\n"); }
        else if (idx == 10){ Rhead.Write( command + "\n"); }
    }

    public void sendMessageAll(string command){
        if (!KINE_ON && !VIB_ON) { return; }
        foreach (int a in openPorts){
            sendMessage(command, a);
        }
    }

    public void alwaysReel(){
        if (!KINE_ON && !VIB_ON) { return; }
        foreach (int a in openPorts){
            if (a != 8 && a != 7){ 
                sendMessage("T 0.1", a); 
                sendMessage("V 2", a); 
                sendMessage("C 0.1", a); 
            }
        }
    }

    public void alwaysReel(int a){
        if (!KINE_ON && !VIB_ON) { return; }
        if (a != 8 && a != 7){ 
            sendMessage("T 0.1", a); 
            sendMessage("V 2", a); 
            sendMessage("C 0.1", a); 
        }
    }

    public void noslack(){
        if (!KINE_ON && !VIB_ON) { return; }
        foreach (int a in openPorts){
            // if (a == 8){ sendMessage("O 0", a);} 
            // else { sendMessage("S 10", a); }
            sendMessage("S 10", a); 
        }
    }

    public void noslack(int a){
        if (!KINE_ON && !VIB_ON) { return; }
        sendMessage("S 10", a);
    }
    
    public void off(){
        if (!KINE_ON && !VIB_ON) { return; }
        foreach (int a in openPorts){
            sendMessage("O 0", a);
        }
    }

    public void off(int a){
        if (!KINE_ON && !VIB_ON) { return; }
        sendMessage("O 0", a);
    }

    public void reset(){
        if (!KINE_ON && !VIB_ON) { return; }
        noslack();
        var t = Task.Run(async delegate
              {
                 await Task.Delay(1000);
                 return 0;
              });
        t.Wait();
        off();
    }

    void OnApplicationQuit(){
        reset();
        if (Lfrontcomm != "none"){ Lfront.Close(); }
        if (Rfrontcomm != "none"){ Rfront.Close(); }
        if (Lhipcomm != "none"){ Lhip.Close(); }
        if (Rhipcomm != "none"){Rhip.Close(); }
        if (Lbackcomm != "none"){ Lback.Close(); }
        if (Rbackcomm != "none"){ Rback.Close(); }
        if (backcomm != "none"){ back.Close(); }
        if (Theadcomm != "none"){ Thead.Close(); }
        if (Lheadcomm != "none"){ Lhead.Close(); }
        if (Rheadcomm != "none"){ Rhead.Close(); }
        if (frontcomm != "none"){ front.Close(); }
    }

}
