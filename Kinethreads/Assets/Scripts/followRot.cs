using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followRot : MonoBehaviour
{
    public Transform following;
    public Vector3 offset = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 rotOffset = new Vector3(0.0f, 0.0f, 0.0f);

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = following.rotation * Quaternion.Euler(rotOffset);
        this.transform.position = following.position + offset; 
    }
}
