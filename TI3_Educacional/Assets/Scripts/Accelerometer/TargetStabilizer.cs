using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetStabilizer : MonoBehaviour
{
    [Range(-1f, 1f)]
    [SerializeField] float correction;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = transform.rotation = Quaternion.Euler(0f, 0f, -Input.acceleration.x * 90 * correction);
    }
}
