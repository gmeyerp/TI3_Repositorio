using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraStabilizer : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 0.3f;
    void Update()
    {

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
