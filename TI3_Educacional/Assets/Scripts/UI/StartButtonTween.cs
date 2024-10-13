using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonTween : MonoBehaviour
{
    [SerializeField] GameObject[] buttons;
    [SerializeField] float delay = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        LeanTween.init();
        float t = 1;
        foreach (GameObject o in buttons)
        {
            LeanTween.scale(o, Vector3.zero, 0);
            LeanTween.scale(o, Vector3.one, 1f).setDelay(t).setEaseOutElastic();
            t += delay;
        }
    }
}
