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

    private int index = -1;

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

    private void FinishDay()
    {
        Debug.Log("Day finished");

        if (progress != null)
            progress.CompleteDay();

        if (!string.IsNullOrEmpty(nextSceneName))
            SceneManager.LoadScene(nextSceneName);   // the scene must be in the Build Profiles scene list
    }
}
