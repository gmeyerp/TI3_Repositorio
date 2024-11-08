using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraHeightDebug : MonoBehaviour
{
    TextMeshProUGUI debug;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        debug = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        debug.text = cam.transform.position.y.ToString();
    }
}
