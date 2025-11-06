using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class set_stimuli : MonoBehaviour
{
    public comms_velo scomms;
    public GameObject[] stimuli;
    public GameObject rightHandvisual;
    public GameObject OVRcamerarig;
    public GameObject tex;
    public Material defaultSky;
    public Material stars;
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
        if (Input.GetKeyUp("1")){           // backpack
            reset();
            // tex.SetActive(true);
            stimuli[0].SetActive(true);
            scomms.noslack(0);
            scomms.noslack(1);
        }else if (Input.GetKeyUp("2")){     // high gravity
            reset();
            // tex.SetActive(true);
            // tex.GetComponent<TMP_Text>().text = "Normal Gravity";
            stimuli[1].SetActive(true);
        }else if (Input.GetKeyUp("3")){     // fireman_hose
            reset();
            stimuli[2].SetActive(true);
            scomms.noslack(5);
        }else if (Input.GetKeyUp("4")){     // rocket blast off
            reset();
            RenderSettings.skybox = stars;
            stimuli[3].SetActive(true);
        }else if (Input.GetKeyUp("5")){     // driving
            reset();
            stimuli[4].SetActive(true);
        }else if (Input.GetKeyUp("6")){     // squeeze 
            reset();
            stimuli[5].SetActive(true);
            scomms.noslack();
        }else if (Input.GetKeyUp("7")){     // impacts
            reset();
            stimuli[6].SetActive(true);
            scomms.noslack(4);
            scomms.noslack(5);
        }else if (Input.GetKeyUp("8")){    // explosion
            reset();
            stimuli[7].SetActive(true);
            scomms.noslack();
        }else if (Input.GetKeyUp("9")){    // waterfall
            reset();
            stimuli[8].SetActive(true);
        }else if (Input.GetKeyUp("0")){     // train turbulence
            reset();
            stimuli[9].SetActive(true);
        }else if (Input.GetKeyUp("p")){      // handshake
            reset();
            rightHandvisual.SetActive(false);
            stimuli[10].SetActive(true);        //SURVEY
        }else if(Input.GetKeyUp("o")){
            reset();
            stimuli[11].SetActive(true);
        }
        
        // else if (Input.GetKeyUp("o")){     // ground texture
        //     reset();
        //     stimuli[11].SetActive(true);
        //     scomms.sendMessage("G 5", 2);
        //     scomms.sendMessage("G 5", 3);
        // }else if (Input.GetKeyUp("i")){    //tennis
        //     reset();
        //     stimuli[12].SetActive(true);
        // }else if (Input.GetKeyUp("u")){     // step box
        //     reset();
        //     stimuli[13].SetActive(true);
        // }
    }

    void reset(){
        scomms.reset();
        foreach (GameObject g in stimuli){
            g.SetActive(false);
        }
        tex.SetActive(false);
        rightHandvisual.SetActive(true);
        RenderSettings.skybox = defaultSky;
        OVRcamerarig.transform.position = origPos;
        OVRcamerarig.transform.rotation = origRot;
    }
}
