using UnityEngine;
using UnityEngine.XR;

[DisallowMultipleComponent]
// Handles basic XR grabbing and releasing of potion objects
public class SimpleGrab : MonoBehaviour
{
    public XRNode node = XRNode.LeftHand;      // Controller to read from
    public float grabRadius = 0.12f;           // Detection radius
    public float holdDistance = 0.15f;         // Offset from hand when held

    [Header("Audio")]
    public AudioClip grabClip;
    public AudioClip releaseClip;
    public float pitchJitter = 0.05f;          // Subtle pitch variation
    public float sfxCooldown = 0.05f;          // Prevent rapid spam

    Transform held;
    float lastSfxTime;
    AudioSource oneShotSource;                 // Local 2D audio source

    void Awake()
    {
        // Create lightweight audio source for one-shots
        oneShotSource = gameObject.AddComponent<AudioSource>();
        oneShotSource.playOnAwake = false;
        oneShotSource.spatialBlend = 0f;
    }

    void Update()
    {
        var dev = InputDevices.GetDeviceAtXRNode(node);
        dev.TryGetFeatureValue(CommonUsages.gripButton, out bool grip);

        // --- Grab ---
        if (grip && held == null)
        {
            var hits = Physics.OverlapSphere(transform.position, grabRadius);
            foreach (var h in hits)
            {
                string tg = h.tag;
                if (!string.IsNullOrEmpty(tg) && (tg.StartsWith("Potion_") || tg == "GoldenPotion"))
                {
                    held = h.transform;
                    held.SetParent(transform);
                    held.localPosition = Vector3.forward * holdDistance;

                    var rb = held.GetComponent<Rigidbody>();
                    if (rb) rb.isKinematic = true;

                    PlayOneShotSafe(grabClip);
                    break;
                }
            }
        }
        // --- Release ---
        else if (!grip && held != null)
        {
            var rb = held.GetComponent<Rigidbody>();
            if (rb) rb.isKinematic = false;

            held.SetParent(null);
            held = null;

            PlayOneShotSafe(releaseClip);
        }
    }

    // Plays a sound with cooldown and pitch jitter
    void PlayOneShotSafe(AudioClip clip)
    {
        if (!clip) return;
        if (Time.time - lastSfxTime < sfxCooldown) return;

        oneShotSource.pitch = 1f + Random.Range(-pitchJitter, pitchJitter);
        oneShotSource.PlayOneShot(clip);
        lastSfxTime = Time.time;
    }
}
