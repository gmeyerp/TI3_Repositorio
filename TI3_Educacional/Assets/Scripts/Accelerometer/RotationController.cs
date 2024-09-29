using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotationController : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 0.3f;

    private void Awake()
    {
        Accelerometer accelerometer = Accelerometer.current;
    }
    // Update is called once per frame
    void Update()
    {

        //tentativa de movimento 3D (precisa de calculos melhores)
        //transform.rotation = Quaternion.Euler(-Input.acceleration.z * 90, 0f, -Input.acceleration.x * 90);

        //movimento 2D funcionando perfeitamente

        Accelerometer accelerometer = Accelerometer.current;
        if (!accelerometer.enabled)
        {
            InputSystem.EnableDevice(Accelerometer.current);
            Debug.Log("Linear Acceleration enabled");
        }
        else
        {
            Vector3 acceleration = accelerometer.acceleration.value;

            float rotation = Mathf.Round(-acceleration.x * 10f);
            Quaternion newRotation = Quaternion.Euler(0f, 0f, -acceleration.x * 90);
            newRotation = Quaternion.Lerp(transform.rotation, newRotation, rotationSpeed);
            transform.rotation = newRotation;
        }
    }
}
