using UnityEngine;
using Luna.Unity;
using UnityEngine.Events;

public class PauseManager : MonoBehaviour
{

    public UnityEvent OnGamePaused;
    public UnityEvent OnGameResumed;

    void Start()
    {
        Luna.Unity.LifeCycle.OnResume += Resume;
        Luna.Unity.LifeCycle.OnPause += PauseGame;
    }

    private void Resume()
    {
        Time.timeScale = 1;
    }

    /// <summary>
    /// Pauses the game when sent to the background or app store.
    /// </summary>
    private void PauseGame()
    {
        Time.timeScale = 0f;

        OnGamePaused?.Invoke();
        Analytics.LogEvent("Game_Paused", 1);

#if UNITY_EDITOR
        Debug.Log("⏸ Game Paused!");
#endif
    }

    /// <summary>
    /// Resumes the game when returning from the background.
    /// </summary>
    private void ResumeGame()
    {
        Time.timeScale = 1f;

        OnGameResumed?.Invoke();
        Analytics.LogEvent("Game_Resumed", 1);

#if UNITY_EDITOR
        Debug.Log("▶️ Game Resumed!");
#endif
    }

}