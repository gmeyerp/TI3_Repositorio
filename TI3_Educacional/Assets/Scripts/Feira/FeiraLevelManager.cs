using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum FeiraCustomers { Nenhum, Poucos, Medio, Muitos}
public class FeiraLevelManager : MonoBehaviour
{
    public static FeiraLevelManager instance;
    public bool isPaused;
    [SerializeField] GameObject reportingPanel;
    [SerializeField] PlayerController playerController;
    [Header("Fruit Randomizer")]
    [SerializeField] List<Stand> standsSelects = new List<Stand>();
    [SerializeField] Image[] chosenFruitsImages;
    [SerializeField] Image[] checkImage;
    public bool[] collectedFruit;
    [SerializeField] public int numberOfFruits = 3;
    [SerializeField] public List<SOFruit> chosenFruits;
    [SerializeField] List<SOFruit> fruits;
    [SerializeField] List<Stand> stands = new List<Stand>();
    [SerializeField] FruitSpritesTween fruitSpritesTween;
    public float lastTimer = 0;
    public float[] fruitTimers = new float[3];

    [Header("Customer Options")]
    public float NPCSpeed = 3f;
    [SerializeField] FeiraCustomers customersDifficulty = FeiraCustomers.Poucos;
    [SerializeField] GameObject[] allCustomers;
    [SerializeField] GameObject[] poucosCustomers;
    [SerializeField] GameObject[] medioCustomers;
    [SerializeField] GameObject[] muitosCustomers;
    public bool customerStops;

    [Header("Tutorial")]
    public bool isTutorial = true;
    public bool isStartDone;
    [SerializeField] FeiraTutorialStart feiraTutorial;

    [SerializeField] GameObject gameplayCanvas;
    float timer;
    bool isBestTime;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI hitsText;
    [SerializeField] int hitTimes;
    [SerializeField] float invincibleTimer;
    [SerializeField] GameObject victoryCanvas;
    float bestTime = 500;
    public bool isInvulnerable { get; private set; }
    [SerializeField] AudioClip[] playerHitSFX;
    // Start is called before the first frame update
    void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        if (instance == null)
        { instance = this; }
        else { Destroy(gameObject); }
        timer = 0; 
        //bestTime = pegar do GameManager

    }

    private void Start()
    {
        int newNumberOfFruits = System.Convert.ToInt32(ProfileManager.GetCurrent(ProfileInfo.Info.intFruitAmount));
        if (newNumberOfFruits > 0 && newNumberOfFruits <= chosenFruitsImages.Length)
        { numberOfFruits = newNumberOfFruits; }

        collectedFruit = new bool[numberOfFruits];
        PickFruits();
        GiveChosenFruits();
        fruitSpritesTween.GiveFruitSprites();
        GiveRemainingFruits();

        customersDifficulty = (FeiraCustomers)System.Convert.ToInt32(ProfileManager.GetCurrent(ProfileInfo.Info.intVisitorAmount));
        StartCustomers(customersDifficulty);

        NPCSpeed = System.Convert.ToInt32(ProfileManager.GetCurrent(ProfileInfo.Info.floatVisitorSpeed));

        feiraTutorial.DoTutorial(isTutorial);
        if (AnalyticsTest.instance != null)
        {
            AnalyticsTest.instance.AddAnalytics(gameObject.name, "Quantidade de Frutas Escolhidas", numberOfFruits.ToString());
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }


    public void PickFruits()
    {
        int i;
        for (i = 0; i < numberOfFruits; i++)
        {
            int fruit = UnityEngine.Random.Range(0, fruits.Count);
            chosenFruits.Add(fruits[fruit]);
            chosenFruitsImages[i].sprite = fruits[fruit].sprite;
            chosenFruitsImages[i].gameObject.SetActive(true);
            fruits.RemoveAt(fruit);
        }
        for (; i < chosenFruitsImages.Length; i++)
        {
            chosenFruitsImages[i].gameObject.SetActive(false);
        }
    }

    public void GiveChosenFruits()
    {
        for (int i = 0; i < numberOfFruits; i++)
        {         
            int s = UnityEngine.Random.Range(0, stands.Count);

            stands[s].PopulateStand(chosenFruits[i], i);
            standsSelects.Add(stands[s]);
            stands.RemoveAt(s);
        }
    }

    public void GiveRemainingFruits()
    {
        foreach (Stand s in stands)
        {
            int fruit = UnityEngine.Random.Range(0, fruits.Count);
            if (!s.hasFruit)
            {
                s.PopulateStand(fruits[fruit]);
            }
        }
    }

    public void PlayerHit()
    {
        if (isInvulnerable)
        {
            return;
        }
        hitTimes++;
        isInvulnerable = true;
        StartCoroutine(SetInvincible());

        Gerenciador_Audio.TocarSFX(playerHitSFX[UnityEngine.Random.Range(0, playerHitSFX.Length)]);
    }

    public IEnumerator SetInvincible()
    {
        Debug.Log("Start");
        yield return new WaitForSeconds(invincibleTimer);
        Debug.Log("End");
        isInvulnerable = false;
    }

    public void CollectedFruit(int collectedFruit)
    {
        this.collectedFruit[collectedFruit] = true;
        checkImage[collectedFruit].gameObject.SetActive(true);
        if (HasWin())
        {
            Victory();
        }
    }

    public bool HasWin()
    {
        bool win = true;
        foreach (bool b in collectedFruit)
        {
            if (b == false)
            {
                win = false;
            }
        }

        return win;
    }

    public void Victory()
    {
        if (timer < bestTime)
        {
            Debug.Log("New record"); //ajustar feedbacks no GameManager depois
            bestTime = timer;
            isBestTime = true;
        }
        gameplayCanvas.SetActive(false);
        victoryCanvas.SetActive(true);
        if (AnalyticsTest.instance != null)
        {
            AnalyticsTest.instance.AddAnalytics(gameObject.name, "Vitoria", timer.ToString());
            AnalyticsTest.instance.AddAnalytics(gameObject.name, "Feira Numero de Atingido", hitTimes.ToString());
        }
        if (isBestTime)
        {
            timerText.gameObject.SetActive(true);
            timerText.text = "Recorde: " + timer.ToString("0.0");
        }
        hitsText.text = "Batidas: " + hitTimes.ToString();
    }

    public void StartCustomers(FeiraCustomers customerDifficulty)
    {
        if (AnalyticsTest.instance != null)
        {
            AnalyticsTest.instance.AddAnalytics(gameObject.name, "Feira Dificuldade", customerDifficulty.ToString());
        }

        foreach (GameObject c in allCustomers)
        {
            if (c.activeSelf)
            c.SetActive(false);            
        }
        switch (customerDifficulty)
        {
            case FeiraCustomers.Poucos:
                {
                    foreach (GameObject c in poucosCustomers)
                    {
                        c.SetActive(true);
                    }
                    break;
                }
            case FeiraCustomers.Medio:
                {
                    foreach (GameObject c in medioCustomers)
                    {
                        c.SetActive(true);
                    }
                    break;
                }
            case FeiraCustomers.Muitos:
                {
                    foreach (GameObject c in muitosCustomers)
                    {
                        c.SetActive(true);
                    }
                    break;
                }
            case FeiraCustomers.Nenhum:
                {
                    Debug.Log("No Customers");
                    break;
                }
        }


    }

    public void SetCustomerDifficulty(int i)
    {
        customersDifficulty = (FeiraCustomers)i;
    }

    public FeiraCustomers GetCustomerDifficulty()
    {
        return customersDifficulty;
    }

    public void StartCustomers()
    {
        foreach (GameObject c in allCustomers)
        {
            if (c.activeSelf)
                c.SetActive(false);
        }
        switch (customersDifficulty)
        {
            case FeiraCustomers.Poucos:
                {
                    foreach (GameObject c in poucosCustomers)
                    {
                        c.SetActive(true);
                    }
                    break;
                }
            case FeiraCustomers.Medio:
                {
                    foreach (GameObject c in medioCustomers)
                    {
                        c.SetActive(true);
                    }
                    break;
                }
            case FeiraCustomers.Muitos:
                {
                    foreach (GameObject c in muitosCustomers)
                    {
                        c.SetActive(true);
                    }
                    break;
                }
            case FeiraCustomers.Nenhum:
                {
                    Debug.Log("No Customers");
                    break;
                }
        }
    }
    public void SendReport()
    {
        reportingPanel.SetActive(true);
        string text = "Paciente: " + System.Convert.ToString(ProfileManager.GetCurrent(ProfileInfo.Info.stringPatientName)) +
            "\nData: " + DateTime.Now.ToString("d/M/y hh:mm") +
            "\nFase: " + SceneManager.GetActiveScene().name +
            "\nNúmero de batidas: " + hitTimes.ToString() +
            "\nFrutas coletadas: " + numberOfFruits.ToString() +
            //"\nTempo gasto na fruta 1: " + fruitTimers[0].ToString("0.0") + //Essa parte estava dando algum problema
            //"\nTempo gasto na fruta 2: " + fruitTimers[1].ToString("0.0") + 
            //"\nTempo gasto na fruta 3: " + fruitTimers[2].ToString("0.0") + 
            "\nTempo total: " + timer.ToString("0.0") +
            "\nPassos dados: " + playerController.steps.ToString();

        StartCoroutine(CSendReport(text));
    }

    public IEnumerator CSendReport(string text)
    {
        reportingPanel.SetActive(true);
        yield return new WaitForSeconds(0.1f);        
        try
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("fisiovrjogo@gmail.com", "yavokpljshvwqixe"),
                EnableSsl = true
            };
            client.Send("fisiovrjogo@gmail.com", "fisiovrjogo@gmail.com", "Análise do jogo", text);
            Debug.Log("Email enviado para desenvolvedores");

            if (ProfileManager.IsManaging)
            {
                string email = ProfileManager.GetCurrent(ProfileInfo.Info.stringEmail).ToString();
                if (email != null && email.Contains('@'))
                {
                    client.Send("fisiovrjogo@gmail.com", email, "Análise do jogo", text);
                    Debug.Log("Email enviado para fisioterapeuta");
                }
            }
        }
        catch { }
        AnalyticsTest.instance.Save();

        reportingPanel.SetActive(false);
    }

    public void SetTimer(int fruitIndex)
    {
        float thisTime = Time.time - lastTimer;
        lastTimer = Time.time;
        fruitTimers[fruitIndex] = thisTime;
    }
}
