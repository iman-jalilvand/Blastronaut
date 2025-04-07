using UnityEngine;

public class SpinNTilt : MonoBehaviour
{
    public float speed; // Base speed of rotation
    public float tiltVariationSpeed = 0.1f; // Speed at which the tilt angle varies
    public float tiltAmplitude = 5f; // Maximum tilt angle in degrees
    private float tiltAngle = 0f; // Current tilt angle
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    // Update is called once per frame
    void Update()
    {
        // Increment tilt angle dynamically
        tiltAngle = Mathf.Sin(Time.time * tiltVariationSpeed) * tiltAmplitude;

        // Apply rotation with varying tilt
        transform.Rotate(new Vector3(tiltAngle, 1, 0), speed * Time.deltaTime);
        
        //transform.Rotate(Vector3.up, speed * Time.deltaTime);

    }
}
