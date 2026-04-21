using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public float magnitude = 0.1f; 
    public float speed = 1.0f; 

    Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        Vector3 randomOffset = Random.insideUnitSphere * magnitude;

        transform.localPosition = initialPosition + randomOffset;
    }
}
