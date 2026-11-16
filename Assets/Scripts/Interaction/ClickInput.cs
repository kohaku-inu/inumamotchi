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

        Vector2 screenPos = pointer.position.ReadValue();
        Vector2 world = cam.ScreenToWorldPoint(screenPos);
        Ray ray = cam.ScreenPointToRay(screenPos);

        // feedback first : every tap gets a ripple (including hotspot clicks)
        SpawnRipple(world);

        // 1) An active hotspot wins
        RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.TryGetComponent(out Hotspot hotspot) && hotspot.IsActive)
            {
                hotspot.Interact(dog);
                return;
            }
        }

        // 2) Otherwise walk there, if it is on the floor
        if (floor != null && floor.OverlapPoint(world))
            dog.MoveTo(world);
    }

    private void SpawnRipple(Vector2 position)
    {
        if (ripplePrefab != null)
            Instantiate(ripplePrefab, position, Quaternion.identity);
    }
}
