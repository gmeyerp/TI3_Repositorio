using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 0.3f;

    // Update is called once per frame
    void Update()
    {

        //tentativa de movimento 3D (precisa de calculos melhores)
        //transform.rotation = Quaternion.Euler(-Input.acceleration.z * 90, 0f, -Input.acceleration.x * 90);

        //movimento 2D funcionando perfeitamente

        float rotation = Mathf.Round(-Input.acceleration.x * 10f);
        Quaternion newRotation = Quaternion.Euler(0f, 0f, -Input.acceleration.x * 90);
        newRotation = Quaternion.Lerp(transform.rotation, newRotation, rotationSpeed);
        transform.rotation = newRotation;
    }
}
