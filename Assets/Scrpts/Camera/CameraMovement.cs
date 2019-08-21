using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float screenEdgeThickness = 10.0f;
    public float speed = 5f;

    public void FixedUpdate()
    {
        var offset = transform.position;

        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - screenEdgeThickness)
            offset.y += speed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= screenEdgeThickness)
            offset.y -= speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= screenEdgeThickness)
            offset.x -= speed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - screenEdgeThickness)
            offset.x += speed * Time.deltaTime;

        transform.position = offset;
    }
}
