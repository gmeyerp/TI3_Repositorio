using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CoinCollect : MonoBehaviour
{
    public bool collected;

    [Header("Color Coins")]
    [SerializeField] private Color inactiveColor = new Color(); // Refer�ncia a cor quando jogador n�o estiver olhando
    [SerializeField] private Color activeColor = new Color(); // Refer�ncia a cor quando jogador estiver olhando
    [SerializeField] private Color limitColor = new Color(); // Referência a cor de quando o jogador ultrapassar o limite de moedas necessárias

    [Header("Coin Value and Images")]
    [SerializeField] public int valueCoin; // Valor da moeda
    [SerializeField] public Image coinsCollected;
    [SerializeField] GameObject objectCanvas;
    [SerializeField] public Image completion;
    [SerializeField] SpriteRenderer coinSprite;


    //private MeshRenderer meshRenderer;
    private Collider coinCollider;

    private void Start() 
    {
        ColorUtility.TryParseHtmlString("#FAFAFA", out inactiveColor);  // Cor da moeda/fruta quando estiver em inatividade
        ColorUtility.TryParseHtmlString("#00FF08", out activeColor);   // Cor da moeda/fruta quando estiver ativa
        ColorUtility.TryParseHtmlString("#FF0000", out limitColor);    // Cor da moeda/fruta quando passar o limite estabelecido
        coinSprite = GetComponent<SpriteRenderer>();
        coinCollider = GetComponent<Collider>();
        collected = false;

        coinSprite.color = inactiveColor; //Atribui ao material a cor inativa
    }
    
    public void Update()
    {
        if (MiniGameFruitManager.Instance.fruitsAcquired > MiniGameFruitManager.Instance.fruitsToPurchase && collected)
        {
            //Quando ultrapassar o valor necessario de moedas elas ficarão vermelhas.
            coinSprite.color = limitColor;
        }
    }

    public void ChangeColorOnLook(bool isLooking)
    {
        float lerpSpeed = Time.deltaTime / PlayerRayCast.Instance.timeToCollect;

        /*if (MiniGameManager.Instance.coinsAcquired > MiniGameManager.Instance.coinsToPurchase)
        {
            //Quando ultrapassar o valor necessario de moedas elas ficarão vermelhas.
            completion.color = limitColor;
            if(collected)
                completion.fillAmount -= lerpSpeed;
            else
                completion.fillAmount += lerpSpeed;
        }*/
        if (isLooking && collected)
        {
            completion.color = Color.Lerp(completion.color, inactiveColor, lerpSpeed);
            completion.fillAmount -= lerpSpeed;
        }
        else if (isLooking && !collected)
        {
            //Quando estiver olhando, alterar� a cor atual at� a cor ativa com o metodo Color.Lerp
            completion.color = Color.Lerp(completion.color, activeColor, lerpSpeed);
            completion.fillAmount += lerpSpeed;
        }
    }

    public void Collect()
    {
        Debug.Log("Moeda selecionada!");
        MiniGameManager.Instance.coinsAcquired += valueCoin;
        MiniGameFruitManager.Instance.fruitsAcquired += valueCoin;
        completion.fillAmount = 1;
        completion.color = activeColor;
        coinSprite.color = activeColor;

        FruitInfos.Instance.UpdateDisplayFruit();
        MiniGameTps.Instance.GetoutMiniGame();
    }

    public void UnCollect()
    {
        Debug.Log("Moeda deselecionada!");
        MiniGameManager.Instance.coinsAcquired -= valueCoin;
        MiniGameFruitManager.Instance.fruitsAcquired -= valueCoin;
        completion.fillAmount = 0;
        completion.color = inactiveColor;
        coinSprite.color = inactiveColor;

        FruitInfos.Instance.UpdateDisplayFruit();
        MiniGameTps.Instance.GetoutMiniGame();
    }
}
