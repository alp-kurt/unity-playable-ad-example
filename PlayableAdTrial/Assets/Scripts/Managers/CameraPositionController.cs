using UnityEngine;
using DG.Tweening;

public class CameraPositionController : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Camera mainCamera;

    [Header("Fixed Positions")]
    [SerializeField] private Vector3 portraitPosition = new Vector3(0f, 0f, 0f);
    [SerializeField] private Vector3 landscapePosition = new Vector3(0f, 0f, 0f);

    [Header("Fixed Rotations")]
    [SerializeField] private Vector3 portraitRotation = new Vector3(0f, 0f, 0f);
    [SerializeField] private Vector3 landscapeRotation = new Vector3(0f, 0f, 0f);

    [Header("Transition Settings")]
    [SerializeField] private float transitionDuration = 0.5f;

    private void Start()
    {
        // Find main cam if not assigned
        if (mainCamera == null) mainCamera = Camera.main;

        // Subscribe to screen orientation events
        ScreenOrientationManager.instance.OnLandscapeMode.AddListener(SetLandscapeView);
        ScreenOrientationManager.instance.OnPortraitMode.AddListener(SetPortraitView);

        // Apply correct position at startup
        CheckInitialPosition();
    }

    /// <summary>
    /// Move the camera to the landscape position.
    /// </summary>
    private void SetLandscapeView()
    {
        mainCamera.transform.DOMove(landscapePosition, transitionDuration);
        mainCamera.transform.DORotate(landscapeRotation, transitionDuration);
        Debug.Log("📷 Camera Moved to Landscape Position!");
    }

    /// <summary>
    /// Move the camera to the portrait position.
    /// </summary>
    private void SetPortraitView()
    {
        mainCamera.transform.DOMove(portraitPosition, transitionDuration);
        mainCamera.transform.DORotate(portraitRotation, transitionDuration);
        Debug.Log("📷 Camera Moved to Portrait Position!");
    }

    /// <summary>
    /// Ensure correct camera position at startup.
    /// </summary>
    private void CheckInitialPosition()
    {
        float screenRatio = (float)Screen.width / Screen.height;
        if (screenRatio >= 1)
            SetLandscapeView();
        else
            SetPortraitView();
    }
}
