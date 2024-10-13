using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpritesTween : MonoBehaviour
{
    [SerializeField] float initHeight;
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
            float targetHeight = t.position.y;
            LeanTween.moveY(t.gameObject, initHeight, 0);
            LeanTween.moveY(t.gameObject, targetHeight, duration).setEaseOutElastic().setDelay(time);
            
            time += delay;
        }
    }
}
