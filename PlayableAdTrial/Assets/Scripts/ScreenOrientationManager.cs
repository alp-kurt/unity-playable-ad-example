using UnityEngine;
using UnityEngine.Events;

public class ScreenOrientationManager : MonoBehaviour
{
    public static ScreenOrientationManager instance;

    public UnityEvent OnLandscapeMode;
    public UnityEvent OnPortraitMode;

    private bool isLandscape;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CheckScreenOrientation(); // Initial check
        InvokeRepeating(nameof(CheckScreenOrientation), 0f, 2f); // Check every 2 seconds
    }

    /// <summary>
    /// Checks if the screen orientation has changed.
    /// </summary>
    private void CheckScreenOrientation()
    {
        float screenRatio = (float)Screen.width / Screen.height;
        bool newIsLandscape = screenRatio >= 1;

        if (newIsLandscape != isLandscape) // trigger when orientation changes
        {
            isLandscape = newIsLandscape;
            TriggerOrientationEvent();
        }
    }

    /// <summary>
    /// Fires appropriate events when orientation changes.
    /// </summary>
    private void TriggerOrientationEvent()
    {
        if (isLandscape)
        {
            OnLandscapeMode?.Invoke();
            Debug.Log("Switched to Landscape Mode!");
        }
        else
        {
            OnPortraitMode?.Invoke();
            Debug.Log("Switched to Portrait Mode!");
        }
    }
}
