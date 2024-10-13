using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpritesTween : MonoBehaviour
{
    [SerializeField] RectTransform[] rectTransform;
    [SerializeField] float delay = 1.5f;
    [SerializeField] float duration = 1;
    // Start is called before the first frame update
    public void GiveFruitSprites()
    {
        LeanTween.init();
        float time = 0f;
        foreach (Transform t in rectTransform)
        {
            LeanTween.scale(t.gameObject, Vector3.zero, 0);
            LeanTween.scale(t.gameObject, Vector3.one, duration).setEaseOutElastic().setDelay(time);
            
            time += delay;
        }
    }
}
