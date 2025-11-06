using UnityEngine;

public class OscillateRotation : MonoBehaviour
{
    // Maximum rotation angle in degrees.
    public float rotationAmplitude = 10f;
    // Speed of the oscillation.
    public float rotationSpeed = 1f;
    // The axis around which the object will rotate. (Default is the Y-axis.)
    public Vector3 rotationAxis = Vector3.right;

    // Store the object's initial rotation.
    private Quaternion startRotation;

    void Start()
    {
        startRotation = transform.localRotation;
    }

    void Update()
    {
        // Calculate the oscillation angle using a sine wave.
        float angle = Mathf.Sin(Time.time * rotationSpeed) * rotationAmplitude;
        // Create an offset rotation based on the calculated angle and desired axis.
        Quaternion offsetRotation = Quaternion.AngleAxis(angle, rotationAxis);
        // Apply the offset to the original rotation.
        transform.localRotation = startRotation * offsetRotation;
    }
}
