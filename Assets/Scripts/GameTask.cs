using System;
using UnityEngine;

/// The contract every task follows: someone calls Begin(), the task does its own thing,
/// and announces Completed when it is done. The task decides HOW it plays; nobody else does.
public abstract class GameTask : MonoBehaviour
{
    public event Action<GameTask> Completed;

    /// Each concrete task overrides this with its own flow.
    public abstract void Begin();

    /// Subclasses call this when they are finished.
    protected void Complete()
    {
        Completed?.Invoke(this);
    }
}
