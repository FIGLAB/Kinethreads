using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class return_pos : MonoBehaviour
{
    public GameObject[] objects;
    private Vector3[] transforms;
    private Rigidbody[] rbs;

    // Start is called before the first frame update
    void Start()
    {
        transforms = new Vector3[objects.Length];
        rbs = new Rigidbody[objects.Length];
        for (int i=0; i<objects.Length; i++){
            transforms[i] = objects[i].transform.position + new Vector3(0.0f, 0.1f, 0.0f);
            rbs[i] = objects[i].GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("r")){
            reset();
        }
        
    }

    void reset(){
        for (int i=0; i<objects.Length; i++){
            rbs[i].velocity = Vector3.zero;
            rbs[i].angularVelocity = Vector3.zero;
            objects[i].transform.position = transforms[i];
            rbs[i].velocity = Vector3.zero;
            rbs[i].angularVelocity = Vector3.zero;
        }
    }
}
