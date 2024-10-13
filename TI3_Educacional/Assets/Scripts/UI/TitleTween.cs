using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleTween : MonoBehaviour
{
    [SerializeField] float startSize = 0.3f;
    [SerializeField] float maxSize = 1.2f;
    [SerializeField] LeanTweenType tweenType;
    [SerializeField] float time = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.init();
        LeanTween.scale(gameObject, Vector3.one * startSize, 0);
        LeanTween.scale(gameObject, Vector3.one * maxSize, time).setEase(tweenType);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
