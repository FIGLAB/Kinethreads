using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followtrain : MonoBehaviour
{
    public Transform following;
    public Vector3 offset = new Vector3(0.0f, 0.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = following.rotation;
        this.transform.position = following.position + offset;
    }
}
