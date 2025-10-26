using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton instance
    public static bool Exists => Instance != null;            // Quick check for existence

    [Header("Spawning")]
    public TargetSpawner spawner;                             // Reference to target spawner

    [Header("UI")]
    public TextMeshProUGUI scoreText;                         // Score display text

    int score;                                                 // Current score value

    void Awake()
    {
        // Enforce singleton (only one GameManager allowed)
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void RegisterHit()
    {
        // Increment score and update UI
        score++;
        if (scoreText != null)
            scoreText.text = "Score: " + score;

        // Optional: trigger global hit sound here
    }
}
