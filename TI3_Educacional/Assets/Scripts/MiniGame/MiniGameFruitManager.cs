using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MiniGameFruitManager : MonoBehaviour
{
    CoinCollect coinCollect;
    public static MiniGameFruitManager Instance { get; private set; }

    [Header("Frutas")]
    [SerializeField] public List<SOFruit> fruitList = new List<SOFruit>(); // Lista de Frutas
    [SerializeField] List<GameObject> fruitActive = new List<GameObject>(); // Lista de Frutas ativas na cena
    
    public Transform spawnCenter;
    [SerializeField] private float radius = 10.0f;

    [Header("Timers")]
    [SerializeField] private float timeToSpawn = 0f; // Tempo necess�rio para spawnar
    [SerializeField] private float spawnTimer = 0; // Temporizador para spawnar 
    
    [Header("Value Fruits")]
    [SerializeField] public int fruitsAcquired; // Frutas que o player tem
    [SerializeField] public int fruitsToPurchase; // Frutas necessarias para comprar o item
    [SerializeField] int minValue = 5;
    [SerializeField] int maxValue = 10;
    
    [Header("Player")]
    [SerializeField] public GameObject player;
    [SerializeField] public PlayerController playerController;
    public StandSpotTrigger trigger;
    public FeiraTutorialStart feiraTutorial;
    [SerializeField] public GameObject fruitSprites;
    
    [Header("Teleports")]
    [SerializeField] private Vector3 miniGamePosition;
    private Vector3 lastPosition; // Pega a �ltima posi��o do player
    

    public CharacterController controller;
    Stand stand;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        fruitsAcquired = 0;
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

        if (stand == null)
        {
            stand = FindObjectOfType<Stand>();
            Debug.Log("Procurando stand...");
        }
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
        float minDistance = 7.5f;  // Distância mínima entre frutas

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

            // Verifica se a posição é válida (distância mínima entre outras Frutas)
            validPosition = true;
            foreach (var fruta in fruitActive)
            {
                if (fruta != null && Vector3.Distance(fruta.transform.position, spawnPosition) < minDistance)
                {
                    validPosition = false;
                    Debug.Log("Posicão inválida");
                    break;  // Se encontrar uma fruta muito próxima, para a verificação
                }
            }
            Debug.Log("Posicão válida");
            if (validPosition) break;  // Se encontrar uma posição válida, para a busca
        }

        // Se encontrar uma posição válida, spawna a fruta
        if (validPosition)
        {
            int indexFruta = Random.Range(0, fruitList.Count);  // Escolhe aleatoriamente uma fruta
            SOFruit selectedFruit = fruitList[indexFruta];

            GameObject fruta = Instantiate(selectedFruit.spritePrefab, spawnPosition, Quaternion.identity);
            SphereCollider sphereCollider = fruta.GetComponent<SphereCollider>();
            sphereCollider.radius = 2.2f;
            fruta.transform.localScale = Vector3.one * 0.5f; // Ajusta o tamanho do original
            fruta.tag = "Coin";

            // Adiciona à lista de frutas ativas
            fruitActive.Add(fruta);

            Vector3 targetDirection = miniGamePosition - fruta.transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            fruta.transform.rotation = targetRotation;
        }
        else
        {
            Debug.LogWarning("Não foi possível encontrar uma posição válida para spawnar a fruta após várias tentativas.");
        }

        yield return null;
    }

    #region Fruits
    public void DestroyfruitActive()
    {
        foreach (var fruta in fruitActive)
        {
            if (fruta != null)
            {
                Destroy(fruta); // Destrói a fruta
            }
        }
        fruitList.Clear();
        fruitActive.Clear(); // Limpa a lista de Frutas ativas
    }

    public void AddFruits(SOFruit fruit)
    { 
        if (fruit == null)
        {
            Debug.LogError("A fruta passada para AddFruits é nula!");
            return;  // Evita continuar se a fruta for nula
        }

        if (fruitList.Count > 1)
        {
            fruitList.Clear();
        }

        if (stand == null)
        {
            Debug.LogError("Stand não foi inicializado!");
            return;
        }
        stand.fruitInfo = fruit;
        fruitList.Add(fruit);
    }

    public void ResetValueFruta()
    {
        fruitsAcquired = 0; // Retornando as Frutas para o valor 0
    }

    public void NewPrice()
    {
        ResetValueFruta();
        fruitsToPurchase = Random.Range(minValue, maxValue);
    }

    public void Infos()
    {
        FruitInfos.Instance.UpdateDisplayFruit();
        FruitInfos.Instance.textFruit.enabled = true;
    }
    #endregion

    public void SetUsedTrigger(StandSpotTrigger trigger)
    {
        this.trigger = trigger;
    }
}
