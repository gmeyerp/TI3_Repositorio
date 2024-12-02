using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FeiraTutorialStart : MonoBehaviour
{
    public static FeiraTutorialStart instance;
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
    float timer;


    public bool isFirstCoinDone;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    public void DoTutorial(bool isTutorial)
    {
        if (isTutorial == false)
        {
            Destroy(gameObject);
        }
        else
        {
            CreateTutorialText(moveTutorial.duration, moveTutorial.text);
        }
    }
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (txt != null)
        {
            if (timer <= 0)
            {
                txt.gameObject.SetActive(false);
            }            
        }

        
        //if (FeiraLevelManager.instance.isStartDone)
        //{
        //    return;
        //}
        //else
        //{
        //    CheckCorrectAngle();
        //}
        //
        //if (player.transform.position.z >= transform.position.z)
        //{
        //    FeiraLevelManager.instance.isStartDone = true;
        //    fruitTutorial.ShowText(txt);
        //    tutorialSpriteAnim.Play("Default");
        //}              
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

    public void CollectFruitTutorial()
    {
        CreateTutorialText(coinTutorial.duration, coinTutorial.text);
    }

    public void DesselectCoinTutorial()
    {
        CreateTutorialText(desselectTutorial.duration, desselectTutorial.text);
    }

    public void FruitTutorial()
    {
        if (!FeiraLevelManager.instance.HasWin())
        {
            CreateTutorialText(fruitTutorial.duration, fruitTutorial.text);
        }
    }

    public void CustomerTrigger()
    {
        CreateTutorialText(customerTutorial.duration, customerTutorial.text);
    }
}
