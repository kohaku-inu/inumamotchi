using UnityEngine;

/// Goes on a hotspot's StandPoint child, which also has a CircleCollider2D with "Is Trigger" ticked.
/// A trigger detects overlaps but never pushes anything: collision detection without physics.
[RequireComponent(typeof(Collider2D))]
public class StandZone : MonoBehaviour
{
    private Hotspot hotspot;

    void Awake()
    {
        hotspot = GetComponentInParent<Hotspot>();   // searches this object, then its parents
    }

    // Unity calls this when another collider overlaps this trigger for the first time
    void OnTriggerEnter2D(Collider2D other)
    {
        if (hotspot != null && other.TryGetComponent(out DogController dog))
            hotspot.NotifyDogArrived(dog);
    }
}
