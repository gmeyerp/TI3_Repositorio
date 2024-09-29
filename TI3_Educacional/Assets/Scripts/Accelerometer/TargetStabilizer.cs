using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TargetStabilizer : MonoBehaviour
{
    [Range(-1f, 1f)]
    [SerializeField] float correction;

    // Update is called once per frame
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
            
            transform.rotation = transform.rotation = Quaternion.Euler(0f, 0f, -acceleration.x * 90 * correction);
        }
    }
}
