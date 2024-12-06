using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AccelerometerLevelManager : MonoBehaviour
{
    public static AccelerometerLevelManager instance;
    [SerializeField] GameObject reportingPanel;
    [SerializeField] float levelTimer = 60f;
    [SerializeField] GameObject endCanvas;
    [SerializeField] GameObject gameplayCanvas;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI newRecordText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int bestScore = 5;
    [SerializeField] TargetSpawner spawner;
    [SerializeField] VrModeController controller;
    public bool isStarted;
    public bool isFinished;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = levelTimer;
        UpdateTimer();
    }

    // Update is called once per frame
    void Update()
    {        
        if (isStarted)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                UpdateTimer();
            }
            else if (!isFinished)
            {
                isFinished = true;
                //if (controller != null)
                //{
                //    controller.ExitVR();
                //}
                EndGame();
            }
        }        
    }

    void UpdateTimer()
    {
        timerText.text = timer.ToString("0.0");
    }

    void EndGame()
    {        
        spawner.gameObject.SetActive(false);
        gameplayCanvas.SetActive(false);
        endCanvas.SetActive(true);
        scoreText.text = GameTracker.instance.GetScore().ToString() + " pontos";
        //if (GameTracker.instance.GetScore() > bestScore)
        //{
        //    newRecordText.gameObject.SetActive(true);
        //    bestScore = GameTracker.instance.GetScore();
        //}
    }

    public void SendReport()
    {
        reportingPanel.SetActive(true);
        string text = "Paciente: " + System.Convert.ToString(ProfileManager.GetCurrent(ProfileInfo.Info.stringPatientName)) +
            "\nData: " + DateTime.Now.ToString("d/M/y hh:mm") +
            "\nFase: " + SceneManager.GetActiveScene().name +
            "\nDura��o: " + levelTimer.ToString() +
            "\nPontos Coletados: " + GameTracker.instance.GetScore().ToString() +
            "\nPontos Perdidos: " + GameTracker.instance.GetMisses().ToString() +
            "\nMedia de Pontos: " + ((float)GameTracker.instance.GetScore() / ((float)GameTracker.instance.GetScore() + (float)GameTracker.instance.GetMisses())).ToString() +
            "\nPontos por minuto: " + ((float)GameTracker.instance.GetScore() / (levelTimer / 60f)).ToString();

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
            client.Send("fisiovrjogo@gmail.com", "fisiovrjogo@gmail.com", "An�lise do jogo", text);
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

}
