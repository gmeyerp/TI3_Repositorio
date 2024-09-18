using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStabilizer : MonoBehaviour
{
    void Update()
    {
        transform.rotation = transform.rotation = Quaternion.Euler(0f, 0f, -Input.acceleration.x * 90);
    }
}
