using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private float minFov = 7f;
    private float maxFov = 12f;
    private float sensitivity = 10f;
 
    private void FixedUpdate()
    {
        float size = Camera.main.orthographicSize;
        size += Input.GetAxis("Mouse ScrollWheel") * sensitivity * -1;
        size = Mathf.Clamp(size, minFov, maxFov);
        Camera.main.orthographicSize = size;
    }
}
