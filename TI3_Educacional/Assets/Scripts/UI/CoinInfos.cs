using UnityEngine;
using TMPro;
using System.Globalization;

public class CoinInfos : MonoBehaviour 
{
    public static CoinInfos Instance { get; private set; }
    [SerializeField] public TMP_Text textCoin;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateDisplayCoin()
    {
        float valueDecCoin = MiniGameManager.Instance.coinsAcquired / 100.0f;
        float valueDecToCollect = MiniGameManager.Instance.coinsToPurchase / 100.0f;

        textCoin.text = 
        valueDecCoin.ToString("C", CultureInfo.CurrentCulture) 
        + " / " + 
        valueDecToCollect.ToString("C", CultureInfo.CurrentCulture);
    }
}
