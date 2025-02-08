using UnityEngine;
using Luna.Unity;

public class CTAButton : MonoBehaviour
{

    /// <summary>
    /// Redirects to App Store on CTA button click.
    /// </summary>
    public void OnCTAButtonClick()
    {
        Analytics.LogEvent("CTA_Button_Clicked", 1);
        Luna.Unity.Playable.InstallFullGame();

#if UNITY_EDITOR
        Debug.Log("🛒 Redirecting to App Store...");
#endif
    }
}
