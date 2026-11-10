using System;
using UnityEngine;

/// A clickable spot in the house (bed, bowl, door...).
/// Put it on an empty GameObject that has a Collider2D covering the object.
/// Its StandPoint child has a trigger collider + StandZone script: when the dog's collider
/// enters that zone, StandZone calls NotifyDogArrived().
[RequireComponent(typeof(Collider2D))]
public class Hotspot : MonoBehaviour
{
    [SerializeField] private Transform standPoint;              // empty child: where the dog stands
    [SerializeField] private float alreadyHereDistance = 0.15f; // keep about equal to the trigger zone size

    public event Action<Hotspot> Activated;                     // fired when the dog has ARRIVED

    public bool IsActive { get; private set; }                  // only the current task's hotspot is active

    public Vector2 StandPosition =>
        standPoint != null ? (Vector2)standPoint.position : (Vector2)transform.position;

    public void SetActive(bool value) { IsActive = value; }

    /// Called by ClickInput when the player clicks this hotspot.
    public void Interact(DogController dog)
    {
        // Edge case: the dog already stands in the zone, so no "enter" collision will ever happen.
        if (Vector2.Distance(dog.transform.position, StandPosition) <= alreadyHereDistance)
        {
            Activated?.Invoke(this);
            return;
        }

        dog.MoveTo(StandPosition);   // arrival is detected by the trigger zone, not by a callback
    }

    /// Called by StandZone when something with a DogController enters the trigger zone.
    public void NotifyDogArrived(DogController dog)
    {
        if (!IsActive) return;

        // Was the dog really heading HERE, or just walking past this spot?
        bool headedHere = dog.State == DogState.Walking && dog.MoveTarget == StandPosition;
        if (headedHere)
            Activated?.Invoke(this);
    }
}
