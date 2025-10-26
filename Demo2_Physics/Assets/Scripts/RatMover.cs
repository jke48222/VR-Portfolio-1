using UnityEngine;

// Moves the rat forward with optional looping or capture stop
public class RatMover : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Speed of the rat in meters per second.")]
    public float moveSpeed = 2f;

    [Tooltip("Maximum travel distance before looping or stopping.")]
    public float maxDistance = 10f;

    [Tooltip("If true, the rat loops back to its start position after reaching max distance.")]
    public bool loop = false;

    [Header("Optional Components")]
    public Rigidbody rb;  // Used for physics-based movement

    Vector3 startPos;
    bool isCaptured;

    void Start()
    {
        startPos = transform.position;

        // Auto-detect Rigidbody if not assigned
        if (rb == null)
            rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isCaptured) return;

        // Move using Rigidbody or transform
        Vector3 direction = transform.forward;
        if (rb && !rb.isKinematic)
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        else
            transform.position += direction * moveSpeed * Time.deltaTime;

        // Check distance traveled
        float traveled = Vector3.Distance(startPos, transform.position);
        if (traveled >= maxDistance)
        {
            if (loop) transform.position = startPos;
            else enabled = false;
        }
    }

    // Stops movement when the rat is captured
    public void Capture()
    {
        isCaptured = true;

        if (rb)
        {
            rb.isKinematic = true;
            rb.linearVelocity = Vector3.zero;
        }

        // Play capture animation if assigned
        GetComponent<Animator>()?.SetTrigger("Captured");
    }
}
