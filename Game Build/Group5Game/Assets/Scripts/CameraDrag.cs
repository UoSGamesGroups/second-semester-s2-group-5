using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    public float dragSpeed = 0.25f;
    public float minCameraXPos = 0.0f;
    public float maxCameraXPos = 0.0f;

    private Vector3 dragOrigin;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed);

        transform.Translate(-move, Space.World);

        if (transform.position.x < minCameraXPos)
        {
            transform.position = new Vector3(minCameraXPos, transform.position.y, transform.position.y);
        }

        if (transform.position.x > maxCameraXPos)
        {
            transform.position = new Vector3(maxCameraXPos, transform.position.y, transform.position.y);
        }
    }


}