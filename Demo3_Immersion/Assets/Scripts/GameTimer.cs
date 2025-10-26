using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public float duration = 60f;           // Total time in seconds
    public TextMeshProUGUI scoreText;      // Optional: reference to score text
    public TextMeshProUGUI timerText;      // Timer display text
    public TargetSpawner spawner;          // Spawner to disable when time ends

    float t;                               // Remaining time

    void OnEnable() { t = duration; }      // Reset timer when enabled

    void Update()
    {
        // Countdown and update display
        t -= Time.deltaTime;
        if (timerText)
            timerText.text = "Time: " + Mathf.CeilToInt(Mathf.Max(0, t)).ToString();

        // Stop spawning and disable timer when time runs out
        if (t <= 0f)
        {
            if (spawner) spawner.enabled = false;
            enabled = false;
        }
    }
}
