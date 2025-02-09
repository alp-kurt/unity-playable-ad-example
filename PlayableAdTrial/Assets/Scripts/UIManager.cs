using UnityEngine;

public class UIManager : MonoBehaviour
{
    public RectTransform portraitLayout;
    public RectTransform landscapeLayout;

    private void Start()
    {
        ScreenOrientationManager.instance.OnLandscapeMode.AddListener(SetLandscapeUI);
        ScreenOrientationManager.instance.OnPortraitMode.AddListener(SetPortraitUI);
    }

    void SetLandscapeUI()
    {
        portraitLayout.gameObject.SetActive(false);
        landscapeLayout.gameObject.SetActive(true);
    }

    void SetPortraitUI()
    {
        portraitLayout.gameObject.SetActive(true);
        landscapeLayout.gameObject.SetActive(false);
    }
}

