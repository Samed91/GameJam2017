using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    Transform target;
    [Range(0, 10)]
    public float smoothFollowTime;
    [Range(0, 10)]
    public float smoothZoom;
    [Range(-10, 10)]
    public float zOffset;
    [Range(-50, 50)]
    public float zoomLevel;
    float defaultZoomLevel;
    public bool enableFollow = true;
    Vector3 vel = Vector3.zero;
    float smoothVel = 0;
    float smoothedZooom;

    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
        defaultZoomLevel = zoomLevel;
        smoothedZooom = zoomLevel;
    }


    void Update()
    {
        UpdateCameraMovement();
    }

    void UpdateCameraMovement()
    {
        if (!enableFollow)
            return;
        smoothedZooom = Mathf.SmoothDamp(smoothedZooom, zoomLevel, ref smoothVel, smoothZoom);
        Vector3 newPos = new Vector3(target.position.x, target.position.y + smoothedZooom, target.position.z + zOffset);
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref vel, smoothFollowTime);

    }

    public void SetZoomLevel(float newZoomLevel)
    {
        zoomLevel = newZoomLevel;
    }

    public void ResetDefaultZoom()
    {
        zoomLevel = defaultZoomLevel;
    }
}
