using UnityEngine;
using Luna.Unity;

public class BuyComponent : MonoBehaviour
{
    [Header("Purchase Settings")]
    [SerializeField] private int price = 100; 
    [SerializeField] private GameObject[] componentsToActivate;
    [SerializeField] private int incomeMultiplierIncrease = 0;
    [SerializeField] private string lunaEventName = "Component Bought";

    private bool isPurchased = false; // Prevent multiple purchases

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPurchased)
        {
            if (MoneySystem.instance.CheckBalance(price))
            {
                Purchase();
            }
        }
    }

    /// <summary>
    /// Handles the purchase process.
    /// </summary>
    private void Purchase()
    {
        if (MoneySystem.instance.CheckBalance(price))
        {
            // Deduct money and increase income multiplier
            MoneySystem.instance.DeductMoney(price);

            // INCREASE INCOME LOGIC
            MoneySystem.instance.UpgradeDailyIncome(incomeMultiplierIncrease);

            // Turn on given items
            ActivateItems();

            // Log event in Luna Analytics
            LogLuna();

            isPurchased = true; // Prevent multiple purchases

            Notifier.instance.ShowNotification("Purchased successfully!"); // Notify player

            gameObject.SetActive(false); // Disable the trigger after purchase
        }
        else {
            Notifier.instance.ShowNotification("Not enough money!");
        }
    }

    private void ActivateItems()
    {
        // Activate purchased components
        foreach (GameObject obj in componentsToActivate)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
    }

    private void LogLuna()
    {
        // Log event in Luna Analytics
        if (!string.IsNullOrEmpty(lunaEventName))
        {
            Luna.Unity.Analytics.LogEvent(lunaEventName, 1);
        }
    }
}
