using UnityEngine;

/// The sunbeam: the dog lies down to rest, then announces that it rested.
/// It doesn't know who listens (DayRunner now, the energy meter later).
public class RestSpot : MonoBehaviour
{
    [SerializeField] private Hotspot hotspot;
    [SerializeField] private DogController dog;
    [SerializeField] private ClickInput input;
    [SerializeField] private GameEvent dogRested;

    void OnEnable()
    {
        hotspot.Activated += OnArrived;
        hotspot.SetActive(true);        // the sunbeam is always available
    }

    void OnDisable()
    {
        hotspot.Activated -= OnArrived;
        hotspot.SetActive(false);
    }

    private void OnArrived(Hotspot _)
    {
        input.InputLocked = true;
        dog.PerformAction(DogAction.Lie, OnRestDone);
    }

    private void OnRestDone()
    {
        input.InputLocked = false;
        dogRested.Raise();
    }
}