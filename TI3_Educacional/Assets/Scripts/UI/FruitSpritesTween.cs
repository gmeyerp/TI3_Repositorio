using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpritesTween : MonoBehaviour
{
    [SerializeField] RectTransform[] rectTransform;
    [SerializeField] float delay = 1.5f;
    [SerializeField] float durationAppear = 1;
    [SerializeField] float durationFade = 30;
    // Start is called before the first frame update
    public void GiveFruitSprites()
    {
        LeanTween.init();
        float time = 0f;
        foreach (Transform t in rectTransform)
        {
            LeanTween.scale(t.gameObject, Vector3.zero, 0);
            LeanTween.scale(t.gameObject, Vector3.one, durationAppear).setEaseOutElastic().setDelay(time);
            
            time += delay;
        }

        if (Convert.ToBoolean(ProfileManager.GetCurrent(ProfileInfo.Info.boolFruitMemorize)))
        {
            foreach (RectTransform t in rectTransform)
            {
                LeanTween.color(t, Color.white, 0);
                LeanTween.color(t, new Color(1, 1, 1, 0), durationFade).setEaseOutCubic().setDelay(time);
            }
        }
    }
}
