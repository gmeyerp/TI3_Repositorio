using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UIElements;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager instance { get; private set; }
    [SerializeField] List<GameObject> localSpawns = new List<GameObject>(); // Lista de spawns
    [SerializeField] List<GameObject> spawnCoins = new List<GameObject>(); // Lista de moedas
    [SerializeField] private float timeToSpawn = 4.0f; // Tempo necessário para spawnar
    [SerializeField] private float spawnTimer = 0; // Temporizador para spawnar 
    [SerializeField] private float timeToDestroy = 2.0f; // Tempo para destruir
    [SerializeField] public int coinsAdquired; // Moedas que o player tem
    [SerializeField] private int coinsToPurchase; // Moedas necessarias para comprar o item
    [SerializeField] private GameObject player;
    private Vector3 lastPosition; // Pega a última posição do player

    [SerializeField] private Vector3 miniGamePosition;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        coinsAdquired = 0;
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            if (player == null)
            {
                Debug.LogError("Jogador não encontrado!");
            }
        }
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= timeToSpawn)
        {
            StartCoroutine(spawnItens());
            spawnTimer = 0.0f; // Reiniciar o temporizador
        }
    }


    IEnumerator spawnItens()
    {
        int indexSpawn = Random.Range(0, localSpawns.Count); // Escolhe aleatoriamente um numero entre 1 e o numero de spawns
        GameObject localSelected = localSpawns[indexSpawn]; // Seta o local onde vai spawnar

        int indexCoin = Random.Range(0, spawnCoins.Count);  // Escolhe aleatoriamente um numero entre 1 e o numero de moedas

        GameObject coin = Instantiate(spawnCoins[indexCoin], localSelected.transform.position, Quaternion.identity); // Spawna a moeda selecionada no lugar selecionado.


        yield return new WaitForSeconds(timeToDestroy); // Espera o tempo de timeToDestroy
        Destroy(coin); // Destroi a moeda
    }

    public void GetoutMiniGame()
    {
        // Se coinsAdquired for igual a coinsToPurchase então teleporta o player para a posição antiga.
        if(coinsAdquired == coinsToPurchase)
        {
            Debug.Log("Moedas iguais");
            TeleportToLastestPosition();
        }
    }

    public void TakeLastPosition()
    {
        lastPosition = player.transform.position; // Armazena a última posição do jogador
        Debug.Log("Peguei a última posição");
    }

    public void TeleportToLastestPosition()
    {
        CharacterController controller = player.GetComponent<CharacterController>();
        controller.enabled = false;
        player.transform.position = lastPosition; // Teleporta o jogador para a última posição salva
        controller.enabled = true;
        Debug.Log("Voltando para a última posição");
    }

    public void TeleportToMiniGame()
    {
        CharacterController controller = player.GetComponent<CharacterController>();
        controller.enabled = false;
        player.transform.position = new Vector3(0, -7.59f, 984.83f); // Teleporta o jogador para o minigame
        controller.enabled = true;
        Debug.Log("Indo para o minigame");
    }

    public void NewPrice()
    {
        coinsAdquired = 0;
        coinsToPurchase = Random.Range(0, 9); // Seta um preço para compra
    }
}
