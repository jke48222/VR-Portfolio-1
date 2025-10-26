using UnityEngine;

public class SelfRotation : MonoBehaviour
{
    public float degreesPerSec = 15f; // Rotation speed

    void Update()
    {
        // Rotate around local Y-axis
        transform.Rotate(0, -degreesPerSec * Time.deltaTime, 0, Space.Self);
    }
}
