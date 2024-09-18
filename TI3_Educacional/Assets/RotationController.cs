using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotationController : MonoBehaviour
{
    [SerializeField] Text textX;
    [SerializeField] Text textY;
    [SerializeField] Text textZ;

    // Update is called once per frame
    void Update()
    {
        textX.text = Input.acceleration.x.ToString();
        textY.text = Input.acceleration.y.ToString();
        textZ.text = Input.acceleration.z.ToString();

        //tentativa de movimento 3D (precisa de calculos melhores)
        //transform.rotation = Quaternion.Euler(-Input.acceleration.z * 90, 0f, -Input.acceleration.x * 90);

        //movimento 2D funcionando perfeitamente
        transform.rotation = Quaternion.Euler(0f, 0f, -Input.acceleration.x * 90);
    }
}
