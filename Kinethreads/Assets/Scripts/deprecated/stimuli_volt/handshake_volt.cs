using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handshake_volt : MonoBehaviour
{
    public multicomms messenger;
    public int motorIdx = 1;
    public Animator animator;
    float noslack = 0.1f;
    public float[] currIntervals = {0.0f, 0.5f, 0.0f, 0.5f, 0.0f, 0.3f, 0.0f, 0.2f, 0.0f};
    public float[] timeIntervals = {1.5f, 0.6f, 0.4f, 0.5f, 0.4f, 0.2f, 0.1f, 0.1f, 1.0f};
    int tIdx = 0;
    float seconds = 0;
    public bool isShaking = false;
    public bool startAttach = false;
    
    public GameObject ball;
    Renderer ballcolor;
    bool balloff = false;

    // Start is called before the first frame update
    void Start()
    {   
        ballcolor = ball.GetComponent<Renderer>();
        ballcolor.material.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator shaking(){
        while (tIdx < timeIntervals.Length)
        {
            if (!balloff){
                balloff = true;
                ballcolor.material.color = Color.black;
            }else{
                balloff = false;
                ballcolor.material.color = Color.red;
            }
            messenger.sendMessage(currIntervals[tIdx], motorIdx);
            seconds = timeIntervals[tIdx];
            tIdx++;
            yield return new WaitForSeconds(seconds);
        }
        messenger.sendMessage(noslack, motorIdx);
        tIdx = 0;
        isShaking = false;
        balloff = false;
        ballcolor.material.color = Color.white;
    }

    void OnTriggerEnter(Collider other){
        
        if (other.tag == "other_hand" && isShaking){ startAttach = true; }
        else if (other.tag == "my_hand" && !isShaking) { 
            isShaking = true;
            animator.SetTrigger("StartShake");
            StartCoroutine(shaking());
        }

    }

    void OnTriggerExit(Collider other){
        if (other.tag == "other_hand" && isShaking){ startAttach = false; }
    }
}
