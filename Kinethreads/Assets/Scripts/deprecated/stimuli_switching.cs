using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stimuli_switching : MonoBehaviour
{
    public GameObject[] stimuli;
    public GameObject rightHandvisual;
    public GameObject OVRcamerarig;
    Vector3 origPos;
    Quaternion origRot; 

    // Start is called before the first frame update
    void Start()
    {
        origPos = OVRcamerarig.transform.position;
        origRot = OVRcamerarig.transform.rotation;
        reset();
        stimuli[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("1")){           // object weight
            reset();
            stimuli[0].SetActive(true);
        }else if (Input.GetKeyUp("2")){     // backpack
            reset();
            stimuli[1].SetActive(true);
        }else if (Input.GetKeyUp("3")){     // high gravity
            reset();
            stimuli[2].SetActive(true);
        }else if (Input.GetKeyUp("4")){     // g force
            reset();
            stimuli[3].SetActive(true);
        }else if (Input.GetKeyUp("5")){     // slingshot
            reset();
            stimuli[4].SetActive(true);
        }else if (Input.GetKeyUp("6")){     // handshake
            reset();
            rightHandvisual.SetActive(false);
            stimuli[5].SetActive(true);
        }else if (Input.GetKeyUp("7")){     // train turbulence
            reset();
            stimuli[6].SetActive(true);
        }else if (Input.GetKeyUp("8")){     // ship rocking
            reset();
            stimuli[7].SetActive(true);
        }else if (Input.GetKeyUp("9")){     // kick ball
            reset();
            stimuli[8].SetActive(true);
        }else if (Input.GetKeyUp("0")){     // head punch
            reset();
            stimuli[9].SetActive(true);
        }else if (Input.GetKeyUp("p")){     // tennis
            reset();
            stimuli[10].SetActive(true);
        }
    }

    void reset(){
        foreach (GameObject g in stimuli){
            g.SetActive(false);
        }
        rightHandvisual.SetActive(true);
        OVRcamerarig.transform.position = origPos;
        OVRcamerarig.transform.rotation = origRot;
    }
}
