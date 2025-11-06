using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion_volt : MonoBehaviour
{
    public multicomms scomms;
    public int motorIdx = 6;
    public ParticleSystem explodeAnim;
    public float explodeTime = 0.2f;
    public float current = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("s")){
            explodeAnim.Play();
            StartCoroutine(explode());
        }     
    }

    IEnumerator explode(){
        float elapsedTime = 0.0f;
        while (elapsedTime < explodeTime){
            foreach (int a in scomms.openPorts){
                scomms.sendMessage(current, a);
            }
            elapsedTime += explodeTime;
            yield return new WaitForSeconds(explodeTime);
        }
        scomms.off();
    }
}
