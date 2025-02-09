using UnityEngine;
using UnityEngine.Events;

public class ScreenOrientationManager : MonoBehaviour
{
    public static ScreenOrientationManager instance;

    [Header("Settings")]
    [Tooltip("Enable/Disable continuous checking")]
    [SerializeField] private bool continuousChecking = false; 
    [Tooltip("Check interval in seconds")]
    [SerializeField] private float checkInterval = 2f; 

    [Header("Events")]
    public UnityEvent OnLandscapeMode;
    public UnityEvent OnPortraitMode;

    private bool isLandscape;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (continuousChecking) StartChecking();
    }

    /// <summary>
    /// Starts continuous orientation checking if enabled.
    /// </summary>
    public void StartChecking()
    {
        CancelInvoke(nameof(CheckScreenOrientation)); // Prevent duplicates
        InvokeRepeating(nameof(CheckScreenOrientation), checkInterval, checkInterval);
    }

    /// <summary>
    /// Checks if the screen orientation has changed.
    /// </summary>
    public void CheckScreenOrientation()
    {
        float screenRatio = (float)Screen.width / Screen.height;
        bool newIsLandscape = screenRatio >= 1;

        if (newIsLandscape != isLandscape) // Only trigger if it changes
        {
            isLandscape = newIsLandscape;
        }
        TriggerOrientationEvent();
    }

    /// <summary>
    /// Fires appropriate events when orientation changes.
    /// </summary>
    private void TriggerOrientationEvent()
    {
        if (isLandscape)
        {
            OnLandscapeMode?.Invoke();
            Debug.Log("📲 Switched to Landscape Mode!");
        }
        else
        {
            OnPortraitMode?.Invoke();
            Debug.Log("📲 Switched to Portrait Mode!");
        }
    }
}
