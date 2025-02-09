using UnityEngine;
using DG.Tweening; 

public class DOTweenPulse : MonoBehaviour
{
    [SerializeField] private float scaleMultiplier = 1.2f; 
    [SerializeField] private float duration = 1.5f; 
    [SerializeField] private Ease easeType = Ease.InOutSine; 

    private void Start()
    {
        StartPulseAnimation();
    }

    /// <summary>
    /// Starts the scaling animation loop.
    /// </summary>
    private void StartPulseAnimation()
    {
        transform.DOScale(scaleMultiplier, duration)
            .SetEase(easeType) // Smooth animation
            .SetLoops(-1, LoopType.Yoyo);
    }
}
