using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tennis_volt : MonoBehaviour
{

    public multicomms scomms;
    public float current = 1.2f;
    public int motorIdx = 1;
    bool bouncing = true;
    float bounceTime = 0.15f;

    public GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator bounce(){
        float elapsedTime = 0.0f;
        while (elapsedTime < bounceTime){
            scomms.sendMessage(current, motorIdx);
            elapsedTime += bounceTime;
            yield return new WaitForSeconds(bounceTime);
        }
        scomms.sendMessage(0.1f, motorIdx);
        ball.GetComponent<Renderer>().material.color = Color.black;
        bouncing = false;
    }

    // void OnTriggerEnter(){
    //     if (!bouncing){
    //         bouncing = true;
    //         StartCoroutine(bounce());
    //         ball.GetComponent<Renderer>().material.color = Color.red;
    //     }
    // }
    void OnTriggerEnter(){
        scomms.sendMessage(current, motorIdx);
    }
    void OnTriggerExit(){
        scomms.sendMessage(0.1f, motorIdx);
    }
}
