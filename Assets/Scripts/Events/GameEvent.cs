using System;
using UnityEngine;

/// An event stored in an asset. Senders and listeners only need the asset, never each other.
[CreateAssetMenu(fileName = "NewGameEvent", menuName = "Inumamotchi/Game Event")]
public class GameEvent : ScriptableObject
{
    public event Action Raised;

    public void Raise()
    {
        Raised?.Invoke();
    }
}