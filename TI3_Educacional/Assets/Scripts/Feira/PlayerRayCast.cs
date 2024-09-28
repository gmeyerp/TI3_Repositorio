using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  PlayerRayCast : MonoBehaviour
{
    public static PlayerRayCast instance { get; private set; }
    [SerializeField] protected float maxDistance = 10.0f; // Distância máxima do raycast
    [SerializeField] public float timeToCollect = 2.0f; // Tempo que o jogador deve olhar para coletar a moeda
    private float lookTime = 0.0f; // Tempo que o jogador está a olhar para o objeto
    [SerializeField] private GameObject currentTarget; // Referência ao objeto atual que o jogador está olhando
    private Camera mainCamera; // Referência à câmara principal

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        // Atribuindo a câmara principal ao script
        mainCamera = Camera.main;
    }

    void Update()
    {
        LookCoin();
    }

    public void LookCoin()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;

        // Verifica se o raycast está colidindo com um objeto na distância máxima
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            Debug.DrawLine(transform.position, hit.point);
            // Verifica se o objeto colidido é um "Coin" (Moeda)
            if (hit.collider.CompareTag("Coin"))
            {
                currentTarget = hit.collider.gameObject;
                CoinCollect coin = hit.collider.GetComponent<CoinCollect>();
                // Caso o jogador esteja olhando para o objeto
                if(coin != null) 
                { 
                    if (currentTarget == hit.collider.gameObject)
                    {
                        coin.ChangeColorOnLook(true);
                        lookTime += Time.deltaTime;
                        Debug.Log($"Olhando para a moeda à {lookTime}");

                        // Caso o tempo de olhar exceder o tempo necessário para coletar
                        if (lookTime >= timeToCollect)
                        {
                            // A moeda será coletada
                            coin.Collect();
                            currentTarget = null;
                            lookTime = 0.0f;
                        }
                    }
                }
                else
                {
                    // Reinicia o temporizador se o jogador mudar de alvo
                    currentTarget = hit.collider.gameObject;
                    lookTime = 0.0f;
                }
            }
            else
            {
                // Se o jogador não estiver a olhar para nada, reseta o temporizador
                ResetLook();
            }
        }
        else
        {
            ResetLook();
        }
    }

    private void ResetLook()
    {
        if(currentTarget != null)
        {
            CoinCollect notCoin = currentTarget.GetComponent<CoinCollect>();
            if (notCoin == null)
            {
                notCoin.ChangeColorOnLook(false);
            }
        }

        currentTarget = null;
        lookTime = 0.0f;
    }
}