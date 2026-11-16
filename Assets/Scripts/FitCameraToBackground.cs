using UnityEngine;

/// Makes the camera FILL the screen with the background (no empty bars).
/// If the screen's shape differs from the image's, a little of the image's edges is cropped.
/// Refits whenever the window size changes (e.g. browser fullscreen).
[RequireComponent(typeof(SpriteRenderer))]
public class FitCameraToBackground : MonoBehaviour
{
    [SerializeField] private Camera cam;   // drag Main Camera here in the Inspector

    private int lastWidth;
    private int lastHeight;

    void Start()
    {
        if (cam == null) cam = Camera.main;
        Fit();
    }

    void Update()
    {
        // Only refit when the window size actually changed
        if (Screen.width != lastWidth || Screen.height != lastHeight)
            Fit();
    }

    private void Fit()
    {
        lastWidth = Screen.width;
        lastHeight = Screen.height;

        Vector2 size = GetComponent<SpriteRenderer>().bounds.size;   // world units
        float screenAspect = (float)Screen.width / Screen.height;
        float spriteAspect = size.x / size.y;

        // FILL: match the side that would otherwise leave a gap
        cam.orthographicSize = spriteAspect >= screenAspect
            ? size.y / 2f                    // image is wider than the screen: match height, crop the sides
            : size.x / screenAspect / 2f;    // image is taller: match width, crop top and bottom

        cam.transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }
}