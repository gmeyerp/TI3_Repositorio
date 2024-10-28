using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenFade : MonoBehaviour
{
    public float time = 0.5f;
    private void Start()
    {
        LeanTween.alpha(gameObject, 1, 0f);
        LeanTween.alpha(gameObject, 0, time);
    }

    public void FadeOut()
    {
        LeanTween.alpha(gameObject, 0, 0f);
        LeanTween.alpha(gameObject, 1, time);
    }
}
