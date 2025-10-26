using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;

// Orchestrates the potion sequence puzzle, feedback, and finale
public class PuzzleManager : MonoBehaviour
{
    [Header("Order (exact sequence)")]
    public string[] correctOrder = { "Potion_3", "Potion_5", "Potion_2", "Potion_4", "Potion_1" };

    [Header("Scene Refs")]
    public Transform tableSpawnPoint; // Unused by snapshot restore but kept for compat
    [Tooltip("Collider on your CauldronTrigger (IsTrigger = true). Drag it here.")]
    public Collider cauldronCollider;
    public GameObject frog;                 // Active at start
    public GameObject prince;               // Inactive at start
    public ParticleSystem smokeFX;

    [Header("UI (Message Banner)")]
    public TextMeshProUGUI messageText;
    public CanvasGroup messageGroup;
    public float messageHoldSeconds = 1.5f;

    [Header("UI (Riddle Panel)")]
    [Tooltip("Root object for the riddle panel (e.g., Canvas2/Panel).")]
    public GameObject riddlePanelRoot;          // Drag your Panel here
    [Tooltip("Optional: CanvasGroup on the riddle panel.")]
    public CanvasGroup riddleGroup;
    [Tooltip("Show the riddle panel as soon as the scene enables.")]
    public bool showRiddleOnEnable = true;

    [Header("Player Control")]
    public MonoBehaviour locomotion;
    public CharacterController playerCC;

    [Header("Audio")]
    public AudioSource sfxSource;
    public AudioClip placeClip;
    public AudioClip failClip;
    public AudioClip successClip;
    public AudioClip transformClip;

    [Header("Reset Mode")]
    [Tooltip("If true, reload the current scene on failure; otherwise do snapshot restore.")]
    public bool reloadSceneOnReset = false;

    [Header("Debug")]
    public bool verboseLogs = true;

    // Runtime state
    readonly List<string> placed = new();
    bool locked;
    bool inRestartOrSuccess;
    bool finaleStarted;
    bool solved;
    Coroutine messageCR;

    // Snapshot model
    [System.Serializable]
    class PotionSnapshot
    {
        public string tag;
        public Transform originalParent;
        public Vector3 localPos;
        public Quaternion localRot;
        public Vector3 localScale;
        public GameObject template; // Hidden inactive clone captured at launch
    }

    readonly List<PotionSnapshot> snapshots = new();
    readonly HashSet<GameObject> livePotions = new();

    // ---------- Lifecycle ----------
    [System.Obsolete]
    void Awake()
    {
        if (prince) prince.SetActive(false);
        if (frog) frog.SetActive(true);
        ForceShowMessage(false);
        ForceShowRiddleIfConfigured();
        CaptureInitialPotions();
        Log($"[Awake] Captured {snapshots.Count} starting potions.");
    }

    void OnEnable()
    {
        SafeSetText("");
        ForceShowMessage(false);
        ForceShowRiddleIfConfigured();
        finaleStarted = false;
        inRestartOrSuccess = false;
        locked = false;
        solved = false;
        placed.Clear();
        livePotions.Clear();
        Log("[OnEnable] State cleared.");
    }

    // ---------- Capture potions at startup ----------
    [System.Obsolete]
    void CaptureInitialPotions()
    {
        var all = FindObjectsOfType<GameObject>(true);
        foreach (var go in all)
        {
            if (!go) continue;
            string tg = go.tag;
            if (string.IsNullOrEmpty(tg) || !tg.StartsWith("Potion_")) continue;

            // Inactive template preserves components/settings exactly
            var template = Instantiate(go);
            template.name = go.name + "_TEMPLATE";
            template.SetActive(false);

            snapshots.Add(new PotionSnapshot
            {
                tag = tg,
                originalParent = go.transform.parent,
                localPos = go.transform.localPosition,
                localRot = go.transform.localRotation,
                localScale = go.transform.localScale,
                template = template
            });

            livePotions.Add(go);
        }
    }

    // ---------- Cauldron events ----------
    public void OnPotionEntered(string potionTag, GameObject potionGO)
    {
        if (locked || inRestartOrSuccess || solved) { Log("[OnPotionEntered] Ignored."); return; }

        placed.Add(potionTag);

        // Consume the dropped potion
        if (potionGO)
        {
            if (livePotions.Contains(potionGO)) livePotions.Remove(potionGO);
            Destroy(potionGO);
        }

        PlaySfx(placeClip);
        Log($"[OnPotionEntered] {potionTag} (count={placed.Count})");
        CheckProgress();
    }

    public void OnPotionExited(string potionTag, GameObject potionGO)
    {
        // No-op (potions are consumed on enter)
    }

    // ---------- Progress / outcome ----------
    void CheckProgress()
    {
        int i = placed.Count - 1;
        if (i < 0) return;

        if (placed[i] != correctOrder[i])
        {
            Log($"[CheckProgress] WRONG at {i}: got {placed[i]} expected {correctOrder[i]}");
            StartCoroutine(RestartSequence());
            return;
        }

        if (placed.Count == correctOrder.Length)
        {
            Log("[CheckProgress] Sequence complete → SuccessSequence()");
            StartCoroutine(SuccessSequence());
        }
    }

    // ---------- WRONG → Reset ----------
    IEnumerator RestartSequence()
    {
        if (inRestartOrSuccess) yield break;
        inRestartOrSuccess = true;
        locked = true;

        ShowMessage("Try Again!", Color.red, messageHoldSeconds);
        PlaySfx(failClip);

        yield return new WaitForSeconds(0.5f);

        if (reloadSceneOnReset)
        {
            Log("[Reset] Reloading scene (hard reset) …");
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex);
            yield break;
        }

        // Soft reset via snapshots
        ToggleCauldronTrigger(false);

        // Destroy remaining live potions
        foreach (var go in new List<GameObject>(livePotions))
            if (go) Destroy(go);
        livePotions.Clear();

        // Clear state
        placed.Clear();
        solved = false;
        finaleStarted = false;

        // Restore all templates exactly
        int restored = 0;
        foreach (var s in snapshots)
        {
            if (!s.template) { LogWarning($"[Reset] Missing template for {s.tag}"); continue; }

            var clone = Instantiate(s.template, s.originalParent);
            clone.name = s.tag;
            clone.tag = s.tag;
            clone.transform.localPosition = s.localPos;
            clone.transform.localRotation = s.localRot;
            clone.transform.localScale = s.localScale;
            clone.SetActive(true);

            var rb = clone.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.isKinematic = false;
                rb.interpolation = RigidbodyInterpolation.Interpolate;
                rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            }

            livePotions.Add(clone);
            restored++;
        }

        Log($"[Reset] Restored {restored}/{snapshots.Count} potions to exact initial state.");

        // Re-enable trigger last
        ToggleCauldronTrigger(true);

        // Hide message after hold
        if (messageHoldSeconds > 0f) yield return new WaitForSeconds(messageHoldSeconds);
        ShowMessage("", Color.white, 0f);

        locked = false;
        inRestartOrSuccess = false;
        Log("[Reset] Soft reset complete.");
    }

    void ToggleCauldronTrigger(bool on)
    {
        if (cauldronCollider)
        {
            cauldronCollider.enabled = on;
            Log($"[Cauldron] Trigger {(on ? "ENABLED" : "DISABLED")}");
        }
        else
        {
            LogWarning("[Cauldron] No collider assigned! (Drag your CauldronTrigger’s collider into PuzzleManager.cauldronCollider)");
        }
    }

    // ---------- RIGHT → Finale ----------
    IEnumerator SuccessSequence()
    {
        if (inRestartOrSuccess) yield break;
        inRestartOrSuccess = true;
        locked = true;
        solved = true;

        ShowMessage("Congratulations!", Color.yellow, messageHoldSeconds);
        PlaySfx(successClip);

        yield return new WaitForSeconds(0.75f);

        UseFinaleDirect();
        inRestartOrSuccess = false;
    }

    void UseFinaleDirect()
    {
        if (finaleStarted) { Log("[UseFinaleDirect] Ignored (already started)."); return; }
        finaleStarted = true;

        if (locomotion) locomotion.enabled = false;
        if (playerCC) playerCC.enabled = false;

        StartCoroutine(FrogToPrince());
    }

    IEnumerator FrogToPrince()
    {
        PlaySfx(transformClip);

        if (frog)
        {
            PlaySmokeAt(frog.transform.position);
            yield return new WaitForSeconds(0.5f);
            SafeSetActive(frog, false);
        }

        yield return new WaitForSeconds(0.5f);

        if (prince)
        {
            PlaySmokeAt(prince.transform.position);
            SafeSetActive(prince, true);
        }

        if (playerCC) playerCC.enabled = true;
        if (locomotion) locomotion.enabled = true;

        ShowMessage("Congratulations!", Color.yellow, messageHoldSeconds);
        Log("[FrogToPrince] Finished. Controls restored.");
    }

    // ---------- UI helpers ----------
    void ShowMessage(string text, Color color, float holdSeconds)
    {
        SafeSetText(text);
        if (messageText) messageText.color = color;
        ForceShowMessage(!string.IsNullOrEmpty(text));

        if (messageCR != null) StopCoroutine(messageCR);
        if (holdSeconds > 0f && !string.IsNullOrEmpty(text))
            messageCR = StartCoroutine(HideMessageAfter(holdSeconds));
    }

    IEnumerator HideMessageAfter(float t)
    {
        yield return new WaitForSeconds(t);
        if (messageGroup)
        {
            float d = 0.25f;
            float a0 = messageGroup.alpha;
            float tt = 0f;
            while (tt < d)
            {
                tt += Time.deltaTime;
                messageGroup.alpha = Mathf.Lerp(a0, 0f, tt / d);
                yield return null;
            }
            messageGroup.alpha = 0f;
        }
        SafeSetText("");
    }

    void ForceShowMessage(bool visible)
    {
        if (!messageText) return;
        messageText.gameObject.SetActive(true);
        messageText.enabled = true;
        if (messageGroup) messageGroup.alpha = visible ? 1f : 0f;
        else messageText.enabled = visible;
    }

    void SafeSetText(string txt)
    {
        if (messageText) messageText.text = txt ?? "";
    }

    void SetRiddleVisible(bool visible)
    {
        if (riddlePanelRoot) riddlePanelRoot.SetActive(visible);
        if (riddleGroup)     riddleGroup.alpha = visible ? 1f : 0f;
    }

    void ForceShowRiddleIfConfigured()
    {
        if (showRiddleOnEnable) SetRiddleVisible(true);
    }

    // ---------- FX & Audio helpers ----------
    void PlaySmokeAt(Vector3 worldPos)
    {
        if (!smokeFX) return;
        smokeFX.transform.position = worldPos;
        smokeFX.Play();
    }

    void PlaySfx(AudioClip clip)
    {
        if (sfxSource && clip) sfxSource.PlayOneShot(clip);
    }

    static void SafeSetActive(GameObject go, bool active)
    {
        if (go && go.activeSelf != active) go.SetActive(active);
    }

    // ---------- Logging ----------
    void Log(string msg)
    {
        if (verboseLogs) Debug.Log($"[PuzzleManager] {msg}");
    }

    void LogWarning(string msg)
    {
        if (verboseLogs) Debug.LogWarning($"[PuzzleManager] {msg}");
    }
}
