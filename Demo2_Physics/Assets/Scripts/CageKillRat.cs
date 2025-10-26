using UnityEngine;

// Handles rat removal when the cage collides with it
public class CageKillRat : MonoBehaviour
{
    [Tooltip("Tag of the rat object(s) to remove on impact.")]
    public string ratTag = "Rat";

    [Tooltip("If true, the rat vanishes instantly when hit.")]
    public bool destroyRat = true;

    [Tooltip("Optional particle or effect prefab to play when the rat is caught.")]
    public GameObject captureEffect;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(ratTag))
        {
            // Play capture effect if assigned
            if (captureEffect)
                Instantiate(captureEffect, collision.collider.transform.position, Quaternion.identity);

            // Remove or disable rat
            if (destroyRat)
                Destroy(collision.collider.gameObject);
            else
                collision.collider.gameObject.SetActive(false);

            Debug.Log("Rat captured and removed!");
        }
    }
}
