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

        float dynamicCooldown = (cooldown / fadeOutSteps) * (image.color.a * fadeOutSteps);

        int cooldownState = Cooldown(dynamicCooldown, ref timeSinceLastStep);
        if (cooldownState == 0)
        { 
            tutorialAnimation.SetBool("Manual", false);
        }
        else if (cooldownState > 0 && image.color != Color.white)
        {
            float transparencyStep = (1 / fadeInTime) * Time.deltaTime;
            image.color += new Color(1, 1, 1, transparencyStep);
        }
    }

    private int Cooldown(float cooldown, ref float timeElapsed)
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed < cooldown)
        { return -1; }

        if (timeElapsed - cooldown < Time.deltaTime )
        { return 0; }

        return 1;
    }

    public void Next()
    {
        timeSinceLastStep = 0;

        tutorialAnimation.SetBool("Manual", true);
        tutorialAnimation.SetTrigger("Next");
    }
}
