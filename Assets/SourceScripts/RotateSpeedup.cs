using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSpeedup : MonoBehaviour
{
    private float _destroyDelay = 10.0f;
    private float _rotationSpeed = 120.0f;
    void Start()
    {
        Destroy(gameObject, _destroyDelay);
    }

    void Update()
    {
        transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
    }
}
