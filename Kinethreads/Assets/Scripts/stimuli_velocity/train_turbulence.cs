using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class train_turbulence : MonoBehaviour
{
    public comms_velo scomms;
    public float animate_time = 20f;
    public float vibOffset = 0.015f;
    // Maximum rotation angle in degrees.
    public float rotationAmplitude = 0.9f;
    Vector3 rotationAxis = Vector3.right;
    private Quaternion startRotation;
    Vector3 origPos;

    public float speed = 50f;
    public float handDivider = 2f;
    public float scale1 = 3f;
    public float scale2 = 50f;
    public float slowscale = 2f;
    public float fastbase = 0.6f;
    public float fastMultiple = 0.35f;
    float maxCurr = 1.2f;
    float slow_p_curr;
    float fast_p_curr;
    float currCurr;
    float dist;
    float elapsedTime;
    bool isTurbulent = false;

    float r = 0.0f;
    public float rumble1 = 0f;
    public float rumble2 = 0.2f;
    public float frequency = 25f;

    public GameObject following;
    public GameObject Ocam;
    public GameObject train;
    public GameObject trainshell;
    public GameObject terrain;

    public float handmult = 3f;
    public float legmult = 3f;

    // Start is called before the first frame update
    void Start()
    {
        origPos = train.transform.position;
        startRotation = trainshell.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp("s")){
            isTurbulent = true; 
            if (scomms.VIB_ON && !scomms.KINE_ON){
                scomms.sendMessage("B " + Random.Range(rumble1, rumble2).ToString() + " " + Random.Range(rumble1, rumble2).ToString() + " " + frequency, 0);
                scomms.sendMessage("B " + Random.Range(rumble1, rumble2).ToString() + " " + Random.Range(rumble1, rumble2).ToString() + " " + frequency, 1);
                scomms.sendMessage("B " + Random.Range(rumble1, rumble2).ToString() + " " + Random.Range(rumble1, rumble2).ToString() + " " + frequency, 2);
                scomms.sendMessage("B " + Random.Range(rumble1, rumble2).ToString() + " " + Random.Range(rumble1, rumble2).ToString() + " " + frequency, 3);
                scomms.sendMessage("B " + Random.Range(rumble1, rumble2).ToString() + " " + Random.Range(rumble1, rumble2).ToString() + " " + frequency, 6);
                scomms.sendMessage("B " + Random.Range(rumble1, rumble2).ToString() + " " + Random.Range(rumble1, rumble2).ToString() + " " + frequency, 9);
                scomms.sendMessage("B " + Random.Range(rumble1, rumble2).ToString() + " " + Random.Range(rumble1, rumble2).ToString() + " " + frequency, 7);
            }
        }else if(Input.GetKeyUp("r")){
            reset();
        }

        if (isTurbulent){
            dist = speed*Time.deltaTime;
            elapsedTime += Time.deltaTime;
            train.transform.position += new Vector3(dist, 0.0f, 0.0f);
            trainshell.transform.position = new Vector3(train.transform.position.x, train.transform.position.y + Random.Range(-vibOffset, vibOffset), train.transform.position.z);
            slow_p_curr = Mathf.PerlinNoise1D(elapsedTime*scale1)*slowscale-slowscale/2;
            fast_p_curr = Random.Range(rumble1, rumble2);

            float angle = slow_p_curr * rotationAmplitude;
            // Create an offset rotation based on the calculated angle and desired axis.
            Quaternion offsetRotation = Quaternion.AngleAxis(angle, rotationAxis);
            // Apply the offset to the original rotation.
            trainshell.transform.localRotation = startRotation * offsetRotation;

            currCurr = Mathf.Min(maxCurr, Mathf.Abs(slow_p_curr)+fast_p_curr);
            if (scomms.KINE_ON&&scomms.VIB_ON){
                if (slow_p_curr < 0.0f){
                    scomms.sendMessage("T " + currCurr.ToString(), 2);
                    scomms.sendMessage("T " + fast_p_curr.ToString(), 3);
                }else{
                    scomms.sendMessage("T " + currCurr.ToString(), 3);
                    scomms.sendMessage("T " + fast_p_curr.ToString(), 2);
                }
                scomms.sendMessage("T " + (currCurr/handDivider).ToString(), 0);
                scomms.sendMessage("T " + (currCurr/handDivider).ToString(), 1);
                scomms.sendMessage("T " + currCurr.ToString(), 6);
                scomms.sendMessage("T " + currCurr.ToString(), 9);
                scomms.sendMessage("T " + (currCurr/handDivider).ToString(), 7);

            }
            else if (scomms.KINE_ON && !scomms.VIB_ON){
                if (slow_p_curr < 0.0f){
                    scomms.sendMessage("T " + (Mathf.Abs(slow_p_curr)).ToString(), 2);
                    scomms.sendMessage("T 0", 3);
                    // scomms.sendMessage("T " + (fast_p_curr).ToString(), 3);
                }else{
                    scomms.sendMessage("T " + (Mathf.Abs(slow_p_curr)).ToString(), 3);
                    scomms.sendMessage("T 0".ToString(), 2);
                    // scomms.sendMessage("T " + (fast_p_curr).ToString(), 2);
                }
                scomms.sendMessage("T " + (Mathf.Abs(slow_p_curr)/handDivider).ToString(), 0);
                scomms.sendMessage("T " + (Mathf.Abs(slow_p_curr)/handDivider).ToString(), 1);
                scomms.sendMessage("T " + (Mathf.Abs(slow_p_curr)/1.2).ToString(), 6);
                scomms.sendMessage("T " + (Mathf.Abs(slow_p_curr)/1.2).ToString(), 9);
                scomms.sendMessage("T " + (Mathf.Abs(slow_p_curr)/handDivider).ToString(), 7);
            }
        }  
        
        if (elapsedTime > animate_time){
            scomms.off();
            isTurbulent = false;
            elapsedTime = 0.0f;
        }
        
        Ocam.transform.rotation = following.transform.rotation;
        Ocam.transform.position = following.transform.position;
    }

    void reset(){
        scomms.reset();
        train.transform.position = origPos;
        trainshell.transform.localRotation = startRotation;
    }
}
