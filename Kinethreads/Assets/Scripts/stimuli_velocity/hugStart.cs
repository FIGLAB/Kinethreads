using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hugStart : MonoBehaviour
{
    public comms_velo scomms;
    // public Animator animator;
    public GameObject rope;
    public Animator animator;
    float currtime = 0.0f;
    public float squeezeTime = 5.0f;
    bool tentSqueeze;
    float rotationSpeed = 5.0f;
    float sforce;
    float scaleSpeed = 1.0f;
    float scaleNo = 2f;

    // Start is called before the first frame update
    void Start()
    {
        rope.transform.localScale = new Vector3(2f, 2f, 2f);
        animator.StopPlayback();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp("s")){
            StartCoroutine(squeeze());
            animator.Play("pullingRope");
        }else if (Input.GetKeyUp("r")){
            rope.transform.localScale = new Vector3(2f, 2f, 2f);
            scomms.off();

        }
    }

    IEnumerator squeeze(){
        float toggleInterval = 0.1f; 
        float elapsedTime = 0.0f;
        while (elapsedTime < squeezeTime)
        {
            sforce = elapsedTime/squeezeTime;
            if (sforce > 1.0f){ sforce = 1.0f; }
            rope.transform.localScale = new Vector3(2f - sforce*1.2f, 2f - sforce*0.6f, 2f - sforce*1.2f);
            scomms.sendMessage("T " + (sforce).ToString(), 2); //left leg
            scomms.sendMessage("T " + (sforce).ToString(), 3); //right leg
            scomms.sendMessage("T " + (sforce).ToString(), 4); //left arm
            scomms.sendMessage("T " + (sforce).ToString(), 5); //right arm
            scomms.sendMessage("T " + (sforce).ToString(), 6); //back
            elapsedTime += toggleInterval;
            yield return new WaitForSeconds(toggleInterval);
            
        }
        // scomms.off();
        animator.Play("idle");
    }
}
