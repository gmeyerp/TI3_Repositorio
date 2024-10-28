using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TweenFade : MonoBehaviour
{
    public float time = 0.5f;
    [SerializeField] CanvasGroup fade;
    private void Start()
    {
        Debug.Log("Teste");
        LeanTween.init();
        LeanTween.alphaCanvas(fade, 1, 0f);
        LeanTween.alphaCanvas(fade, 0, time);
    }

    public void FadeOut()
    {
        LeanTween.alphaCanvas(fade, 0, 0f);
        LeanTween.alphaCanvas(fade, 1, time);
    }
}
