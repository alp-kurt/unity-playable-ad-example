using UnityEngine;
using UnityEngine.Events;
using Luna.Unity;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public UnityEvent OnGameStarted;
    public UnityEvent OnGameEnded;

    private bool isGameEnded = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartGame();
        isGameEnded = false;
    }

    /// <summary>
    /// Starts the game and logs it to Luna Analytics.
    /// </summary>
    public void StartGame()
    {
        if (isGameEnded) return;

        Time.timeScale = 1f;
 
        OnGameStarted?.Invoke();
        Analytics.LogEvent(Analytics.EventType.LevelStart);

#if UNITY_EDITOR
        Debug.Log("📊 Game Started!");
#endif
    }


    /// <summary>
    /// Ends the game, triggers CTA, and logs completion.
    /// </summary>
    public void EndGame()
    {
        if (isGameEnded) return;

        Time.timeScale = 0f;
        isGameEnded = true;

        OnGameEnded?.Invoke();
        Analytics.LogEvent("Playable_Completed", 1);

        // Notify Luna Playable that the game has ended
        Luna.Unity.LifeCycle.GameEnded();

#if UNITY_EDITOR
        Debug.Log("🏁 Game Ended! CTA Triggered.");
#endif
    }
}
