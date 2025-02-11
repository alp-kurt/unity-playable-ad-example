using UnityEngine;
using TMPro;
using DG.Tweening;
using Luna.Unity;

public class MoneySystem : MonoBehaviour
{
    public static MoneySystem instance;

    [Header("UI Elements")]
    public TextMeshProUGUI moneyText;   // Displays the current money
    public TextMeshProUGUI incomeText;  // Displays the daily income

    [Header("Money Settings")]
    [LunaPlaygroundField("Base Daily Income", 0, "Economy Settings")]
    public int baseDailyIncome = 1000;  // The default daily income

    [LunaPlaygroundField("Current Daily Income", 0, "Economy Settings")]
    public int dailyIncome = 1;  // The upgradable daily income

    [LunaPlaygroundField("Income Interval (Seconds)", 0, "Economy Settings")]
    public float incomeInterval = 1.5f;  // Time interval for income collection

    private int currentMoney = 0;  
    private float incomeMultiplier ;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        incomeMultiplier = incomeInterval; // Equalize multiplier and interval
        UpdateIncomeUI(); // Initial UI update
        InvokeRepeating(nameof(EarnIncome), incomeInterval, incomeInterval);
    }

    /// <summary>
    /// Earns income every defined interval.
    /// </summary>
    private void EarnIncome()
    {
        int earnedAmount = Mathf.RoundToInt(dailyIncome * incomeMultiplier);
        currentMoney += earnedAmount;
        AnimateMoneyUpdate(earnedAmount);
    }

    /// <summary>
    /// Upgrades the daily income by increasing its base value.
    /// </summary>
    public void UpgradeDailyIncome(int amount)
    {
        dailyIncome += amount;
        UpdateIncomeUI();
    }

    /// <summary>
    /// Updates the UI for money and income.
    /// </summary>
    private void UpdateIncomeUI()
    {
        moneyText.text = currentMoney.ToString();
        incomeText.text = $"{dailyIncome} / Sec";
    }

    /// <summary>
    /// Animates money updates using DOTween.
    /// </summary>
    private void AnimateMoneyUpdate(int earnedAmount)
    {
        int startValue = int.Parse(moneyText.text);
        DOTween.To(() => startValue, x => moneyText.text = x.ToString(), currentMoney, 0.5f)
            .SetEase(Ease.OutQuad);

        // UI Scale Animation for Feedback
        moneyText.transform.DOScale(2f, 0.2f).SetLoops(2, LoopType.Yoyo);
    }

    /// <summary>
    /// Increases current money for the given amount.
    /// </summary>
    public void AddMoney(int amount)
    {
        currentMoney += amount;
    }

    /// <summary>
    /// Reduces current money for the given amount.
    /// </summary>
    /// <param name="amount"></param>
    public void DeductMoney(int amount)
    {
        if(currentMoney > amount)
        {
            currentMoney -= amount;
            Notifier.instance.ShowNotification($"Bought! -{amount}$", 2.5f);
        }
        else {
            Notifier.instance.ShowNotification("Can't Buy!");
        }
    }

    /// <summary>
    /// Returns a boolean according to current balance.
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool CheckBalance(int amount)
    {
        if (currentMoney > amount)
        {
            return true;
        }
        else { Notifier.instance.ShowNotification("Not Enough Money!"); return false; }
    }
}
