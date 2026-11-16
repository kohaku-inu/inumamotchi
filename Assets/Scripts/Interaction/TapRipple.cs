using UnityEngine;

/// A tap marker: grows and fades, then removes itself.
/// Lives on the TapRipple prefab; ClickInput spawns one at every tap.
[RequireComponent(typeof(SpriteRenderer))]
public class TapRipple : MonoBehaviour
{
    [SerializeField] private float lifetime = 0.5f;    // seconds
    [SerializeField] private float startScale = 0.2f;
    [SerializeField] private float endScale = 0.8f;

    private SpriteRenderer sr;
    private float age;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        age += Time.deltaTime;
        float t = age / lifetime;                 // goes from 0 to 1 over the lifetime

        if (t >= 1f)
        {
            Destroy(gameObject);                  // remove this copy from the scene
            return;
        }

        transform.localScale = Vector3.one * Mathf.Lerp(startScale, endScale, t);

        Color c = sr.color;
        c.a = 1f - t;                             // fade out
        sr.color = c;
    }
}
