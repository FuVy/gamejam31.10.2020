using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;
    public Vector3 offset;
    [SerializeField] float minX, maxX;
    [SerializeField] float minY, maxY;

    private void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform;
    }
    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        if (smoothedPosition.x > maxX)
            smoothedPosition.x = maxX;
        if (smoothedPosition.x < minX)
            smoothedPosition.x = minX;
        if (smoothedPosition.y > maxY)
            smoothedPosition.y = maxY;
        if (smoothedPosition.y < minY)
            smoothedPosition.y = minY;
        transform.position = smoothedPosition;
    }
}
