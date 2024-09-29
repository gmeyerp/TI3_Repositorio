using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStability : MonoBehaviour
{
    [SerializeField] float stableAngle;
    public bool isStable;


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

            if (acceleration.x * 90 > stableAngle || acceleration.x * 90 < -stableAngle)
            {
                isStable = false;
            }
            else
            {
                isStable = true;
            }
        }
    }
}
