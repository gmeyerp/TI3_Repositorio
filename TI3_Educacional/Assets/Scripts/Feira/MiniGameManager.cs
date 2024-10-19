using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager Instance { get; private set; }

    [Header("Lists")]
    [SerializeField] List<GameObject> coinsList = new List<GameObject>(); // Lista de moedas
    [SerializeField] List<GameObject> coinsActive = new List<GameObject>(); // Lista de moedas ativas na cena
    
    public Transform spawnCenter;
    [SerializeField] private float radius = 10.0f;

    [Header("Timers")]
    [SerializeField] private float timeToSpawn = 4.0f; // Tempo necess�rio para spawnar
    [SerializeField] private float spawnTimer = 0; // Temporizador para spawnar 
    [SerializeField] float waitTeleportTime = 3.0f;
    
    [Header("Value Coins")]
    [SerializeField] public int coinsAcquired; // Moedas que o player tem
    [SerializeField] public int coinsToPurchase; // Moedas necessarias para comprar o item
    [SerializeField] int minValue = 20;
    [SerializeField] int maxValue = 300;

    
    [Header("Player")]
    [SerializeField] private GameObject player;
    [SerializeField] PlayerController playerController;
    StandSpotTrigger trigger;
    
    [Header("Teleports")]
    [SerializeField] private Vector3 miniGamePosition;
    private Vector3 lastPosition; // Pega a �ltima posi��o do player
    

    CharacterController controller;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        coinsAcquired = 0;
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");

            if (player == null)
            {
                Debug.LogError("Jogador n�o encontrado!");
            }
        }
        controller = player.GetComponent<CharacterController>();
        miniGamePosition = spawnCenter.position;
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= timeToSpawn)
        {
            StartCoroutine(SpawnItens());
            spawnTimer = 0.0f; // Reiniciar o temporizador
        }
    }


    IEnumerator SpawnItens()
    {
        yield return new WaitForSeconds(spawnTimer);

        Vector3 spawnPosition = Vector3.zero;
        bool validPosition = false;
        int maxAttempts = 10;  // Limite de tentativas para encontrar uma posição válida
        float minDistance = 2.0f;  // Distância mínima entre moedas

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            Debug.Log("Tentando spawnar a moeda");
            // Gera coordenadas esféricas
            float u = Random.Range(0f, 1f); // Gera um valor entre 0 e 1
            float theta = Random.Range(0f, 2f * Mathf.PI); // Gera um ângulo theta entre 0 e 2 * PI
            float phi = Mathf.Acos(2 * u - 1); // Gera um ângulo phi para a esfera

            // Calcula as coordenadas cartesianas a partir das coordenadas esféricas
            float x = radius * Mathf.Sin(phi) * Mathf.Cos(theta);
            float y = radius * Mathf.Sin(phi) * Mathf.Sin(theta);
            float z = radius * Mathf.Cos(phi);

            // Define a posição final de spawn somando o ponto central
            spawnPosition = new Vector3(x, y, z) + spawnCenter.position;

            // Verifica se a posição é válida (distância mínima entre outras moedas)
            validPosition = true;
            foreach (var coin in coinsActive)
            {
                if (coin != null && Vector3.Distance(coin.transform.position, spawnPosition) < minDistance)
                {
                    validPosition = false;
                    Debug.Log("Posicão inválida");
                    break;  // Se encontrar uma moeda muito próxima, para a verificação
                }
            }
            Debug.Log("Posicão válida");
            if (validPosition) break;  // Se encontrar uma posição válida, para a busca
        }

        // Se encontrar uma posição válida, spawna a moeda
        if (validPosition)
        {
            int indexCoin = Random.Range(0, coinsList.Count);  // Escolhe aleatoriamente uma moeda
            GameObject coin = Instantiate(coinsList[indexCoin], spawnPosition, Quaternion.identity); // Spawna a moeda na posição

            coinsActive.Add(coin); // Adiciona à lista de moedas ativas
        }
        else
        {
            Debug.LogWarning("Não foi possível encontrar uma posição válida para spawnar a moeda após várias tentativas.");
        }
    }

    #region Teleports
    public void GetoutMiniGame()
    {
        // Se coinsAcquired for igual a coinsToPurchase ent�o teleporta o player para a posi��o antiga.
        Debug.Log("Numero de moedas igual a quantidade necess�ria");
        if(coinsAcquired == coinsToPurchase)
        {
            Debug.Log("Teleportando de volta a feira");
            Debug.Log($"x = {lastPosition.x} | z = {lastPosition.z}");
            TeleportToLastPosition();
            StopCoroutine(SpawnItens());
            
            CoinInfos.Instance.textCoin.enabled = false;
        }
        else
        {
            Debug.Log($"Moedas adquiridas {coinsAcquired} / {coinsToPurchase}");
        }
    }

    public void TakeLastPosition()
    {
        lastPosition = player.transform.position; // Armazena a �ltima posi��o do jogador
        Debug.Log("Peguei a �ltima posi��o");
    }

    public void TeleportToLastPosition()
    {
        //playerController.enabled = true;
        controller.enabled = false;

        player.transform.position = lastPosition; // Teleporta o jogador para a �ltima posi��o salva
        controller.enabled = true;
        PlayerRayCast.Instance.maxDistance = 10.0f;
        CoinInfos.Instance.textCoin.enabled = false;

        Debug.Log("Voltando para a �ltima posi��o");

        trigger.StandComplete();
        trigger.gameObject.SetActive(false);
    }

    public void TeleportToMiniGame()
    {
        DestroycoinsActive();

        CoinInfos.Instance.UpdateDisplayCoin();
        CoinInfos.Instance.textCoin.enabled = true;
        controller.enabled = false;

        player.transform.position = miniGamePosition; // Teleporta o jogador para o minigame
        controller.enabled = true;

        PlayerRayCast.Instance.maxDistance = 100.0f;
        Debug.Log("Indo para o minigame");
    }
    #endregion

    #region Coins
    public void DestroycoinsActive()
    {
        foreach (var coin in coinsActive)
        {
            if (coin != null)
            {
                Destroy(coin); // Destrói a moeda
            }
        }
        coinsActive.Clear(); // Limpa a lista de moedas ativas
    }

    public void ResetValueCoin()
    {
        coinsAcquired = 0; // Retornando as moedas para o valor 0
    }

    public void NewPrice()
    {
        ResetValueCoin();
        coinsToPurchase = Random.Range(minValue, maxValue);
        Debug.Log($"Moedas para conseguir é de {coinsToPurchase}");
    }
    #endregion

    public void SetUsedTrigger(StandSpotTrigger trigger)
    {
        this.trigger = trigger;
    }
}
