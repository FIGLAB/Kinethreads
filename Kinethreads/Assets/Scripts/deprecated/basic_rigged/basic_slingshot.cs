using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basic_slingshot : MonoBehaviour
{
    public multicomms messenger;
    public GameObject Lwrist;
    public GameObject Rwrist;
    public float current = 0.1f;
    bool tension = false;

    // Start is called before the first frame update
    void Start()
    {
        messenger.sendMessage(current, 7);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp("s")){
            messenger.sendMessage(0.5f, 7);
            tension = true;
        }
        if (tension){
            if (Vector3.Distance(Lwrist.transform.position, Rwrist.transform.position) > 0.5f){
                tension = false;
                messenger.sendMessage(0.0f, 7);
            }
        }
        
    }

    void reset(){
        messenger.sendMessage(0.0f, 7);
    }
}
