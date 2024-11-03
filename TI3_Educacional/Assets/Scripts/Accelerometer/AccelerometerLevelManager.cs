using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AccelerometerLevelManager : MonoBehaviour
{
    [SerializeField] float levelTimer = 60f;
    [SerializeField] GameObject endCanvas;
    [SerializeField] GameObject gameplayCanvas;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI newRecordText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int bestScore = 5;
    [SerializeField] TargetSpawner spawner;
    [SerializeField] VrModeController controller;

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
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            UpdateTimer();
        }
        else
        {
            if (controller != null)
            {
                controller.ExitVR();
            }
            EndGame();
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
        if (GameTracker.instance.GetScore() > bestScore)
        {
            newRecordText.gameObject.SetActive(true);
            bestScore = GameTracker.instance.GetScore();
        }
    }

}
