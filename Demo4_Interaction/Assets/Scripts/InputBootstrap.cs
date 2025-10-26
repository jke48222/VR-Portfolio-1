using UnityEngine;

// Initializes and manages the global XRControls instance
public class InputBootstrap : MonoBehaviour
{
    public static XRControls Controls { get; private set; } // Shared input reference

    void Awake()
    {
        // Create and enable controls if not already set
        if (Controls == null)
            Controls = new XRControls();
        Controls.Enable();
    }

    void OnDestroy()
    {
        // Disable controls when destroyed
        Controls?.Disable();
    }
}
