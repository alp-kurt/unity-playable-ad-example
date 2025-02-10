using UnityEngine;
using UnityEngine.Events;
using Luna.Unity;
using System.Collections.Generic;

public class ProgressTracker : MonoBehaviour
{
    public static ProgressTracker instance;

    [System.Serializable]
    public class PhaseEvent
    {
        public string phaseName;
        public UnityEvent OnPhaseCompleted;
    }

    // Dynamic phase list
    public List<PhaseEvent> phases = new List<PhaseEvent>();

    // Tracks completed phases
    private HashSet<int> completedPhases = new HashSet<int>(); 

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Analytics.LogEvent(Analytics.EventType.LevelStart);
    }

    /// <summary>
    /// Completes a phase based on the given index.
    /// Logs event to Luna Analytics.
    /// </summary>
    /// <param name="index">Phase index (starting from 0)</param>
    public void CompletePhase(int index)
    {
        // Validate Index
        if (index < 0 || index >= phases.Count)
        {
            Debug.LogError($"⚠ ERROR: Invalid phase index ({index}). Must be between 0 and {phases.Count - 1}.");
            return;
        }

        // Step 2: Check if the phase is already completed
        if (completedPhases.Contains(index))
        {
            Debug.LogWarning($"⚠ Phase {index} ({phases[index].phaseName}) is already completed.");
            return;
        }

        // Mark Phase as Completed & Trigger Event
        completedPhases.Add(index);
        phases[index].OnPhaseCompleted?.Invoke();

        // Log Event to Luna Analytics
        Luna.Unity.Analytics.LogEvent($"Phase_{index + 1}_Completed", 1); 
        Debug.Log($"📊 SUCCESS: {phases[index].phaseName} Completed!");
    }

    /// <summary>
    /// Ends the game and logs completion to Luna Analytics.
    /// </summary>
    public void CompleteGame()
    {
        GameManager.instance.EndGame();
    }
}
