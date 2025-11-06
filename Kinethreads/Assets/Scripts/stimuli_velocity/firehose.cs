using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class firehose : MonoBehaviour
{
    public comms_velo scomms;
    public ParticleSystem particleSystem; // Reference to the particle system
    private ParticleSystem.EmissionModule emissionModule;
    private ParticleSystem.MainModule mainModule;
    public float changeAmount = 20.0f;
    public float maxRate = 40.0f;
    public float zdist = 0.0f;
    public GameObject leftHand;
    public GameObject rightHand;
    public TMP_Text tex;
    float speed = 0.0f;

    public float currLow = 0.0f;
    public float currHighDiv_vib = 1.5f;
    public float minfreq = 100f;
    public float freqMult = 300f;
    public float currLowDiv = 2f;

    float updateRate = 0.1f;
    float nextUpdate = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        emissionModule = particleSystem.emission;
        mainModule = particleSystem.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKey(KeyCode.W))
        // {
            // emissionModule.rateOverTime = Mathf.Max(0, Mathf.Min(emissionModule.rateOverTime.constant + changeAmount * Time.deltaTime, maxRate));
            // scomms.sendMessage("T 0.05", 0);
            // scomms.sendMessage("T 0.05", 4);
            // scomms.sendMessage("T 0.05", 5);
        // }
        // else if (Input.GetKey(KeyCode.S))
        // {
            // emissionModule.rateOverTime = Mathf.Max(0, Mathf.Max(emissionModule.rateOverTime.constant - changeAmount * Time.deltaTime, 0.0f));
        // }

        zdist = leftHand.transform.position.z - rightHand.transform.position.z;
        mainModule.startSpeed = Mathf.Max(0, Mathf.Min(zdist*10, 10.0f));
        emissionModule.rateOverTime = Mathf.Max(0, Mathf.Min(zdist*200, maxRate));

        if (nextUpdate > updateRate){
            if (!scomms.VIB_ON){ 
                scomms.sendMessage("T " + (emissionModule.rateOverTime.constant/maxRate).ToString(), 5); 
            }
            else if (!scomms.KINE_ON){
                scomms.sendMessage("B " + currLow.ToString() + " " + (emissionModule.rateOverTime.constant/maxRate/currHighDiv_vib).ToString() + " " + (Mathf.Max(minfreq, emissionModule.rateOverTime.constant/maxRate * freqMult)).ToString(), 5); 
                scomms.sendMessage("B " + currLow.ToString() + " " + (emissionModule.rateOverTime.constant/maxRate/currHighDiv_vib).ToString() + " " + (Mathf.Max(minfreq, emissionModule.rateOverTime.constant/maxRate * freqMult)).ToString(), 1); 
            }
            else { 
                scomms.sendMessage("B " + (emissionModule.rateOverTime.constant/maxRate/currLowDiv).ToString() + " " + (emissionModule.rateOverTime.constant/maxRate).ToString() + " " + (Mathf.Max(minfreq, emissionModule.rateOverTime.constant/maxRate * freqMult)).ToString(), 5); 
            }
            nextUpdate = 0.0f;
        }else{
            nextUpdate += Time.deltaTime;
        }

        tex.text = mainModule.startSpeed.constant.ToString();
        
    }
}
