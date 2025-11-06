using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class headpunch_volt : MonoBehaviour
{
    public multicomms messenger;
    public int motorIdx = 6;
    public Animator anim;
    public GameObject puncher;
    Quaternion origRot;
    float noslack = 0.1f;
    float[] currents = {0.1f, 1.2f};
    public float[] punchtimes = {0.7f, 0.1f};
    int punchIdx = 0;
    float waitsecs;

    public GameObject ball;
    Renderer ballcolor;
    bool balloff = false;

    // Start is called before the first frame update
    void Start()
    {
        ballcolor = ball.GetComponent<Renderer>();
        ballcolor.material.color = Color.white;
        // origRot = puncher.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("s")){
            anim.SetTrigger("StartPunch");
        }else if (Input.GetKeyUp("r")){
            anim.ResetTrigger("StartPunch");
        }
    }

    IEnumerator punching(){
        while (punchIdx < punchtimes.Length)
        {
            messenger.sendMessage(currents[punchIdx], motorIdx);
            if (punchIdx == 1){
                ballcolor.material.color = Color.red;
            }
            waitsecs = punchtimes[punchIdx];
            punchIdx++;
            yield return new WaitForSeconds(waitsecs);
        }
        messenger.sendMessage(0.0f, motorIdx);
        ballcolor.material.color = Color.white;
        punchIdx = 0;
        // puncher.transform.rotation = origRot;
    }

    void OnTriggerEnter(){
        anim.SetTrigger("StartPunch");
        StartCoroutine(punching());

    }

}
