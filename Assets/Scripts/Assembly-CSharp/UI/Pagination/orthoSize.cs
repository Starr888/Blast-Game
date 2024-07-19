using UnityEngine;

public class OrthoSize : MonoBehaviour
{
    public float baseOrthoSize = 5f; // Default value if not set in Inspector
    public float ERROR_VALUE = 0.1f;  // Default error value if not set in Inspector
    private float lastScreenHeight;
    private float lastScreenWidth;

    private void Awake()
    {
        UpdateOrthosize();
        lastScreenHeight = Screen.height;
        lastScreenWidth = Screen.width;
    }

    private void Update()
    {
        // Check if the screen resolution has changed
        if (Screen.height != lastScreenHeight || Screen.width != lastScreenWidth)
        {
            UpdateOrthosize();
            lastScreenHeight = Screen.height;
            lastScreenWidth = Screen.width;
        }
    }

    public void UpdateOrthosize()
    {
        float baseRes = 1.777778f; // 16:9 aspect ratio
        float res = (float)Screen.height / (float)Screen.width;
        if (Camera.main != null)
        {
            Camera.main.rect = new Rect(0, 0, 1, 1); // Ensure camera rect is set properly
            Camera.main.orthographicSize = ((baseOrthoSize * res) / baseRes) - ERROR_VALUE;

            Debug.Log($"Updated orthographic size to {Camera.main.orthographicSize} for resolution {Screen.width}x{Screen.height}");
        }
        else
        {
            Debug.LogError("Main Camera not found!");
        }
    }
}
