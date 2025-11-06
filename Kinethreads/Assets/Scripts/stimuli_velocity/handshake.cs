using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handshake : MonoBehaviour
{
    public comms_velo scomms;
    public GameObject otherHand;
    public float amplitude = 1f; // Maximum height of the movement (up and down)
    public float frequency = 1f; // Speed of the movement (how fast it oscillates)
    public float ampMult = 5f; // Speed of the movement (how fast it oscillates)
    float origAmp;
    Vector3 handPos;
    public bool isShaking = false;
    public bool startAttach = false;
    public float shakeTime = 5.0f;
    float currTime;
    float newY;
    public GameObject ball;
    Renderer ballcolor;
    bool balloff = false;

    public float currValue = 0.0f;
    public float maxCurr = 1.3f;

    // Start is called before the first frame update
    void Start()
    {   
        ballcolor = ball.GetComponent<Renderer>();
        ballcolor.material.color = Color.white;
        handPos = otherHand.transform.position;
        origAmp = amplitude;
    }

    // Update is called once per frame
    void Update()
    {
        if (isShaking){
            currTime += Time.deltaTime;
            newY = Mathf.Sin(currTime * frequency) * amplitude;
            // Update the position, keeping the X and Z coordinates the same
            otherHand.transform.position = new Vector3(handPos.x, handPos.y + newY, handPos.z);

            currValue = Mathf.Min(maxCurr, Mathf.Max(0.0f, (0.0f - newY)*5));
            scomms.sendMessage("T " + currValue.ToString(), 1);
            if (currTime > shakeTime){
                isShaking = false;
                startAttach = false;
                otherHand.transform.position = handPos;
                scomms.off(1);
                amplitude = origAmp;
                this.GetComponent<Collider>().enabled = true;
            }
        }

        if (Input.GetKeyUp("s")){
            isShaking = true;
            currTime = 0.0f;
            StartCoroutine(SpeedUpAndSlowDown());
        }
    }

    void OnTriggerEnter(Collider other){
        if (other.tag == "my_hand" ) { 
            startAttach = true;
            isShaking = true;
            currTime = 0.0f;
            this.GetComponent<Collider>().enabled = false;
            StartCoroutine(SpeedUpAndSlowDown());
        }
    }

    IEnumerator SpeedUpAndSlowDown()
    {
        // Speed up for the first 2 seconds
        float elapsedTime = 0f;
        while (elapsedTime < (shakeTime/2f))
        {
            amplitude = Mathf.Lerp(origAmp, origAmp * ampMult, elapsedTime / (shakeTime/2f));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Now slow down for the next 2 seconds
        elapsedTime = 0f;
        while (elapsedTime < shakeTime/2f)
        {
            amplitude = Mathf.Lerp(origAmp * ampMult, origAmp, elapsedTime / (shakeTime/2f));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

}
