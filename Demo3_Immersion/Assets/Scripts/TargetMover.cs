using UnityEngine;

// Moves targets toward the player and handles misses or cleanup
public class TargetMover : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("Meters per second toward the player.")]
    public float speed = 2.5f;

    [Tooltip("When this close to the player, count as a miss and remove.")]
    public float stopDistance = 0.35f;

    [Tooltip("Tilt the target so its front faces the player.")]
    public bool facePlayer = true;

    [Header("Cleanup")]
    [Tooltip("Destroy if farther than this from its spawn point (safety).")]
    public float maxTravel = 30f;

    [Header("Scoring (optional)")]
    public bool countMisses = true;
    public int missPenalty = 1;

    Transform cam;          // Player camera
    Vector3 spawnPos;       // Original spawn position
    bool initialized;       // Camera found flag

    void OnEnable()
    {
        spawnPos = transform.position;
        InitializeCamera();
    }

    void Start()
    {
        if (!initialized) InitializeCamera();
    }

    void InitializeCamera()
    {
        // Locate main camera or XR rig camera
        cam = Camera.main ? Camera.main.transform : null;
        if (!cam)
        {
            var found = GameObject.FindGameObjectWithTag("MainCamera");
            if (found) cam = found.transform;
        }
        initialized = cam != null;
    }

    void Update()
    {
        if (!initialized) { InitializeCamera(); return; }
        if (!cam) return;

        // Direction to player
        Vector3 toCam = cam.position - transform.position;
        float dist = toCam.magnitude;

        // Face player for visibility
        if (facePlayer && dist > 0.001f)
            transform.forward = Vector3.Lerp(transform.forward, toCam.normalized, 0.25f);

        // Move toward player or count as miss when close enough
        float step = speed * Time.deltaTime;
        if (dist > stopDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, cam.position, step);
        }
        else
        {
            if (countMisses) ScoreManager.Add(-Mathf.Abs(missPenalty));
            Destroy(gameObject);
            return;
        }

        // Destroy if too far from spawn (safety)
        if (Vector3.Distance(spawnPos, transform.position) > maxTravel)
            Destroy(gameObject);
    }
}
