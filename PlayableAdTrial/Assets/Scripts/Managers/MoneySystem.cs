using UnityEngine;
using TMPro;
using DG.Tweening;
using Luna.Unity;
using System.Collections;

/// <summary>
/// What an unholy way of developing a system, putting everything in a single class...
/// Developed for prototyping purposes. Holds functionalities to be extracted into Single Responsibility modules.
/// </summary>
public class MoneySystem : MonoBehaviour
{
    public static MoneySystem instance;

    [Header("UI Elements")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI incomeText;
    [SerializeField] private GameObject endGameCard;

    [Header("Money Settings")]
    [LunaPlaygroundField("Base Daily Income", 0, "Economy Settings")]
    public int baseDailyIncome = 1000;

    [LunaPlaygroundField("Current Daily Income", 0, "Economy Settings")]
    public int dailyIncome = 1;

    [LunaPlaygroundField("Income Interval (Seconds)", 0, "Economy Settings")]
    public float incomeInterval = 1.5f;

    private int currentMoney = 0;
    private float incomeMultiplier;
    private bool isProcessingDeduction = false;

    private int winCondition = 1000001;
    private bool hasGameEnded = false; 

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        incomeMultiplier = incomeInterval;
        UpdateIncomeUI();
        StartCoroutine(IncomeCoroutine());
    }

    /// <summary>
    /// Coroutine to handle income increment every interval.
    /// </summary>
    private IEnumerator IncomeCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(incomeInterval);
            if (!isProcessingDeduction) // Prevents conflicts
            {
                EarnIncome();
                CheckWinCondition();
            }
        }
    }

    /// <summary>
    /// Earns income every defined interval.
    /// </summary>
    private void EarnIncome()
    {
        int earnedAmount = Mathf.RoundToInt(dailyIncome * incomeMultiplier);
        currentMoney += earnedAmount;
        AnimateMoneyUpdate(earnedAmount);
        UpdateIncomeUI();
    }

    /// <summary>
    /// Upgrades the daily income.
    /// </summary>
    public void UpgradeDailyIncome(int amount)
    {
        dailyIncome += amount;
        UpdateIncomeUI();
    }

    /// <summary>
    /// Updates money & income UI.
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

        moneyText.transform.DOScale(2f, 0.2f).SetLoops(2, LoopType.Yoyo);
    }

    /// <summary>
    /// Increases money manually.
    /// </summary>
    public void AddMoney(int amount)
    {
        currentMoney += amount;
        UpdateIncomeUI();
        CheckWinCondition();
    }

    /// <summary>
    /// Deducts money safely while avoiding conflicts with income updates.
    /// </summary>
    public void DeductMoney(int amount)
    {
        if (currentMoney >= amount)
        {
            StartCoroutine(ProcessDeduction(amount));
        }
        else
        {
            Notifier.instance.ShowNotification("Not Enough Money!");
        }
    }

    /// <summary>
    /// Handles deduction to prevent conflicts.
    /// </summary>
    private IEnumerator ProcessDeduction(int amount)
    {
        isProcessingDeduction = true; // Prevent income from updating during deduction

        currentMoney -= amount;
        Notifier.instance.ShowNotification($"Bought! -{amount}$", 2.5f);
        UpdateIncomeUI();

        yield return new WaitForSeconds(0.2f); // Ensure deduction processes first

        isProcessingDeduction = false; // Allow income updates again
    }

    /// <summary>
    /// Returns if balance is sufficient.
    /// </summary>
    public bool CheckBalance(int amount)
    {
        if (currentMoney >= amount)
        {
            return true;
        }
        else
        {
            Notifier.instance.ShowNotification("Not Enough Money!");
            return false;
        }
    }

    /// <summary>
    /// Checks if the player has reached 1 million money.
    /// </summary>
    private void CheckWinCondition()
    {
        if (!hasGameEnded && currentMoney >= winCondition) 
        {
            hasGameEnded = true;
            TriggerEndGame();
        }
    }

    /// <summary>
    /// Ends the game when the win condition is met.
    /// </summary>
    private void TriggerEndGame()
    {
        if (endGameCard != null)
        {
            endGameCard.SetActive(true);
            Luna.Unity.Analytics.LogEvent("Game Completed", 1);
            Debug.Log("🎉 Game Completed! End Card Activated.");
        }
    }
}
