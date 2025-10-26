using UnityEngine;
using TMPro;

// Handles global score tracking and UI updates
public class ScoreManager : MonoBehaviour
{
    public static int Score { get; private set; }            // Current score
    [SerializeField] TextMeshProUGUI scoreText;              // UI text reference

    void Awake()
    {
        // Initialize score and find text component if missing
        Score = 0;
        if (!scoreText) scoreText = GetComponentInChildren<TextMeshProUGUI>();
        UpdateText();
    }

    public static void Add(int amount)
    {
        // Add to score and refresh display
        Score += amount;
        instance?.UpdateText();
    }

    void UpdateText()
    {
        // Update the score label
        if (scoreText)
            scoreText.text = $"Score: {Score}";
    }

    // Lightweight singleton pattern for static access
    static ScoreManager instance;
    void OnEnable() { instance = this; }
    void OnDisable() { if (instance == this) instance = null; }
}
