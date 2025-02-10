using UnityEngine;
using Luna.Unity;

public class CameraFollow : MonoBehaviour
{
    [Header("Target to Follow")]
    public Transform target; // Player or object to follow

    [Header("Camera Offsets")]
    // Expose these values to Luna Playground
    [LunaPlaygroundField("Portrait Mode Offset", 0, "Camera Settings")]
    public Vector3 portraitOffset = new Vector3(4f, 5f, 0f); // Offset for portrait mode

    [LunaPlaygroundField("Landscape Mode Offset", 0, "Camera Settings")]
    public Vector3 landscapeOffset = new Vector3(3f, 4f, 0f); // Offset for landscape mode

    private Vector3 currentOffset; // Stores the active offset

    private void Start()
    {
        // Subscribe to screen orientation events
        ScreenOrientationManager.instance.OnLandscapeMode.AddListener(SetLandscapeOffset);
        ScreenOrientationManager.instance.OnPortraitMode.AddListener(SetPortraitOffset);

        // Set initial offset based on the current orientation
        CheckInitialOrientation();
    }

    private void LateUpdate()
    {
        if (target == null) return; // Prevent errors if no target is assigned

        // Instantly move the camera to follow the target with the active offset
        transform.position = target.position + currentOffset;
    }

    /// <summary>
    /// Sets the offset for portrait mode.
    /// </summary>
    private void SetPortraitOffset()
    {
        currentOffset = portraitOffset;
        Debug.Log("Camera switched to Portrait Mode!");
    }

    /// <summary>
    /// Sets the offset for landscape mode.
    /// </summary>
    private void SetLandscapeOffset()
    {
        currentOffset = landscapeOffset;
        Debug.Log("Camera switched to Landscape Mode!");
    }

    /// <summary>
    /// Checks the initial screen orientation at start and sets the correct offset.
    /// </summary>
    private void CheckInitialOrientation()
    {
        float screenRatio = (float)Screen.width / Screen.height;
        if (screenRatio >= 1)
            SetLandscapeOffset();
        else
            SetPortraitOffset();
    }
}
