using UnityEngine;
using UnityEngine.XR;

// Tracks the position and rotation of an XR hand device
public class TrackedHand : MonoBehaviour
{
    public XRNode node = XRNode.LeftHand;  // Use RightHand for the right controller

    void Update()
    {
        // Get the XR device for the assigned hand
        var dev = InputDevices.GetDeviceAtXRNode(node);

        // Update transform if position and rotation data are available
        if (dev.TryGetFeatureValue(CommonUsages.devicePosition, out var pos) &&
            dev.TryGetFeatureValue(CommonUsages.deviceRotation, out var rot))
        {
            transform.localPosition = pos;
            transform.localRotation = rot;
        }
    }
}
