using UnityEngine;

/// Values that must survive scene changes, stored in an ASSET (not in a scene object).
/// Any script in any scene that holds a reference to this asset reads and writes the same data.
[CreateAssetMenu(fileName = "GameProgress", menuName = "Inumamotchi/Game Progress")]
public class GameProgress : ScriptableObject
{
    [SerializeField] private int daysCompleted;
    [SerializeField] private int trust;

    // Read-only from outside: other scripts must use the methods below to change values
    public int DaysCompleted => daysCompleted;
    public int Trust => trust;

    public void AddTrust(int amount) { trust += amount; }
    public void CompleteDay()        { daysCompleted++; }

    // Right-click the asset's component header in the Inspector -> "Reset values"
    [ContextMenu("Reset values")]
    public void ResetAll()
    {
        daysCompleted = 0;
        trust = 0;
    }
}
