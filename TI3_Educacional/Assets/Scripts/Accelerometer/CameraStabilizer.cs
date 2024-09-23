using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStabilizer : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 0.3f;
    void Update()
    {
        float rotation = Mathf.Round(-Input.acceleration.x * 10f);
        Quaternion newRotation = Quaternion.Euler(0f, 0f, -Input.acceleration.x * 90);
        newRotation = Quaternion.Lerp(transform.rotation, newRotation, rotationSpeed);
        transform.rotation = newRotation;
    }
}
