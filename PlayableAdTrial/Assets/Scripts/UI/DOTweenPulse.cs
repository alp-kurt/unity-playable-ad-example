using UnityEngine;
using DG.Tweening; 

public class DOTweenPulse : MonoBehaviour
{
    [SerializeField] private float scaleMultiplier = 1.2f; 
    [SerializeField] private float duration = 1.5f; 
    [SerializeField] private Ease easeType = Ease.InOutSine; 

    private Tween pulseTween; // Store tween reference

    private void OnEnable()
    {
        StartPulseAnimation();
    }

    private void OnDisable()
    {
        // Stop animation when object is turned off
        pulseTween?.Kill();
    }

    /// <summary>
    /// Starts the scaling animation loop.
    /// </summary>
    private void StartPulseAnimation()
    {

        // Create a new pulse animation
        pulseTween = transform.DOScale(scaleMultiplier, duration)
            .SetEase(easeType) 
            .SetLoops(-1, LoopType.Yoyo);
    }
}
