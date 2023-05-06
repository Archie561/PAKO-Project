using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;

    private Vector3 _offset = new Vector3(0, 15, -10);
    private float _smoothSpeed = 10.0f;

    void LateUpdate()
    {
        Vector3 playerPosition = playerTransform.position + _offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, playerPosition, _smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;
    }
}
