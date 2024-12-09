using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GincanaTutorial : MonoBehaviour
{
    public static GincanaTutorial instance;
    [SerializeField] TutorialSO moveTutorial;
    [SerializeField] TutorialSO fallTutorial;
    [SerializeField] TutorialSO dodgeTutorial;
    [SerializeField] TutorialSO jumpTutorial;

    [SerializeField] TextMeshProUGUI txt;
    public bool isTutorial = true;
    float timer;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    private void Start()
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

    public void FallTutorial()
    {
        CreateTutorialText(fallTutorial.duration, fallTutorial.text);
    }

    public void DodgeTutorial()
    {
        CreateTutorialText(dodgeTutorial.duration, dodgeTutorial.text);
    }

    public void JumpTutorial()
    {
        CreateTutorialText(jumpTutorial.duration, jumpTutorial.text);
    }
}
