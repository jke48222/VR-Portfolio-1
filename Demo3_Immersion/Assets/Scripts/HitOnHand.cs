using UnityEngine;

// Detects collisions with player hands and handles score, sound, and destruction
[RequireComponent(typeof(SphereCollider))]
public class HitOnHand : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip hitClip;             // Sound played on hit
    [Range(0f, 1f)] public float volume = 0.8f; // Playback volume

    [Header("Settings")]
    public bool destroyOnHit = true;      // Whether to remove object after hit

    void OnTriggerEnter(Collider other)
    {
        // Check if collider belongs to a hand or saber
        if (other.name.Contains("Hand") || other.CompareTag("PlayerHand"))
        {
            // Add to score
            ScoreManager.Add(1);

            // Play sound at hit position
            if (hitClip)
                AudioSource.PlayClipAtPoint(hitClip, transform.position, volume);

            // Remove this object if enabled
            if (destroyOnHit)
                Destroy(gameObject);
        }
    }
}
