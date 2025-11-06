using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow_car : MonoBehaviour
{
    public GameObject Ocam;
    // Offset from the target position
    [SerializeField] Vector3 offset = -Vector3.forward;

    // Camera speeds
    [Range(0, 10)]
    [SerializeField] float lerpPositionMultiplier = 1f;
    [Range(0, 10)]		
    [SerializeField] float lerpRotationMultiplier = 1f;

    void Start () {
    }

    void FixedUpdate() {
        // Save transform localy
        Quaternion curRot = Ocam.transform.rotation;
        Vector3 tPos = transform.position + this.transform.TransformDirection(offset);

        // Look at the target
        Ocam.transform.LookAt(this.transform);

        // Keep the camera above the target y position
        if (tPos.y < this.transform.position.y) {
            tPos.y = this.transform.position.y;
        }

        // Set transform with lerp
        Ocam.transform.position = Vector3.Lerp(Ocam.transform.position, tPos, Time.fixedDeltaTime * lerpPositionMultiplier);
        // transform.rotation = Quaternion.Lerp(curRot, transform.rotation, Time.fixedDeltaTime * lerpRotationMultiplier);

        // Keep camera above the y:0.5f to prevent camera going underground
        if (Ocam.transform.position.y < 0.5f) {
            Ocam.transform.position = new Vector3(Ocam.transform.position.x , 0.5f, Ocam.transform.position.z);
        }
        
    }
}
