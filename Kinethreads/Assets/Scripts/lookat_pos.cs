using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookat_pos : MonoBehaviour
{
    public Transform following;
    public Vector3 offset = new Vector3(0.0f, 0.0f, 0.0f);
    public bool followRot = true;
    public Vector3 rotOffset = new Vector3(0.0f, 0.0f, 0.0f);
    public bool rotDirection = false;
    public GameObject dirTarget;
    public Vector3 diroffset = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rotDirection) {
            direction = (following.position) - (dirTarget.transform.position + diroffset);
            this.transform.rotation = Quaternion.LookRotation(direction);
        }
        if (followRot) { this.transform.rotation = following.rotation * Quaternion.Euler(rotOffset); }
        this.transform.position = following.position + offset; 
    }
}
