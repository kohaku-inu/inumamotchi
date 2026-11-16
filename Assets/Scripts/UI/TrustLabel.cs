using TMPro;
using UnityEngine;

// Shows the current Trust. Listens to GameProgress; GameProgress never knows this exists.
public class TrustLabel : MonoBehaviour
{
    [SerializeField] private GameProgress progress;
    [SerializeField] private TMP_Text label;

    void OnEnable()
    {
        // game progress manages the subs, show subscribe to run when trust changed is invoked
        progress.TrustChanged += Show;
        Show(progress.Trust);   // the event only fires on change, so show the starting value now
    }

    void OnDisable()
    {
        progress.TrustChanged -= Show;
    }

    private void Show(int trust)
    {
        label.text = $"Trust: {trust}";
    }
}