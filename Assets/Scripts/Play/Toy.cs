using System;
using UnityEngine;

/// A physical toy: it can be thrown, rolls and spins on its own,
/// and reports when the dog touches it (and how fast it was moving).
[RequireComponent(typeof(Rigidbody2D))]
public class Toy : MonoBehaviour
{
    public event Action<float> TouchedByDog;   // sends the toy's speed at the moment of touching

    private Rigidbody2D body;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    /// Put the toy at 'from' and launch it. Physics moves it after this.
    public void Throw(Vector2 from, Vector2 direction, float force, float spin)
    {
        body.linearVelocity = Vector2.zero;     // forget any old movement
        body.angularVelocity = 0f;
        body.position = from;

        body.AddForce(direction.normalized * force, ForceMode2D.Impulse);
        body.AddTorque(spin, ForceMode2D.Impulse);
    }

    // Unity calls this when a SOLID collider (not a trigger) starts touching this one
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out DogController _))
            TouchedByDog?.Invoke(collision.relativeVelocity.magnitude);
    }
}