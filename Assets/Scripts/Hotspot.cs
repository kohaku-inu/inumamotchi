using System;
using UnityEngine;

/// A clickable spot in the house (bed, bowl, door...).
/// Put it on an empty GameObject that has a Collider2D covering the object.
[RequireComponent(typeof(Collider2D))]
public class Hotspot : MonoBehaviour
{
    [SerializeField] private Transform standPoint;   // empty child: where the dog stands to use this

    public event Action<Hotspot> Activated;          // fired when the dog has ARRIVED, not on click

    public bool IsActive { get; private set; }       // only the current task's hotspot is active

    // Expression-bodied property: a getter written as a single expression
    public Vector2 StandPosition =>
        standPoint != null ? (Vector2)standPoint.position : (Vector2)transform.position;

    public void SetActive(bool value) { IsActive = value; }

    /// Called by ClickInput: send the dog over, and fire Activated when it gets there.
    public void Interact(DogController dog)
    {
        dog.MoveTo(StandPosition, () => Activated?.Invoke(this));
    }
}
