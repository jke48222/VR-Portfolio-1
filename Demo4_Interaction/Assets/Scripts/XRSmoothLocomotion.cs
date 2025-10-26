using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(CharacterController))]
// Handles smooth XR movement and turning using controller thumbsticks
public class XRSmoothLocomotion : MonoBehaviour
{
    public Transform head;                     // Player camera
    public XRNode moveHand = XRNode.LeftHand;  // Movement input hand
    public XRNode turnHand = XRNode.RightHand; // Turning input hand

    public float moveSpeed = 1.8f;             // Movement speed (m/s)
    public float gravity = -9.81f;
    public float turnSpeedDeg = 120f;          // Turn speed (deg/sec)
    public float deadZone = 0.2f;              // Stick input threshold

    CharacterController cc;
    float fallingSpeed;

    void Awake()
    {
        // Cache CharacterController and fallback to main camera if needed
        cc = GetComponent<CharacterController>();
        if (head == null && Camera.main != null)
            head = Camera.main.transform;
    }

    void Update()
    {
        // Skip if rig or controller is inactive
        if (cc == null || !cc.enabled || !cc.gameObject.activeInHierarchy)
            return;

        // Read controller input
        Vector2 move = ReadAxis(moveHand, CommonUsages.primary2DAxis);
        Vector2 turn = ReadAxis(turnHand, CommonUsages.primary2DAxis);

        // Head-relative movement
        Vector3 fwd = Vector3.ProjectOnPlane(head.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(head.right, Vector3.up).normalized;

        Vector3 vel = Vector3.zero;
        if (move.magnitude > deadZone)
            vel = (fwd * move.y + right * move.x) * moveSpeed;

        // Apply gravity
        if (cc.isGrounded) fallingSpeed = 0f;
        else fallingSpeed += gravity * Time.deltaTime;
        vel.y = fallingSpeed;

        cc.Move(vel * Time.deltaTime);

        // Smooth turn (yaw around world Y-axis)
        if (Mathf.Abs(turn.x) > deadZone)
        {
            float yaw = turn.x * turnSpeedDeg * Time.deltaTime;
            transform.Rotate(0f, yaw, 0f, Space.World);
        }
    }

    // Reads Vector2 input from the specified controller
    static Vector2 ReadAxis(XRNode node, InputFeatureUsage<Vector2> usage)
    {
        var dev = InputDevices.GetDeviceAtXRNode(node);
        return dev.TryGetFeatureValue(usage, out var v) ? v : Vector2.zero;
    }
}
