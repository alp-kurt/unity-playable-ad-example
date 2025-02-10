using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator; 
    private string currentAnimation;

    private void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        PlayWalk();
    }

    /// <summary>
    /// Plays the walking animation.
    /// </summary>
    public void PlayWalk()
    {
        PlayAnimation("Walk");
    }

    /// <summary>
    /// Plays the idle animation.
    /// </summary>
    public void PlayIdle()
    {
        PlayAnimation("Idle");
    }

    /// <summary>
    /// Switches animations dynamically without Animator transitions.
    /// </summary>
    private void PlayAnimation(string animationName)
    {
        if (currentAnimation == animationName) return; // Avoid repeating the same animation

        animator.Play(animationName);
        currentAnimation = animationName;
    }
}
