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
    
    [Header("Value Coins")]
    [SerializeField] public int coinsAcquired; // Moedas que o player tem
    [SerializeField] public int coinsToPurchase; // Moedas necessarias para comprar o item
    [SerializeField] int minValue = 20;
    [SerializeField] int maxValue = 300;
    
    [Header("Player")]
    [SerializeField] public GameObject player;
    public PlayerController playerController;
    public StandSpotTrigger trigger;
    public FeiraTutorialStart feiraTutorial;
    public GameObject fruitSprites;
    
    [Header("Teleports")]
    [SerializeField] private Vector3 miniGamePosition;
    private Vector3 lastPosition; // Pega a �ltima posi��o do player
    

    public CharacterController controller;

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


    public IEnumerator SpawnItens()
    {
        Vector3 spawnPosition = Vector3.zero;
        bool validPosition = false;
        int maxAttempts = 10;  // Limite de tentativas para encontrar uma posição válida
        float minDistance = 2.0f;  // Distância mínima entre moedas

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
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

        yield return null;
    }

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
    }

    public void Infos()
    {
        CoinInfos.Instance.UpdateDisplayCoin();
        CoinInfos.Instance.textCoin.enabled = true;
    }
    #endregion

    public void SetUsedTrigger(StandSpotTrigger trigger)
    {
        this.trigger = trigger;
    }
}
