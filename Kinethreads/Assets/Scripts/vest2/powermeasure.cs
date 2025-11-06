using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.IO.Ports;

public class powermeasure : MonoBehaviour
{
    public string portName = "COM47"; // Set your port name here
    public int baudRate = 115200;
    public float recordSeconds = 5.0f;
    public string fname = "serialdata";
    
    private SerialPort serialPort;
    public bool isReading = false;
    private List<string> serialDataBuffer = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        serialPort = new SerialPort(portName, baudRate);
        while (!serialPort.IsOpen) { serialPort.Open(); }
        Debug.Log("Logging Serial port opened");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && !isReading)
        {
            StartCoroutine(ReadSerialForSeconds(recordSeconds));
        }
    }

    IEnumerator ReadSerialForSeconds(float seconds)
    {
        isReading = true;
        serialDataBuffer.Clear();

        float startTime = Time.time;

        while (Time.time - startTime < seconds)
        {
            if (serialPort != null)
            {
                string line = serialPort.ReadLine();
                // Debug.Log(line); // Log the line read from the serial port
                serialDataBuffer.Add((Time.time - startTime).ToString() + "," + (float.Parse(line)-0.03).ToString());
            }

            yield return null;
        }

        WriteToCSV();
        isReading = false;
    }

    void WriteToCSV()
    {
        string filePath = Path.Combine(Application.dataPath, "power_csvs/" + fname + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");

        try
        {
            File.WriteAllLines(filePath, serialDataBuffer);
            Debug.Log("Data written to: " + filePath);
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to write CSV: " + e.Message);
        }
    }

    void OnApplicationQuit()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
            Debug.Log("Serial port closed");
        }
    }
}