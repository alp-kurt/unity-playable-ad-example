using UnityEngine;
using TMPro;
using DG.Tweening;

public class Notifier : MonoBehaviour
{
    public static Notifier instance;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI notificationText;
    [SerializeField] private CanvasGroup canvasGroup; 

    [Header("Settings")]
    [SerializeField] private float fadeInDuration = 0.3f;
    [SerializeField] private float displayDuration = 1.5f;
    [SerializeField] private float fadeOutDuration = 0.5f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Hide notification initially
        if (canvasGroup == null)
        {
            canvasGroup = notificationText.GetComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0;
    }

    /// <summary>
    /// Displays a notification with the given message.
    /// </summary>
    public void ShowNotification(string message, float duration)
    {
        if (notificationText == null) return;

        notificationText.text = message;
        canvasGroup.DOFade(1f, fadeInDuration) // Fade in
            .OnComplete(() => StartCoroutine(HideAfterDelay(duration)));
    }

    /// <summary>
    /// Displays a notification with the given message.
    /// </summary>
    public void ShowNotification(string message)
    {
        if (notificationText == null) return;

        notificationText.text = message;
        canvasGroup.DOFade(1f, fadeInDuration) // Fade in
            .OnComplete(() => StartCoroutine(HideAfterDelay(displayDuration)));
    }

    /// <summary>
    /// Hides the notification after a delay.
    /// </summary>
    private System.Collections.IEnumerator HideAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        canvasGroup.DOFade(0f, fadeOutDuration); // Fade out
    }
}
