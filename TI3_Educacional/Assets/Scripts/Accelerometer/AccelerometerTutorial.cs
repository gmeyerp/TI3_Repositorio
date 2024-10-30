using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AccelerometerTutorial : MonoBehaviour
{
    [SerializeField] TutorialSO tutorials;
    [SerializeField] TextMeshProUGUI text;
    bool isTutorial = true;

    private void Start()
    {
        if (isTutorial)
        {
            tutorials.ShowText(text);
            StartCoroutine(tutorials.NextTutorial(text));
            StartCoroutine(tutorials.nextTutorial.HideText(text)); 
        }
    }
}
