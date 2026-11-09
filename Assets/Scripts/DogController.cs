using System;
using System.Collections;
using UnityEngine;

public enum DogState  { Idle, Walking, Acting }
public enum DogAction { Jump, Nudge, Bark, Sit, Lie }

/// The dog character: walks to points and performs actions.
/// Animations are placeholders (hop / squash / wiggle) so you can test without art.
/// Later, replace the bodies of the *Routine methods with Animator triggers.
public class DogController : MonoBehaviour
{
    [SerializeField] private float speed = 3f;      // world units per second
    [SerializeField] private SpriteRenderer body;   // the dog's sprite, used for flipping

    public DogState State { get; private set; } = DogState.Idle;

    private Coroutine moveRoutine;

    // ---------- Movement ----------

    public void MoveTo(Vector2 target, Action onArrived = null)
    {
        if (State == DogState.Acting) return;                  // busy: ignore
        if (moveRoutine != null) StopCoroutine(moveRoutine);   // player changed their mind
        moveRoutine = StartCoroutine(MoveRoutine(target, onArrived));
    }

    private IEnumerator MoveRoutine(Vector2 target, Action onArrived)
    {
        State = DogState.Walking;

        while (Vector2.Distance(transform.position, target) > 0.05f)
        {
            Vector2 next = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

            if (body != null && Mathf.Abs(next.x - transform.position.x) > 0.001f)
                body.flipX = next.x < transform.position.x;    // assumes the art faces right

            transform.position = next;
            yield return null;                                 // wait one frame
        }

        State = DogState.Idle;
        moveRoutine = null;
        onArrived?.Invoke();
    }

    // ---------- Actions ----------

    public void PerformAction(DogAction action, Action onDone = null)
    {
        if (State == DogState.Acting) return;
        if (moveRoutine != null) { StopCoroutine(moveRoutine); moveRoutine = null; }
        StartCoroutine(ActionRoutine(action, onDone));
    }

    private IEnumerator ActionRoutine(DogAction action, Action onDone)
    {
        State = DogState.Acting;
        Debug.Log($"Dog action: {action}");

        switch (action)
        {
            case DogAction.Jump: yield return Hop(0.6f, 0.4f);     break;
            case DogAction.Sit:  yield return Squash(0.7f, 0.8f);  break;
            case DogAction.Lie:  yield return Squash(0.5f, 1.2f);  break;
            default:             yield return Wiggle(0.6f);        break;   // Nudge, Bark
        }

        State = DogState.Idle;
        onDone?.Invoke();
    }

    // ---------- Placeholder animations ----------

    private IEnumerator Hop(float height, float duration)
    {
        Vector3 start = transform.position;
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float lift = Mathf.Sin(t / duration * Mathf.PI) * height;
            transform.position = start + Vector3.up * lift;
            yield return null;
        }
        transform.position = start;
    }

    private IEnumerator Squash(float yScale, float hold)
    {
        Vector3 baseScale = transform.localScale;
        transform.localScale = new Vector3(baseScale.x, baseScale.y * yScale, baseScale.z);
        yield return new WaitForSeconds(hold);
        transform.localScale = baseScale;
    }

    private IEnumerator Wiggle(float duration)
    {
        Quaternion start = transform.rotation;
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            transform.rotation = start * Quaternion.Euler(0f, 0f, Mathf.Sin(t * 30f) * 10f);
            yield return null;
        }
        transform.rotation = start;
    }
}
