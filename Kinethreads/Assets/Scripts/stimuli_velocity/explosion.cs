using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    public comms_velo scomms;
    public ParticleSystem fuse;
    public GameObject dynastick;
    public ParticleSystem explodeAnim;
    public AudioSource explodeSound;
    public float explodeTime = 0.5f;
    public float maxCurr = 1.2f;

    float currTime = 0.0f;
    bool fuseDown = false;
    float fusetime = 2.0f;
    bool exploding;
    float perl = 0.0f;
    float elapsedExplode = 0.0f;
    public float currCurr = 0.0f;

    public float rumble1 = -0.2f;
    public float rumble2 = 0.3f;
    public float frequency = 100f;

    int[] exIdxs = {2, 3, 4, 5, 6, 7, 9};

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("s")){
            currTime = 0.0f;
            fuseDown = true;
            fuse.Play();
        }     

        if (fuseDown){
            currTime += Time.deltaTime;
            if (currTime > fusetime){
                fuseDown = false;
                fuse.Stop();
                dynastick.SetActive(false);
                explodeAnim.Play();
                explodeSound.Play();
                exploding = true;
                elapsedExplode = 0.0f;
                if (!scomms.VIB_ON && scomms.KINE_ON){ 
                    foreach (int a in exIdxs){
                        scomms.sendMessage("T " + (maxCurr).ToString(), a);
                    }
                }
            }
        }
        else if (exploding){
            if (elapsedExplode < explodeTime){
                perl = Mathf.PerlinNoise1D(elapsedExplode*100f);
                currCurr = 0.3f +  perl* maxCurr;
                if (currCurr > maxCurr) { currCurr = maxCurr; }
                foreach (int a in exIdxs){
                    if (scomms.VIB_ON && !scomms.KINE_ON){
                        scomms.sendMessage("B " + (perl*rumble1).ToString() + " " + (perl*rumble2).ToString() + " " + frequency, a);
                    }else if (scomms.VIB_ON && scomms.KINE_ON) {
                        scomms.sendMessage("B " + currCurr.ToString() + " " + (currCurr-0.2).ToString() + " " + frequency, a);
                    }
                }
                elapsedExplode += Time.deltaTime;
            }else{
                exploding = false;
                explodeSound.Stop();
                explodeAnim.Stop();       
                scomms.off();
                dynastick.SetActive(true);
            }
        } 
    }
}
