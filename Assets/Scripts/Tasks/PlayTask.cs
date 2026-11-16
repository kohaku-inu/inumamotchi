using UnityEngine;

/// Play: the dog fetches the toy, the owner throws it, the dog chases it.
/// Catching it while it still rolls fast is a good catch (bonus Trust).
public class PlayTask : GameTask
{
    [SerializeField] private Toy toy;
    [SerializeField] private Hotspot toyHotspot;     // on the toy: makes it clickable
    [SerializeField] private Transform throwFrom;    // where the owner throws from

    [Header("Throw")]
    [SerializeField] private float throwForce = 4f;
    [SerializeField] private float throwSpin = 3f;

    [Header("Reward")]
    [SerializeField] private GameProgress progress;
    [SerializeField] private int trustReward = 1;
    [SerializeField] private int goodCatchBonus = 1;
    [SerializeField] private float goodCatchSpeed = 1.5f;   // toy speed needed for the bonus

    private bool thrown;   // false = the dog still has to fetch it; true = it's been thrown

    public override void Begin()
    {
        thrown = false;
        toy.gameObject.SetActive(true);     // the toy appears
        toyHotspot.SetActive(true);         // and can be clicked
        toy.TouchedByDog += OnTouched;
    }

    public override void Cancel()
    {
        Cleanup();
    }

    private void OnTouched(float speed)
    {
        if (!thrown)
        {
            // First touch: the dog brought the toy, so the owner throws it in a random direction
            thrown = true;
            float angle = Random.Range(0f, 360f);
            Vector2 direction = Quaternion.Euler(0f, 0f, angle) * Vector2.right;
            toy.Throw(throwFrom.position, direction, throwForce, throwSpin);
            return;
        }

        // Second touch: the dog caught it
        Cleanup();
        bool goodCatch = speed >= goodCatchSpeed;
        int reward = trustReward + (goodCatch ? goodCatchBonus : 0);
        Debug.Log($"Toy caught at speed {speed:F1} (good catch: {goodCatch}) -> +{reward} Trust");

        if (progress != null) progress.AddTrust(reward);
        Complete();
    }

    private void Cleanup()
    {
        toy.TouchedByDog -= OnTouched;
        toyHotspot.SetActive(false);
    }
}