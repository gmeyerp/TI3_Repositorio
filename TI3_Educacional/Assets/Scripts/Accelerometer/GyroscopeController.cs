using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;

public class GyroscopeController : MonoBehaviour
{
    Vector3 rot;
    private void Start()
    {
        

        rot = Vector3.zero;
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        rot.z = -Input.gyro.rotationRateUnbiased.y;
        rot.y = -Input.gyro.rotationRateUnbiased.z;
        rot.x = -Input.gyro.rotationRateUnbiased.x;
        transform.Rotate(rot);
    }
}
