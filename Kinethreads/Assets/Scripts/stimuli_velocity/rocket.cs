using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{
    public comms_velo scomms;
    public GameObject headcenter;
    public float offsetY = 0.8f;
    public float animate_time = 6f;
    float speed = 0f;
    public float speed_multiplier = 3f;
    bool blastOff = false;
    float gforce;
    float maxCurr = 1.2f;
    float dist;
    float maxdist = 600.0f;
    float totaldist = 0.0f;

    public float vib_currLow = 0.2f;
    public float currHighDiv = 3f;
    public float minfreq = 100f;
    public float currAdd = 0.2f;
    public float rumble1 = -0.2f;
    public float rumble2 = 0.3f;


    public ParticleSystem particleSystem;
    public AudioSource audioSource;
    public GameObject tunnel;
    Vector3 origTunnelPos;

    // Start is called before the first frame update
    void Start()
    {
        particleSystem.Stop();
        origTunnelPos = tunnel.transform.position;
        
    }

    void Update(){
        if(Input.GetKeyUp("s")){
            blastOff = true;
            particleSystem.Play();
            audioSource.Play();
            StartCoroutine(blast());
        }else if(Input.GetKeyUp("r")){
            reset();
        }

        if (blastOff){
            dist = speed*Time.deltaTime;
            totaldist += dist;
            if (totaldist < 900.0){ tunnel.transform.Translate(0.0f, dist, 0.0f); }
            else { tunnel.transform.Translate(0.0f, 0.01f, 0.0f); }
            speed += 0.2f*speed_multiplier;
        }
    }

    IEnumerator blast(){
        float toggleInterval = 0.1f; 
        float elapsedTime = 0.0f;
         while (elapsedTime < animate_time)
        {
            gforce = (0.5f + totaldist/maxdist);
            if (gforce > maxCurr){ gforce = maxCurr; }
            if (!scomms.VIB_ON){ 
                scomms.sendMessage("T " + (gforce/2).ToString(), 2); //L hip
                scomms.sendMessage("T " + (gforce/2).ToString(), 3); //R hip
                scomms.sendMessage("T " + gforce.ToString(), 6); //back
                scomms.sendMessage("T " + (gforce).ToString(), 7); //Thead
            }
            else if (!scomms.KINE_ON){ 
                float speedratio = totaldist/maxdist/currHighDiv;
                scomms.sendMessage("B " + rumble1.ToString() + " " + rumble2.ToString() + " " + minfreq, 3); //front

                scomms.sendMessage("B " + vib_currLow.ToString() + " " + (speedratio/2).ToString() + " " + minfreq, 2); //L hip
                scomms.sendMessage("B " + vib_currLow.ToString() + " " + (speedratio/2).ToString() + " " + minfreq, 3); //R hip
                scomms.sendMessage("B " + vib_currLow.ToString() + " " + (speedratio/2).ToString() + " " + minfreq, 6); //back
                scomms.sendMessage("B " + vib_currLow.ToString() + " " + (speedratio/2).ToString() + " " + minfreq, 7); //Thead
            }
            else {
                // scomms.sendMessage("B " + ((totaldist/maxdist)/2).ToString() + (gforce/2).ToString() + " " + minfreq, 2); //L hip
                // scomms.sendMessage("B " + ((totaldist/maxdist)/2).ToString() + (gforce/2).ToString() + " " + minfreq, 3); //R hip
                // scomms.sendMessage("B " + (totaldist/maxdist).ToString() + gforce.ToString() + " " + minfreq, 6); //back
                // scomms.sendMessage("B " + (totaldist/maxdist).ToString() + gforce.ToString() + " " + minfreq, 7); //Thead
                scomms.sendMessage("B " + rumble1.ToString() + " " + rumble2.ToString() + " " + minfreq, 3); //front
                
                scomms.sendMessage("B " + (gforce/2).ToString() + " " + (gforce/2 + currAdd).ToString() + " " +  minfreq, 2); //L hip
                scomms.sendMessage("B " + (gforce/2).ToString() + " " + (gforce/2 + currAdd).ToString() + " " + minfreq, 3); //R hip
                scomms.sendMessage("B " + gforce.ToString() + " " + (gforce + currAdd).ToString() + " " + minfreq, 6); //back
                scomms.sendMessage("B " + (gforce/2).ToString() + " " + ((gforce + currAdd)/2).ToString() + " " + minfreq, 7); //Thead
            }
            
            elapsedTime += toggleInterval;
            yield return new WaitForSeconds(toggleInterval);
            
        }
        particleSystem.Pause();
        audioSource.Stop();
        scomms.off();
        blastOff = false;
    }
    
    void reset(){
        scomms.reset();
        speed = 0f;
        this.transform.position = new Vector3(this.transform.position.x, headcenter.transform.position.y-offsetY, this.transform.position.z);
        //tunnel.transform.position = new Vector3(tunnel.transform.position.x, headcenter.transform.position.y-0.4f, tunnel.transform.position.z);
        totaldist = 0.0f;
        particleSystem.Stop();
        tunnel.transform.position = new Vector3(origTunnelPos.x, headcenter.transform.position.y-offsetY, origTunnelPos.z);
    }
}
