using UnityEngine;

public class MouseOrbitCamera : MonoBehaviour
{
    public Transform target;   // Object for the camera to orbit around
    public float dist = 18f;   // Distance from target
    float x, y;                // Rotation angles

    void LateUpdate()
    {
        // Update rotation based on mouse movement
        x += Input.GetAxis("Mouse X") * 2f;
        y = Mathf.Clamp(y - Input.GetAxis("Mouse Y") * 2f, -80, 80);

        // Set camera position and orientation
        transform.position = target.position + Quaternion.Euler(y, x, 0) * Vector3.back * dist;
        transform.LookAt(target);
    }
}
