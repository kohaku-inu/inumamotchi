using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FitCameraToBackground : MonoBehaviour
{
    [SerializeField] private Camera cam;   // drag Main Camera here in the Inspector

    void Start()
    {
        if (cam == null) cam = Camera.main;

        Vector2 size = GetComponent<SpriteRenderer>().bounds.size;  // world units
        float screenAspect = (float)Screen.width / Screen.height;
        float spriteAspect = size.x / size.y;

        // Show the WHOLE image: width limits on wide-image/narrow-screen, height otherwise
        cam.orthographicSize = spriteAspect >= screenAspect
            ? size.x / screenAspect / 2f
            : size.y / 2f;

        cam.transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }
}