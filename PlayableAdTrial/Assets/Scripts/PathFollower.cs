using UnityEngine;
using DG.Tweening;

public class PathFollower : MonoBehaviour
{
    [SerializeField] private Transform[] pathPoints;
    [SerializeField] private float duration = 5f;
    [SerializeField] private PathType pathType = PathType.Linear;
    [SerializeField] private Ease easeType = Ease.Linear;
    [SerializeField] private CharacterAnimationController animationController;

    private void OnEnable()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        if (pathPoints.Length == 0)
        {
            Debug.LogError("No path points assigned!");
            return;
        }

        Vector3[] waypoints = new Vector3[pathPoints.Length];
        for (int i = 0; i < pathPoints.Length; i++)
        {
            waypoints[i] = pathPoints[i].position;
        }

        // ✅ Play Mixamo Walk Animation
        if (animationController != null)
        {
            animationController.PlayWalk();
        }

        transform.DOPath(waypoints, duration, pathType)
            .SetEase(easeType)
            .SetLookAt(0.01f)
            .SetSpeedBased(false)
            .OnComplete(OnReachDestination);
    }

    private void OnReachDestination()
    {
        Debug.Log("Reached Final Path! Switching to Idle Animation.");

        // Switch to Mixamo Idle Animation
        if (animationController != null)
        {
            animationController.PlayIdle();
        }
    }
}
