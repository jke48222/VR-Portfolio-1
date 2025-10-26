using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Collider))]
// Detects potion objects entering or exiting the cauldron and notifies the PuzzleManager
public class CauldronTrigger : MonoBehaviour
{
    [Header("Puzzle Hook")]
    public PuzzleManager puzzle;                 // Reference to the puzzle controller

    [Header("Detection")]
    [Tooltip("Only GameObjects whose tag starts with this prefix will be considered potions.")]
    public string potionTagPrefix = "Potion_";   // Tag prefix for identifying potions

    void Reset()
    {
        // Ensure collider is set as trigger
        var col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!puzzle) return;

        // Detect potions entering the cauldron
        string tag = other.tag;
        if (!string.IsNullOrEmpty(tag) && tag.StartsWith(potionTagPrefix))
        {
            Debug.Log($"[CauldronTrigger] Potion ENTER: {other.name} (tag={tag})");
            puzzle.OnPotionEntered(tag, other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!puzzle) return;

        // Detect potions leaving the cauldron
        string tag = other.tag;
        if (!string.IsNullOrEmpty(tag) && tag.StartsWith(potionTagPrefix))
        {
            Debug.Log($"[CauldronTrigger] Potion EXIT: {other.name} (tag={tag})");
            puzzle.OnPotionExited(tag, other.gameObject);
        }
    }
}
