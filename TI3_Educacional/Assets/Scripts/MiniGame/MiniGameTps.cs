using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameTps : MonoBehaviour
{
    public static MiniGameTps Instance {get; private set;}

    [Header("Player")]
    [SerializeField] private GameObject player;
    StandSpotTrigger trigger;

    [Header("Teleports")]
    [SerializeField] private Vector3 miniGamePosition;
    private Vector3 lastPosition; // Pega a �ltima posi��o do player
    float coinTimer = 0;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        miniGamePosition = MiniGameFruitManager.Instance.spawnCenter.position;
        
        player = MiniGameFruitManager.Instance.player;
    }
    public void GetoutMiniGame()
    {
        // Se fruitsAcquired for igual a fruitsToPurchase ent�o teleporta o player para a posi��o antiga.
        Debug.Log("Numero de moedas igual a quantidade necess�ria");
        if(MiniGameFruitManager.Instance.fruitsAcquired >= MiniGameFruitManager.Instance.fruitsToPurchase)
        {
            Debug.Log("Teleportando de volta a feira");
            Debug.Log($"x = {lastPosition.x} | z = {lastPosition.z}");
            TeleportToLastPosition();
            MiniGameFruitManager.Instance.fruitSprites.SetActive(true);

            StopCoroutine(MiniGameFruitManager.Instance.SpawnItens());
            
            FruitInfos.Instance.textFruit.enabled = false;
        }
        else
        {
            Debug.Log($"Moedas adquiridas {MiniGameFruitManager.Instance.fruitsAcquired} / {MiniGameFruitManager.Instance.fruitsToPurchase}");
        }
    }

    public void TeleportToLastPosition()
    {
        MiniGameFruitManager.Instance.isStarted = false;
        MiniGameFruitManager.Instance.playerController.enabled = true;
        MiniGameFruitManager.Instance.controller.enabled = false;

        player.transform.position = lastPosition; // Teleporta o jogador para a �ltima posi��o salva
        MiniGameFruitManager.Instance.controller.enabled = true;
        PlayerRayCast.Instance.maxDistance = 10.0f;
        FruitInfos.Instance.textFruit.enabled = false;

        Debug.Log("Voltando para a �ltima posi��o");

        MiniGameFruitManager.Instance.trigger.StandComplete();

        if (AnalyticsTest.instance != null)
        {
            AnalyticsTest.instance.AddAnalytics(gameObject.name, "Duração Moedas", (Time.time - coinTimer).ToString());
        }

        MiniGameFruitManager.Instance.trigger.gameObject.SetActive(false);
    }

    public void TakeLastPosition()
    {
        lastPosition = player.transform.position; // Armazena a �ltima posi��o do jogador
        Debug.Log("Peguei a �ltima posi��o");
    }

    public void TeleportToMiniGame()
    {
        if (MiniGameFruitManager.Instance.feiraTutorial != null)
        {
            MiniGameFruitManager.Instance.feiraTutorial.StartCoinTutorial();
        }
        MiniGameFruitManager.Instance.playerController.enabled = false;
        coinTimer = Time.time;
        MiniGameFruitManager.Instance.DestroyfruitActive();
        MiniGameFruitManager.Instance.fruitSprites.SetActive(false);
        MiniGameFruitManager.Instance.Infos();
        MiniGameFruitManager.Instance.controller.enabled = false;

        player.transform.position = miniGamePosition; // Teleporta o jogador para o minigame
        MiniGameFruitManager.Instance.controller.enabled = true;
        MiniGameFruitManager.Instance.isStarted = true;

        PlayerRayCast.Instance.maxDistance = 100.0f;
        Debug.Log("Indo para o minigame");
    }
}
