using UnityEngine;

public class TakeLoan : MonoBehaviour
{
    [Header("Narrative Window")]
    [SerializeField] private GameObject narrativeWindow;
    [SerializeField] private GameObject loan;


    private bool hasTriggered = false; // Ensure it triggers only once

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true; // Prevent multiple activations

            narrativeWindow.SetActive(true); // Turn on the narrative window
            loan.SetActive(false); //Turn off the money object

            Luna.Unity.Analytics.LogEvent($"Loan Taken", 1);

            MoneySystem.instance.AddMoney(1000000);

            gameObject.SetActive(false);
        }
    }
}
