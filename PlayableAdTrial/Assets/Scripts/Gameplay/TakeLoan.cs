using UnityEngine;

public class TakeLoan : MonoBehaviour
{

    [SerializeField] private string notificationText;
    [SerializeField] private GameObject loan;

    public void TakeOutLoan()
    {
        Luna.Unity.Analytics.LogEvent($"Loan Taken", 1);
        MoneySystem.instance.AddMoney(1000000);
        Notifier.instance.ShowNotification(notificationText, 1.5f);

        loan.SetActive(false); //Turn off the money object

        TutorialTextController.instance.UpdateText("Drag To Move");

        gameObject.SetActive(false);
    }
}
