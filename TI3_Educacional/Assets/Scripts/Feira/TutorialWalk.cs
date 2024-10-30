using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialWalk : MonoBehaviour
{
    [SerializeField] private float cooldown = 7;
    [SerializeField] private float fadeInTime = 3;
    [SerializeField] private int fadeOutSteps = 5;

    private Animator tutorialAnimation;
    private Image image;

    private float timeSinceLastStep = 0;

    private void Awake()
    {
        tutorialAnimation = GetComponent<Animator>();
        image = GetComponent<Image>();
    }

    private void Update()
    {
        timeSinceLastStep += Time.deltaTime;

        if (timeSinceLastStep > cooldown && timeSinceLastStep - cooldown < Time.deltaTime)
        {
            tutorialAnimation.SetBool("Manual", false);
            tutorialAnimation.SetTrigger("Next");
        }
        else if (timeSinceLastStep > cooldown && image.color != Color.white)
        {
            float transparencyStep = (1 / fadeInTime) * Time.deltaTime;
            image.color += new Color(0, 0, 0, Mathf.Min(transparencyStep, 1 - image.color.a));
        }
    }

    public void Next()
    {
        tutorialAnimation.SetTrigger("Next");
    }

    public void Step()
    {
        timeSinceLastStep = 0;
        tutorialAnimation.SetBool("Manual", true);

        float transparencyStep = (1f / fadeOutSteps);

        if (image.color.a != 0 && image.color.a < transparencyStep)
        { image.color = new Color(1, 1, 1, 0); }
        else 
        { image.color -= new Color(0, 0, 0, Mathf.Min(transparencyStep, image.color.a)); }
    }
}
