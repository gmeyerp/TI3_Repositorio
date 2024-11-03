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

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        miniGamePosition = MiniGameManager.Instance.spawnCenter.position;
        
        player = MiniGameManager.Instance.player;
    }
    public void GetoutMiniGame()
    {
        // Se coinsAcquired for igual a coinsToPurchase ent�o teleporta o player para a posi��o antiga.
        Debug.Log("Numero de moedas igual a quantidade necess�ria");
        if(MiniGameManager.Instance.coinsAcquired == MiniGameManager.Instance.coinsToPurchase)
        {
            Debug.Log("Teleportando de volta a feira");
            Debug.Log($"x = {lastPosition.x} | z = {lastPosition.z}");
            TeleportToLastPosition(); 
            StopCoroutine(MiniGameManager.Instance.SpawnItens());
            
            CoinInfos.Instance.textCoin.enabled = false;
        }
        else
        {
            Debug.Log($"Moedas adquiridas {MiniGameManager.Instance.coinsAcquired} / {MiniGameManager.Instance.coinsToPurchase}");
        }
    }

    public void TeleportToLastPosition()
    {
        //playerController.enabled = true;
        //MiniGameManager.Instance.controller.enabled = false;

        player.transform.position = lastPosition; // Teleporta o jogador para a �ltima posi��o salva
        //MiniGameManager.Instance.controller.enabled = true;
        PlayerRayCast.Instance.maxDistance = 10.0f;
        CoinInfos.Instance.textCoin.enabled = false;

        Debug.Log("Voltando para a �ltima posi��o");

        trigger.StandComplete();
        trigger.gameObject.SetActive(false);
    }

    public void TakeLastPosition()
    {
        lastPosition = player.transform.position; // Armazena a �ltima posi��o do jogador
        Debug.Log("Peguei a �ltima posi��o");
    }

    public void TeleportToMiniGame()
    {
        MiniGameManager.Instance.DestroycoinsActive();

        MiniGameManager.Instance.Infos();
        //MiniGameManager.Instance.controller.enabled = false;

        player.transform.position = miniGamePosition; // Teleporta o jogador para o minigame
        //MiniGameManager.Instance.controller.enabled = true;

        PlayerRayCast.Instance.maxDistance = 100.0f;
        Debug.Log("Indo para o minigame");
    }
}
