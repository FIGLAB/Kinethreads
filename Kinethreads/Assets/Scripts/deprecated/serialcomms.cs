using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class serialcomms : MonoBehaviour
{

    public string sPortName = "COM6";
    private SerialPort _serialPort;
    string message;

    // Start is called before the first frame update
    void Start()
    {
        foreach (string str in SerialPort.GetPortNames())
        {
            Debug.Log(string.Format("Existing COM port: {0}", str));
        }
        _serialPort = new SerialPort(sPortName, 115200);
        while (!_serialPort.IsOpen)
        {
            _serialPort.Open();
        }
        Debug.Log("SERIAL OPEN*****************************************************************");

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void sendMessage(float current){
        Debug.Log("Send message: " + current.ToString("0.00"));
        _serialPort.Write(current.ToString("0.00") + "\n");
        // _serialPort.Write(animBytes, 0, animBytes.Length);
        Debug.Log("DONE SEND********************************************************");
    }
}
