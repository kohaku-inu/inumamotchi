using System.Collections;
using UnityEngine;

/// Spawns ScentWisp prefabs at a steady rhythm, each one drifting toward a target.
/// A task starts it with StartEmitting() and stops it with StopEmitting().
public class ScentEmitter : MonoBehaviour
{
    [SerializeField] private GameObject wispPrefab;
    [SerializeField] private Transform target;           // where the scent leads
    [SerializeField] private float interval = 0.4f;      // seconds between puffs

    private Coroutine routine;

    public void StartEmitting()
    {
        if (routine == null)
            routine = StartCoroutine(EmitLoop());
    }

    public void StopEmitting()
    {
        if (routine != null)
        {
            StopCoroutine(routine);
            routine = null;
        }
    }

    private IEnumerator EmitLoop()
    {
        if (wispPrefab == null || target == null) yield break;   // nothing to do

        while (true)
        {
            GameObject wisp = Instantiate(wispPrefab, transform.position, Quaternion.identity);
            wisp.GetComponent<ScentWisp>().Init(target.position);
            yield return new WaitForSeconds(interval);
        }
    }
}
