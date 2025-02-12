using UnityEngine;
using TMPro;
using System.Collections;
using DG.Tweening;

public class TutorialTextController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI tutorialText;

    public static TutorialTextController instance;

    private Coroutine updateTextCoroutine;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        tutorialText.alpha = 0; // Ensure text starts hidden
    }

    /// <summary>
    /// Updates the tutorial text with a smooth transition.
    /// </summary>
    /// <param name="newText">The new text to display.</param>
    public void UpdateText(string newText)
    {
        if (tutorialText == null) return;

        if (updateTextCoroutine != null)
        {
            StopCoroutine(updateTextCoroutine);
        }

        updateTextCoroutine = StartCoroutine(AnimateTextChange(newText));
    }

    /// <summary>
    /// Updates the tutorial text and clears it after a duration.
    /// </summary>
    /// <param name="newText">The text to display.</param>
    /// <param name="duration">Time in seconds before the text clears.</param>
    public void UpdateText(string newText, float duration)
    {
        UpdateText(newText);

        if (updateTextCoroutine != null)
        {
            StopCoroutine(updateTextCoroutine);
        }
        updateTextCoroutine = StartCoroutine(ClearTextAfterDelay(duration));
    }

    /// <summary>
    /// Animates text change with scale-up, color change, and new text update.
    /// </summary>
    private IEnumerator AnimateTextChange(string newText)
    {
        // Change color to green
        tutorialText.DOColor(Color.green, 0.3f);

        // Wait 1.5 seconds before changing text
        yield return new WaitForSeconds(0.8f);

        // Change text & reset color to white
        tutorialText.DOColor(Color.white, 0.3f);

        tutorialText.text = newText;

        // Ensure text is visible
        tutorialText.DOFade(1, 0.3f);
    }

    /// <summary>
    /// Clears the text after the specified delay.
    /// </summary>
    private IEnumerator ClearTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        tutorialText.DOFade(0, 0.3f).OnComplete(() => tutorialText.text = ""); // Fade out, then clear text
    }
}
