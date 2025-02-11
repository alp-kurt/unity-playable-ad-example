using UnityEngine;
using Luna.Unity; 

public class BuyBank : MonoBehaviour
{
    [Header("Bank Purchase Settings")]
    [SerializeField] private int bankCost = 500;
    [SerializeField] private GameObject bankGround; 
    [SerializeField] private GameObject[] buyableObjects;


    private bool hasPurchased = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPurchased)
        {

            if (MoneySystem.instance.CheckBalance(bankCost))
            {
                MoneySystem.instance.DeductMoney(bankCost);
                Luna.Unity.Analytics.LogEvent($"Bank Bought", 1);

                // Enable the bank ground and related objects
                bankGround.SetActive(true);
                foreach (GameObject obj in buyableObjects)
                {
                    obj.SetActive(true);
                }

                // Disable the trigger to prevent re-triggering
                gameObject.SetActive(false);
            }         
        }
    }
}
