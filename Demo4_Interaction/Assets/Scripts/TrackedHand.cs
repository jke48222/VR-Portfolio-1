using UnityEngine;
using UnityEngine.XR;

// Tracks the XR hand/controller position and rotation
public class TrackedHand : MonoBehaviour
{
    public XRNode node = XRNode.LeftHand;  // Use RightHand for the right controller

    void Update()
    {
        var dev = InputDevices.GetDeviceAtXRNode(node);

        // Update transform if tracking data is available
        if (dev.TryGetFeatureValue(CommonUsages.devicePosition, out var pos) &&
            dev.TryGetFeatureValue(CommonUsages.deviceRotation, out var rot))
        {
            transform.localPosition = pos;
            transform.localRotation = rot;
        }
    }
}
