using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;   // the NEW Input System

/// The single place that turns a click into "what did the player mean?".
/// Priority: an active hotspot under the cursor, otherwise walk to a point on the floor.
public class ClickInput : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private DogController dog;
    [SerializeField] private Collider2D floor;          // collider tracing the walkable floor
    [SerializeField] private GameObject ripplePrefab;   // spawned at every tap (optional)

    /// Set to true while a task UI is open ("the world is locked").
    public bool InputLocked { get; set; }

    void Awake()
    {
        if (cam == null) cam = Camera.main;
    }

    void Update()
    {
        if (InputLocked) return;

        Pointer pointer = Pointer.current;   // mouse, touch or pen: whichever is in use
        if (pointer == null || !pointer.press.wasPressedThisFrame) return;

        // Ignore clicks that land on UI buttons
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;

        Vector2 world = cam.ScreenToWorldPoint(pointer.position.ReadValue());

        // Spawn a ripple copy of the prefab where the player tapped
        if (ripplePrefab != null)
            Instantiate(ripplePrefab, world, Quaternion.identity);

        // 1) An active hotspot wins
        foreach (Collider2D col in Physics2D.OverlapPointAll(world))
        {
            if (col.TryGetComponent(out Hotspot hotspot) && hotspot.IsActive)
            {
                hotspot.Interact(dog);
                return;
            }
        }

        // 2) Otherwise walk there, if it is on the floor
        if (floor != null && floor.OverlapPoint(world))
            dog.MoveTo(world);
    }
}
