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
    [SerializeField] private int valueCoin; // Valor da moeda
    [SerializeField] public Image coinsCollected;
    [SerializeField] GameObject objectCanvas;
    [SerializeField] public Image completion;
    [SerializeField] SpriteRenderer coinSprite;


    //private MeshRenderer meshRenderer;
    private Collider coinCollider;

    private void Start() 
    {
        //Ativando o MeshRenderer e o Collider da moeda
        //meshRenderer = GetComponent<MeshRenderer>(); 
        coinSprite = GetComponent<SpriteRenderer>();
        coinCollider = GetComponent<Collider>();
        collected = false;

        coinSprite.color = inactiveColor; //Atribui ao material a cor inativa
    }
    
    public void Update()
    {
        if (MiniGameManager.Instance.coinsAcquired > MiniGameManager.Instance.coinsToPurchase && collected)
        {
            //Quando ultrapassar o valor necessario de moedas elas ficarão vermelhas.
            coinSprite.color = limitColor;
        }
    }

    public void ChangeColorOnLook(bool isLooking)
    {
        float lerpSpeed = Time.deltaTime / PlayerRayCast.Instance.timeToCollect;

        if (MiniGameManager.Instance.coinsAcquired > MiniGameManager.Instance.coinsToPurchase)
        {
            //Quando ultrapassar o valor necessario de moedas elas ficarão vermelhas.
            coinSprite.color = limitColor;
            if(collected)
                completion.fillAmount -= lerpSpeed;
            else
                completion.fillAmount += lerpSpeed;
        }
        else if (isLooking && collected)
        {
            coinSprite.color = Color.Lerp(coinSprite.color, inactiveColor, lerpSpeed);
            completion.fillAmount -= lerpSpeed;
        }
        else if (isLooking && !collected)
        {
            //Quando estiver olhando, alterar� a cor atual at� a cor ativa com o metodo Color.Lerp
            coinSprite.color = Color.Lerp(coinSprite.color, activeColor, lerpSpeed);
            completion.fillAmount += lerpSpeed;
        }
        completion.color = coinSprite.color;
    }

    public void Collect()
    {
        Debug.Log("Moeda selecionada!");
        MiniGameManager.Instance.coinsAcquired += valueCoin;
        completion.fillAmount = 1;
        completion.color = activeColor;

        CoinInfos.Instance.UpdateDisplayCoin();
        MiniGameTps.Instance.GetoutMiniGame();
        //Destroy(gameObject);
    }

    public void UnCollect()
    {
        Debug.Log("Moeda deselecionada!");
        MiniGameManager.Instance.coinsAcquired -= valueCoin;
        coinSprite.color = inactiveColor;
        completion.fillAmount = 0;
        completion.color = coinSprite.color;

        CoinInfos.Instance.UpdateDisplayCoin();
        MiniGameTps.Instance.GetoutMiniGame();
    }
}
