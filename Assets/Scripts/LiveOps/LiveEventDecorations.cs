using UnityEngine;

public class LiveEventDecorations : MonoBehaviour
{
    [SerializeField] LiveConfigAsset liveConfig;
    [SerializeField] string eventId = "winter_lights";
    [SerializeField] GameObject decorationsRoot;   // parent of all the event props

    void OnEnable()
    {
        liveConfig.Changed += Refresh;
        Refresh();
    }

    void OnDisable() => liveConfig.Changed -= Refresh;

    void Refresh()
    {
        LiveConfig c = liveConfig.Current;
        bool show = liveConfig.EventActive && c.eventDecorations && c.eventId == eventId;
        decorationsRoot.SetActive(show);
    }
}