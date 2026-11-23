using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// Runs a list of tasks, one after another. When the list ends, it records the finished day
/// and moves on to the next scene. It knows nothing about hotspots, the dog, or input.
public class DayRunner : MonoBehaviour
{
    [SerializeField] private List<GameTask> tasks;   // the day, in order

    [Header("When the day ends")]
    [SerializeField] private GameProgress progress;
    [SerializeField] private string nextSceneName = "DayEnd";
    [Tooltip("This many ignored tasks or more makes the day Quiet")]
    [SerializeField] private int quietDayThreshold = 2;
    [SerializeField] private LiveConfigAsset liveConfig;
    [Header("Listens to")]
    [SerializeField] private GameEvent dogRested;   // when the dog rests, the current task's moment passes

    private int index = -1;
    private int ignoredCount;
    void OnEnable()
    {
        if (dogRested != null) dogRested.Raised += SkipCurrent;
    }

    void OnDisable()
    {
        if (dogRested != null) dogRested.Raised -= SkipCurrent;

        // safety net: if the scene unloads in the middle of a task, stop listening to it
        if (index >= 0 && index < tasks.Count)
            tasks[index].Completed -= OnTaskCompleted;
    }
    void Start()
    {
        Next();
    }

    private void Next()
    {
        index++;
        if (index >= tasks.Count)
        {
            FinishDay();
            return;
        }

        Debug.Log($"Starting task {index + 1} of {tasks.Count}: {tasks[index].name}");
        tasks[index].Completed += OnTaskCompleted;
        tasks[index].Begin();
    }

    private void OnTaskCompleted(GameTask task)
    {
        task.Completed -= OnTaskCompleted;
        Next();
    }
    [ContextMenu("Skip current task")]
    public void SkipCurrent()
    {
        if (index < 0 || index >= tasks.Count) return; // no tasks

        GameTask task = tasks[index];
        task.Completed -= OnTaskCompleted;  // never complete, 
        task.Cancel();
        ignoredCount++;
        Debug.Log($"Skipped {task.name} (ignored so far: {ignoredCount})");
        Next();
    }
    private void FinishDay()
    {
        Debug.Log("Day finished");
        int threshold = liveConfig != null ? liveConfig.Current.quietDayThreshold : quietDayThreshold;
        DayOutcome outcome = ignoredCount >= threshold ? DayOutcome.Quiet : DayOutcome.Warm;
        Debug.Log($"Day outcome: {outcome} (ignored {ignoredCount})");

        if (progress != null)
            progress.CompleteDay(outcome);
        if (!string.IsNullOrEmpty(nextSceneName))
            SceneManager.LoadScene(nextSceneName);   // the scene must be in the Build Profiles scene list
    }
}
