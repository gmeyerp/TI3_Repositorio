using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameDebug : MonoBehaviour
{
    TextMesh tm;
    // Start is called before the first frame update
    void Start()
    {
        tm = gameObject.GetComponent(typeof(TextMesh)) as TextMesh;
    }

    // Update is called once per frame
    void Update()
    {
        tm.text = Time.deltaTime.ToString();
    }
}
