using UnityEngine;
using UnityEngine.XR;

// Handles smooth locomotion and turning using XR controller thumbsticks
[RequireComponent(typeof(CharacterController))]
public class XRSmoothLocomotion : MonoBehaviour
{
    public Transform head;              // Player camera
    public XRNode moveHand = XRNode.LeftHand;   // Movement hand
    public XRNode turnHand = XRNode.RightHand;  // Turning hand

    public float moveSpeed = 1.8f;      // Movement speed (m/s)
    public float gravity = -9.81f;      // Gravity strength
    public float turnSpeedDeg = 120f;   // Turn speed (degrees/sec)
    public float deadZone = 0.2f;       // Stick sensitivity threshold

    CharacterController cc;
    float fallingSpeed;

    void Awake()
    {
        // Cache CharacterController and default to main camera if not set
        cc = GetComponent<CharacterController>();
        if (head == null && Camera.main != null)
            head = Camera.main.transform;
    }

    void Update()
    {
        // Skip if character controller or rig is inactive
        if (cc == null || !cc.enabled || !cc.gameObject.activeInHierarchy)
            return;

        // Read thumbstick inputs
        Vector2 move = ReadAxis(moveHand, CommonUsages.primary2DAxis);
        Vector2 turn = ReadAxis(turnHand, CommonUsages.primary2DAxis); // May vary by device

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

        // Move character
        cc.Move(vel * Time.deltaTime);

        // Smooth turning around world Y-axis
        if (Mathf.Abs(turn.x) > deadZone)
        {
            float yaw = turn.x * turnSpeedDeg * Time.deltaTime;
            transform.Rotate(0f, yaw, 0f, Space.World);
        }
    }

    // Reads a Vector2 input from a specific XR controller
    static Vector2 ReadAxis(XRNode node, InputFeatureUsage<Vector2> usage)
    {
        var dev = InputDevices.GetDeviceAtXRNode(node);
        return dev.TryGetFeatureValue(usage, out var v) ? v : Vector2.zero;
    }
}
