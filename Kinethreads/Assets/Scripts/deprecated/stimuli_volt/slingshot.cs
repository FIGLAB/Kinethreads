using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class slingshot : MonoBehaviour
{
    public GameObject pellet;
    public GameObject boneEnd;
    public GameObject origin;
    Vector3 origBone;
    Vector3 origPos;
    public slingshot_pellet sp;
    public Rigidbody pelletRB;
    Vector3 throwForce = new Vector3(0, 20, 20);

    // Start is called before the first frame update
    void Start()
    {
        origPos = pellet.transform.position;
        origBone = boneEnd.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (!sp.grabbed && !sp.released){
            pellet.transform.position = boneEnd.transform.position;
        }else if (sp.grabbed && !sp.released){
            boneEnd.transform.position = pellet.transform.position;
            Debug.Log("************************************************************");
        }else if (sp.released){
            pelletRB.AddForce(origin.transform.forward * 5f, ForceMode.Impulse);
            boneEnd.transform.localPosition = origBone;
            // pelletRB.AddForce(throwForce, ForceMode.Impulse);
        }

        if (Input.GetKeyUp("r")){
            reset();
        }
        
    }

    public void reset(){
        sp.grabbed = false;
        sp.released = false;
    }

}
