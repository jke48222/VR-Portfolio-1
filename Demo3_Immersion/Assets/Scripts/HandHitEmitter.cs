using UnityEngine;
using System;

// Ensures a SphereCollider exists for hand collision detection
[RequireComponent(typeof(SphereCollider))]
public class HandHitEmitter : MonoBehaviour
{
    [Range(0, 1)] public float hapticAmp = 0.4f;  // Haptic intensity
    public float hapticDur = 0.06f;               // Haptic duration
    public string handTag = "HandHit";            // Tag assigned to this hand collider

    void Reset()
    {
        // Configure collider as trigger and apply default size/tag
        var col = GetComponent<SphereCollider>();
        col.isTrigger = true;
        if (col.radius < 0.07f) col.radius = 0.07f;
        gameObject.tag = handTag;
    }

    // Triggers a haptic pulse if supported by attached components
    public void Pulse()
    {
        // Try Starter Assets' HapticImpulsePlayer (if present)
        var hip = GetComponent("HapticImpulsePlayer");
        if (hip != null)
        {
            var m = hip.GetType().GetMethod("SendHapticImpulse", new Type[] { typeof(float), typeof(float) });
            if (m != null)
            {
                m.Invoke(hip, new object[] { hapticAmp, hapticDur });
                return;
            }
        }

        // Try any attached component with SendHapticImpulse(float, float)
        var behaviours = GetComponents<MonoBehaviour>();
        foreach (var b in behaviours)
        {
            if (b == null) continue;
            var m = b.GetType().GetMethod("SendHapticImpulse", new Type[] { typeof(float), typeof(float) });
            if (m != null)
            {
                m.Invoke(b, new object[] { hapticAmp, hapticDur });
                return;
            }
        }

        // No haptic-compatible component found — safely do nothing
    }
}
