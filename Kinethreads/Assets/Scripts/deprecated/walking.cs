using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walking : MonoBehaviour
{
    public float torque = 0.5f;
    public serialcomms scomms;

    // Start is called before the first frame update
    void Start()
    {
        scomms.sendMessage(0.5f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnApplicationQuit()
    {
        scomms.sendMessage(0.0f);
    }
}
