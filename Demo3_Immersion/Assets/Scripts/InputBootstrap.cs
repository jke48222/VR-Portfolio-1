using UnityEngine;

// Initializes and manages the global XR input controls
public class InputBootstrap : MonoBehaviour
{
    public static XRControls Controls { get; private set; } // Shared input reference

    void Awake()
    {
        // Create and enable controls if not already initialized
        if (Controls == null)
            Controls = new XRControls();
        Controls.Enable();
    }

    void OnDestroy()
    {
        // Disable controls when object is destroyed
        Controls?.Disable();
    }
}
