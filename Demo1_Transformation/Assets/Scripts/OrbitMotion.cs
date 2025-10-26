using UnityEngine;

public class OrbitMotion : MonoBehaviour
{
    public float degreesPerSec = 5f; // Rotation speed

    void Update()
    {
        // Rotate around world Y-axis
        transform.Rotate(0, degreesPerSec * Time.deltaTime, 0, Space.World);
    }
}
