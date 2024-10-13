using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CoinCollect : MonoBehaviour
{
    public bool collected;

    [Header("Color Coins")]
    [SerializeField] private Color inactiveColor; // Refer�ncia a cor quando jogador n�o estiver olhando
    [SerializeField] private Color activeColor; // Refer�ncia a cor quando jogador estiver olhando
    [SerializeField] private Color limitColor; // Referência a cor de quando o jogador ultrapassar o limite de moedas necessárias

    [Header("Coin Value and Images")]
    [SerializeField] public int valueCoin; // Valor da moeda
    [SerializeField] public Image coinsCollected;
    [SerializeField] GameObject objectCanvas;
    [SerializeField] Image completion;


    private MeshRenderer meshRenderer;
    private Collider coinCollider;

    private void Start() 
    {
        //Ativando o MeshRenderer e o Collider da moeda
        meshRenderer = GetComponent<MeshRenderer>(); 
        coinCollider = GetComponent<Collider>();
        collected = false;

        meshRenderer.material.color = inactiveColor; //Atribui ao material a cor inativa
    }
    
    public void Update()
    {
        if (MiniGameManager.Instance.coinsAcquired > MiniGameManager.Instance.coinsToPurchase)
        {
            //Quando ultrapassar o valor necessario de moedas elas ficarão vermelhas.
            meshRenderer.material.color = limitColor;
        }
    }

    public void ChangeColorOnLook(bool isLooking)
    {
        float lerpSpeed = Time.deltaTime / PlayerRayCast.Instance.timeToCollect;

        if (MiniGameManager.Instance.coinsAcquired > MiniGameManager.Instance.coinsToPurchase)
        {
            //Quando ultrapassar o valor necessario de moedas elas ficarão vermelhas.
            meshRenderer.material.color = limitColor;
        }
        else if (isLooking && collected)
        {
            meshRenderer.material.color = Color.Lerp(meshRenderer.material.color, inactiveColor, lerpSpeed);
        }
        else if (isLooking && !collected)
        {
            //Quando estiver olhando, alterar� a cor atual at� a cor ativa com o metodo Color.Lerp
            meshRenderer.material.color = Color.Lerp(meshRenderer.material.color, activeColor, lerpSpeed);
        }

    }

    public void Collect()
    {
        Debug.Log("Moeda selecionada!");
        MiniGameManager.Instance.coinsAcquired += valueCoin;

        MiniGameManager.Instance.GetoutMiniGame();
        CoinInfos.Instance.UpdateDisplayCoin();
        //Destroy(gameObject);
    }

    public void UnCollect()
    {
        Debug.Log("Moeda deselecionada!");
        MiniGameManager.Instance.coinsAcquired -= valueCoin;

        MiniGameManager.Instance.GetoutMiniGame();
        CoinInfos.Instance.UpdateDisplayCoin();
    }
}
