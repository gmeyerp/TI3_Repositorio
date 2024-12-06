using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ObstacleType { Jump, Dodge, Anchor, Cannon }    
public class GincanaLevelManager : MonoBehaviour
{
    public static GincanaLevelManager instance;
    [SerializeField] GameObject player;
    [SerializeField] Rigidbody playerRB;
    [SerializeField] PlayerController playerController;
    public Vector3 safeSpot;
    public int hitCount = 0;
    public float timer = 0;
    public bool isPaused;
    public bool isFinished;
    [SerializeField] AudioClip hitSFX;

    [SerializeField] GameObject victoryCanvas;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI hitText;

    [SerializeField] GameObject reportingPanel;
    // Start is called before the first frame update
    void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (playerController == null)
        {
            playerController = FindObjectOfType<PlayerController>();
        }
        if (playerRB == null)
        {
            playerRB = player.GetComponent<Rigidbody>();
        }

        safeSpot = player.transform.position;
    }

    private void Update()
    {
        if (!isFinished)
            timer += Time.deltaTime;
    }

    public void HitPlayer(string name)
    {
        Gerenciador_Audio.TocarSFX(hitSFX);
        hitCount++;
        playerRB.velocity = Vector3.zero;
        player.transform.position = new Vector3(safeSpot.x, safeSpot.y + player.transform.position.y, safeSpot.z);
        if (AnalyticsTest.instance != null)
        {
            AnalyticsTest.instance.AddAnalytics(name, "Jogador Atingido", "true");
        }
    }

    public void HitPlayer(ObstacleType type, string name)
    {
        if (type == ObstacleType.Jump)
        {
            if (!playerController.IsJumpSafe())
            {
                Gerenciador_Audio.TocarSFX(hitSFX);
                hitCount++;
                playerRB.velocity = Vector3.zero;
                player.transform.position = new Vector3(safeSpot.x, safeSpot.y + player.transform.position.y, safeSpot.z);
                if (AnalyticsTest.instance != null)
                {
                    AnalyticsTest.instance.AddAnalytics(name, "Jogador Atingido", "true");
                }
            }
        }
        else if (type == ObstacleType.Dodge)
        {
            if (!playerController.isDodging)
            {
                Gerenciador_Audio.TocarSFX(hitSFX);
                hitCount++;
                playerRB.velocity = Vector3.zero;
                player.transform.position = new Vector3(safeSpot.x, safeSpot.y + player.transform.position.y, safeSpot.z);
                if (AnalyticsTest.instance != null)
                {
                    AnalyticsTest.instance.AddAnalytics(name, "Jogador Atingido", "true");
                }
            }
        }
    }

    public void FixedUpdate()
    {
        if (player.transform.position.y <= - 3f)
        {
            HitPlayer("FallTrigger");
            GincanaTutorial.instance.FallTutorial();
        }
    }

    private void OnDestroy()
    {
        if (AnalyticsTest.instance != null)
        {
            AnalyticsTest.instance.AddAnalytics(gameObject.name, "Gincana Numero de Atingido", hitCount.ToString());
            AnalyticsTest.instance.AddAnalytics(gameObject.name, "Gincana Tempo", timer.ToString());
        }
    }

    public void Victory()
    {
        isFinished = true;
        victoryCanvas.SetActive(true);
        playerController.enabled = false;
        if (AnalyticsTest.instance != null)
        {
            AnalyticsTest.instance.AddAnalytics(gameObject.name, "Vitoria", timer.ToString("0.0"));
            AnalyticsTest.instance.AddAnalytics(gameObject.name, "Gincana Numero de Atingido", hitCount.ToString());
        }
        timerText.text = "Tempo: " + timer.ToString("0.0");
        hitText.text = "Batidas: " + hitCount.ToString();
    }

    public void SendReport()
    {
        reportingPanel.SetActive(true);
        string text = "Paciente: " + System.Convert.ToString(ProfileManager.GetCurrent(ProfileInfo.Info.stringPatientName)) +
            "\nData: " + DateTime.Now.ToString("d/M/y hh:mm") +
            "\nFase: " + SceneManager.GetActiveScene().name +
            "\nN�mero de batidas: " + hitCount.ToString() +
            "\nTempo gasto: " + timer.ToString() +
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
