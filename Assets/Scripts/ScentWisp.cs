using UnityEngine;

/// One puff of scent: drifts from where it was spawned toward a target, fades in and out,
/// then removes itself. Lives on the ScentWisp prefab.
[RequireComponent(typeof(SpriteRenderer))]
public class ScentWisp : MonoBehaviour
{
    [SerializeField] private float lifetime = 2f;     // seconds to travel the whole way
    [SerializeField] private float wobble = 0.15f;    // sideways waving, in world units

    private SpriteRenderer sr;
    private Vector2 start;
    private Vector2 end;
    private float age;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    /// Called by the spawner right after Instantiate.
    public void Init(Vector2 targetPosition)
    {
        start = transform.position;
        end = targetPosition;
    }

    void Update()
    {
        age += Time.deltaTime;
        float t = age / lifetime;                     // 0 -> 1

        if (t >= 1f)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 pos = Vector2.Lerp(start, end, t);    // straight line from start to end
        pos.y += Mathf.Sin(t * 12f) * wobble * (1f - t);   // plus a gentle wave that calms down
        transform.position = pos;

        Color c = sr.color;
        c.a = Mathf.Sin(t * Mathf.PI) * 0.6f;         // invisible -> soft -> invisible
        sr.color = c;
    }
}
