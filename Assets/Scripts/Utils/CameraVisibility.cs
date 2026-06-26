using UnityEngine;

public static class CameraVisibility
{
    public static bool IsVisible(Vector3 worldPoint, Camera camera = null)
    {
        camera ??= Camera.main;
        if (camera == null) return false;

        Vector3 screenPoint = camera.WorldToScreenPoint(worldPoint);

        return screenPoint.z > 0f
            && screenPoint.x >= 0f && screenPoint.x <= Screen.width
            && screenPoint.y >= 0f && screenPoint.y <= Screen.height;
    }
}
