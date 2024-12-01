using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AccelerometerTutorial : MonoBehaviour
{
    [SerializeField] TutorialSO firstTutorial;
    [SerializeField] TutorialSO secondTutorial;
    [SerializeField] TextMeshProUGUI txt;
    float timer = 0;
    public bool isTutorial = true;
    bool isFirstDone;

    private void Start()
    {
        if (isTutorial == false)
        {
            Destroy(gameObject);
        }
        else
        {
            CreateTutorialText(firstTutorial.duration, firstTutorial.text);
        }
    }
    private void Update()
    {
        timer -= Time.deltaTime;

        if (txt != null)
        {
            if (timer <= 0)
            {
                if (isFirstDone == false)
                {
                    CreateTutorialText(secondTutorial.duration, secondTutorial.text);
                    isFirstDone = true;
                }
                else
                {
                    txt.gameObject.SetActive(false);
                }
            }
        }
    }
    public void CreateTutorialText(float duration, string text)
    {
        if (txt != null)
        {
            txt.gameObject.SetActive(true);
            timer = duration;
            txt.text = text;
        }
    }
}
