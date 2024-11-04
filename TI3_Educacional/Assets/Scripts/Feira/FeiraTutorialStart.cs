using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FeiraTutorialStart : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float correctAngle;
    [SerializeField] Animator tutorialSpriteAnim;
    public TextMeshProUGUI txt;
    [SerializeField] TutorialSO moveTutorial;
    [SerializeField] TutorialSO customerTutorial;
    [SerializeField] TutorialSO fruitTutorial;
    [SerializeField] TutorialSO coinTutorial;
    [SerializeField] TutorialSO collectTutorial;
    [SerializeField] TutorialSO desselectTutorial;


    public bool isFirstCoinDone;

    public void DoTutorial(bool isTutorial)
    {
        if (isTutorial == false)
        {
            Destroy(gameObject);
        }
        else
        {
            moveTutorial.ShowText(txt);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (FeiraLevelManager.instance.isStartDone)
        {
            return;
        }
        else
        {
            CheckCorrectAngle();
        }

        if (player.transform.position.z >= transform.position.z)
        {
            FeiraLevelManager.instance.isStartDone = true;
            fruitTutorial.ShowText(txt);
            tutorialSpriteAnim.Play("Default");
        }              
    }

    private void CheckCorrectAngle()
    {
        float yAngle = player.transform.rotation.eulerAngles.y;
        if (yAngle < correctAngle || yAngle > 360 - correctAngle)
        {
            tutorialSpriteAnim.Play("Default");
        }
        else if (yAngle < 360 - correctAngle && yAngle > 180)
        {
            tutorialSpriteAnim.Play("LookRight");
        }
        else
        {
            tutorialSpriteAnim.Play("LookLeft");
        }
    }

    public void StartCoinTutorial()
    {
        Debug.Log("Show text acontecendo");
        coinTutorial.ShowText(txt);
        StartCoroutine(coinTutorial.NextTutorial(txt));
    }

    public void DesselectCoinTutorial()
    {
        desselectTutorial.ShowText(txt);
        desselectTutorial.HideText(txt);
    }

    public void FruitTutorial()
    {
        fruitTutorial.duration = 5f;
        if (!FeiraLevelManager.instance.HasWin())
        {
            fruitTutorial.ShowText(txt);
            fruitTutorial.HideText(txt);
        }
    }

    public void CustomerTrigger()
    {
        customerTutorial.ShowText(txt);
        customerTutorial.HideText(txt);
    }
}
