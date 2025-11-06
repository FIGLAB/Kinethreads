using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading.Tasks;

public class sPort : MonoBehaviour
{

    public string portComm;
    private SerialPort pComm;

    // Start is called before the first frame update
    void Start()
    {
        pComm = new SerialPort(portComm, 115200);
        while (!pComm.IsOpen) { pComm.Open(); }
        Debug.Log("Serial " + portComm + " is OPEN");
        reset();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("r")){
            reset();
        }
    }

    public void reset(){
        sendMessage(0.5f);
        var t = Task.Run(async delegate
              {
                 await Task.Delay(1000);
                 return 0;
              });
        t.Wait();
        sendMessage(0);
    }

    public void sendMessage(float current){
        Debug.Log("Send message: " + current.ToString("0.00") + " to port " + portComm);
        pComm.Write(current.ToString("0.00") + "\n");
    }

    void OnApplicationQuit(){
        reset();
        pComm.Close();
    }
}
