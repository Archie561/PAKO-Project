using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    private float _delay = 2.0f;
    void Start()
    {
        Destroy(gameObject, _delay);
    }
}
