using UnityEngine;

public class TakeLoanZone : MonoBehaviour
{
    [SerializeField] private GameObject narrativeWindow;

    private bool hasTriggered = false; // Ensure it triggers only once

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true; // Prevent multiple activations

            narrativeWindow.SetActive(true); // Turn on the narrative window
        }
    }
}
