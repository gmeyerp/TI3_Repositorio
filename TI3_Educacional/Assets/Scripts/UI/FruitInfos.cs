using UnityEngine;
using TMPro;
using System.Globalization;

public class FruitInfos : MonoBehaviour
{
    public static FruitInfos Instance { get; private set; }
    [SerializeField] public TMP_Text textFruit;

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

    public void UpdateDisplayFruit()
    {
        float valueFruitsAcquired = MiniGameFruitManager.Instance.fruitsAcquired;
        float valueFruitsToPurchase = MiniGameFruitManager.Instance.fruitsToPurchase;

        textFruit.text = 
        valueFruitsAcquired
        + " / " + 
        valueFruitsToPurchase;
    }
}