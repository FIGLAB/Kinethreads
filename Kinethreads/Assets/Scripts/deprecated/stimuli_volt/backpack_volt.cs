using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class backpack_volt : MonoBehaviour
{
    public multicomms scomms;
    private float current = 0.0f;
    public int motorIdx = 6;
    public TMP_Text tex;
    public GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("r")){
            current = 0.0f;
            tex.text = current.ToString("0.00");
            scomms.sendMessage(current, motorIdx);
        }
        
    }

    void OnTriggerEnter (Collider other){
        ball.GetComponent<Renderer>().material.color = Color.red;
        if (other.tag == "bagitem"){ current += 0.3f; }
        else if (other.tag == "bagitem_heavy"){ current += 0.5f; }
        if (current > 1.2f) { current = 1.2f; }
        tex.text = current.ToString("0.00");
        scomms.sendMessage(current, motorIdx);
    }

    void OnTriggerExit (Collider other){
        ball.GetComponent<Renderer>().material.color = Color.black;
    }
}
