using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followhand : MonoBehaviour
{
    public handshake hs;
    public Transform following;
    public followpos f;
    public Vector3 offset = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 rotOffset = new Vector3(0.0f, 0.0f, 0.0f);


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hs.isShaking){
            if (hs.startAttach){
                f.enabled = false;
                this.transform.rotation = following.rotation * Quaternion.Euler(rotOffset);    
                this.transform.position = following.position + offset; 
            } 
        } else{
            // this.transform.position = new Vector3(5.0f, 5.0f, 5.0f);
            f.enabled = true;
        }

        
    }

    
}
