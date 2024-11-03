using UnityEngine;
using TMPro;


public class MgSceneWin : MonoBehaviour
{
    [SerializeField] private TMP_Text victoryText;
    [SerializeField] private TMP_Text victoryTime;

    public float time;
    //private bool stopTime;


    void Start()
    {
        PlayerRayCast.Instance.maxDistance = 100.0f;
        MiniGameManager.Instance.Infos();
        MiniGameManager.Instance.NewPrice();
        MiniGameManager.Instance.SpawnItens();
        InvokeRepeating("IncreaseTime", 1f, 1f);
        victoryText.enabled = false;
        victoryTime.enabled = false;
    }

    void WinCondicion()
    {
        if(MiniGameManager.Instance.coinsAcquired == MiniGameManager.Instance.coinsToPurchase)
        {
            CancelInvoke("IncreaseTime");
            MiniGameManager.Instance.DestroycoinsActive();
            MiniGameManager.Instance.StopAllCoroutines();
            CoinInfos.Instance.textCoin.enabled = false;
            victoryText.enabled = true;
            victoryTime.enabled = true;
        }
    }

    void TimeToComplete(float timer)
    {
        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);

        victoryTime.text = string.Format("TEMPO - {0:00}:{1:00}", minutes, seconds);
    }

    void IncreaseTime()
    {
        if(time < 0f) return;

        time++;
        TimeToComplete(time);
    }

    void Update()
    {
        WinCondicion();
    }
}
