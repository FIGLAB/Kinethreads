using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class box_target : MonoBehaviour
{
    public comms_velo scomms;
    public TMP_Text tex;
    public GameObject leftGlove;
    public GameObject rightGlove;

    string[] target_strings = {"SwingL", "SwingR", "LHead", "RHead", "LHand", "RHand", "Front", "Head", "fill"};
    public GameObject[] targets;
    Transform[] tarPos;
    Transform currTar;
    public int currIdx = -1;
    GameObject currGlove;

    public float moveSpeed = 5f;   // Movement speed
    public float rotationSpeed = 10f; // Rotation speed
    public float impactTime = 0.2f;
    public float maxCurr = 1.2f;
    public float rumble1 = 0.0f;
    public float rumble2 = 0.3f;
    public float frequency = 50f;
    bool punching = false;

    Vector3 initLeftPos;
    Vector3 initRightPos;

    void Start()
    {
        initLeftPos = leftGlove.transform.position;
        initRightPos = rightGlove.transform.position;
        tarPos = new Transform[9];
        for (int i = 0; i < 9; i++){
            tarPos[i] = targets[i].transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("q")){           // l head
            tex.text = "Left Hook";
            currIdx = 0;
            currTar = tarPos[currIdx];
            currGlove = leftGlove;
            punching = true;
        }else if (Input.GetKeyUp("w")){     // head
            tex.text = "Head Jab";
            currIdx = 7;
            currTar = tarPos[currIdx];
            currGlove = rightGlove;
            punching = true;
        }else if (Input.GetKeyUp("e")){     // r head
            tex.text = "Right Hook";
            currIdx = 1;
            currTar = tarPos[currIdx];
            currGlove = rightGlove;
            punching = true;
        }else if (Input.GetKeyUp("a")){     // l hand
            tex.text = "Left Hand";
            currIdx = 4;
            currTar = tarPos[currIdx];
            currGlove = leftGlove;
            punching = true;
        }else if (Input.GetKeyUp("s")){     // back
            tex.text = "Chest Jab";
            currIdx = 6;
            currTar = tarPos[currIdx];
            currGlove = rightGlove;
            punching = true;
        }else if (Input.GetKeyUp("d")){     // r hand
            tex.text = "Right Hand";
            currIdx = 5;
            currTar = tarPos[currIdx];
            currGlove = rightGlove;
            punching = true;
        }

        if (punching){ punch(); }
        
    }

    void punch(){
        // Rotate towards the target smoothly
        Vector3 direction = (currTar.position - currGlove.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        currGlove.transform.rotation = Quaternion.Slerp(currGlove.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // Move towards the target
        currGlove.transform.position = Vector3.MoveTowards(currGlove.transform.position, currTar.position, moveSpeed * Time.deltaTime);

        // Stop when close enough and return
        if (Vector3.Distance(currGlove.transform.position, currTar.position) < 0.1f)
        {
            if (currIdx == 0 || currIdx == 1){
                currIdx += 2;
                currTar = tarPos[currIdx];
            }else{
                punching = false;
                StartCoroutine(impact());
            }
        }
    }

    IEnumerator impact(){
        float elapsedTime = 0.0f;
        if(!scomms.VIB_ON){
            if (currIdx == 2){ scomms.sendMessage("T " + (-maxCurr).ToString(), 8);
            } else if ( currIdx == 3 ){ scomms.sendMessage("T " + (maxCurr).ToString(), 8);
            }else if (currIdx == 6) { scomms.sendMessage("T " + maxCurr.ToString(), 9); }
            else { scomms.sendMessage("T " + maxCurr.ToString(), currIdx); }
            impactTime = 0.2f;
        }else if (!scomms.KINE_ON){
            if (currIdx == 2 || currIdx == 3){ scomms.sendMessage("B " + rumble1.ToString() + " " + rumble2.ToString() + " " + frequency, 7);}
            else{ scomms.sendMessage("B " + rumble1.ToString() + " " + rumble2.ToString() + " " + frequency, currIdx); }
            impactTime = 0.4f;
        }else{
            Debug.Log(currIdx);
            if (currIdx == 2){ scomms.sendMessage("B " + (maxCurr).ToString() + " " + (maxCurr+rumble2).ToString() + " " + frequency, 8);
            } else if ( currIdx == 3 ){ scomms.sendMessage("B " + (maxCurr).ToString() + " " + (maxCurr-rumble2).ToString() + " " + frequency, 10);
            } else if ( currIdx == 6) { scomms.sendMessage("B " + (maxCurr).ToString() + " " + (maxCurr-rumble2).ToString() + " " + frequency, 9); }
            else { scomms.sendMessage("B " + (maxCurr).ToString() + " " + (maxCurr-rumble2).ToString() + " " + frequency, currIdx); 
            Debug.Log("why here");}
            impactTime = 0.2f;
        }
        while (elapsedTime < impactTime){
            elapsedTime += impactTime;
            yield return new WaitForSeconds(impactTime);
        }
        // if (currIdx == 2){
        //     scomms.sendMessage("T 0.2", 8); 
        // } else if ( currIdx == 3 ){
        //     scomms.sendMessage("T -0.2", 8);
        // }
        scomms.off(7);
        scomms.off(8);
        scomms.off(9);
        scomms.off(10);
        scomms.noslack(currIdx);
        returnGlove();
    }

    void returnGlove(){
        leftGlove.transform.position = initLeftPos;
        rightGlove.transform.position = initRightPos;
        currIdx = -1;
    }
}
