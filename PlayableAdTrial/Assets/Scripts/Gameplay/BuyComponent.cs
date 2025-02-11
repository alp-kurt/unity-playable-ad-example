using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Luna.Unity;

public class BuyComponent : MonoBehaviour
{
    [Header("Purchase Settings")]
    [SerializeField] private int price = 100;
    [SerializeField] private GameObject[] componentsToActivate;
    [SerializeField] private int incomeMultiplierIncrease = 0;
    [SerializeField] private string lunaEventName = "Component Bought";

    [Header("3D Progress Bar Settings")]
    [SerializeField] private GameObject progressBarUI; // The 3D world-space UI canvas
    [SerializeField] private Slider progressBar; // The slider inside canvas
    [SerializeField] private float fillDuration = 1.0f; // Time to fill the bar

    private bool isPurchased = false;
    private Coroutine progressCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPurchased)
        {
            StartProgress();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !isPurchased)
        {
            CancelProgress();
        }
    }

    private void StartProgress()
    {
        if (progressBarUI != null)
        {
            progressBarUI.SetActive(true);
            progressBar.value = 0;
        }

        if (progressCoroutine == null)
        {
            progressCoroutine = StartCoroutine(FillProgressBar());
        }
    }

    private void CancelProgress()
    {
        if (progressCoroutine != null)
        {
            StopCoroutine(progressCoroutine);
            progressCoroutine = null;
        }

        if (progressBarUI != null)
        {
            progressBarUI.SetActive(false);
        }
    }

    private IEnumerator FillProgressBar()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fillDuration)
        {
            progressBar.value = elapsedTime / fillDuration;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        progressBar.value = 1f;
        Purchase();
        progressBarUI.SetActive(false);
        progressCoroutine = null;
    }

    private void Purchase()
    {
        if (MoneySystem.instance.CheckBalance(price))
        {
            MoneySystem.instance.DeductMoney(price);
            MoneySystem.instance.UpgradeDailyIncome(incomeMultiplierIncrease);
            ActivateItems();
            LogLuna();
            isPurchased = true;

            Notifier.instance.ShowNotification("Purchased successfully!");
            gameObject.SetActive(false);
        }
        else
        {
            Notifier.instance.ShowNotification("Not enough money!");
        }
    }

    private void ActivateItems()
    {
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
        if (!string.IsNullOrEmpty(lunaEventName))
        {
            Luna.Unity.Analytics.LogEvent(lunaEventName, 1);
        }
    }
}
