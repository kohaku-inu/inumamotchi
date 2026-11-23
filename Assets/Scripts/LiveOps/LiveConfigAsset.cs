using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Inumamotchi/Live Config")]
public class LiveConfigAsset : ScriptableObject
{
    [SerializeField] LiveConfig defaults = new LiveConfig();

    [NonSerialized] LiveConfig current;
    public event Action Changed;

    public LiveConfig Current => current ?? defaults;

    public bool EventActive => Current.IsEventActive(DateTime.UtcNow);

    // 1 when no event is running, so the rest of the code can always multiply.
    public float TrustMultiplier => EventActive ? Current.eventTrustMultiplier : 1f;

    public void Apply(LiveConfig config)
    {
        current = config;
        Changed?.Invoke();
    }

    void OnEnable() => current = null;   // start every Play session from the defaults
}