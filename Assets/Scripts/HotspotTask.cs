using UnityEngine;

/// A task: activate one hotspot; when the dog arrives, it performs one action; the task rewards Trust.
/// Configure it in the Inspector. Special tasks (like Wake with its timing ring) will be
/// their own GameTask subclasses.
public class HotspotTask : GameTask
{
    [SerializeField] private DogController dog;
    [SerializeField] private ClickInput input;
    [SerializeField] private Hotspot hotspot;
    [SerializeField] private DogAction action;       // shows up as a dropdown in the Inspector

    [Header("Optional")]
    [SerializeField] private ScentEmitter scent;     // leave empty for tasks without a scent trail

    [Header("Progress")]
    [SerializeField] private GameProgress progress;
    [SerializeField] private int trustReward = 1;

    public override void Begin()
    {
        hotspot.Activated += OnArrived;
        hotspot.SetActive(true);

        if (scent != null) scent.StartEmitting();
    }

    private void OnArrived(Hotspot arrivedAt)
    {
        hotspot.Activated -= OnArrived;      // unsubscribe: avoid a double fire
        hotspot.SetActive(false);

        if (scent != null) scent.StopEmitting();

        input.InputLocked = true;            // the world is locked during a task
        dog.PerformAction(action, OnActionDone);
    }

    private void OnActionDone()
    {
        input.InputLocked = false;

        if (progress != null)
            progress.AddTrust(trustReward);

        Complete();
    }
}
