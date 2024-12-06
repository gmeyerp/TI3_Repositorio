using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StandSpotTrigger : MonoBehaviour
{
    [SerializeField] Stand stand;
    SOFruit soFruit;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MiniGameManager.Instance.SetUsedTrigger(this);
            MiniGameTps.Instance.TakeLastPosition();
            MiniGameManager.Instance.NewPrice();
            MiniGameTps.Instance.TeleportToMiniGame();
            MiniGameFruitManager.Instance.SetUsedTrigger(this);
            MiniGameFruitManager.Instance.NewPrice();
            MiniGameFruitManager.Instance.AddFruits(stand.fruitInfo);
        }
    }

    public void StandComplete()
    {
        FeiraLevelManager.instance.CollectedFruit(stand.GetChosenFruit());
    }

    public void NPCInteraction()
    {
        //MiniGameManager.Instance.SetUsedTrigger(this);
        MiniGameTps.Instance.TakeLastPosition();
        //MiniGameManager.Instance.NewPrice();
        MiniGameTps.Instance.TeleportToMiniGame();
        MiniGameFruitManager.Instance.Infos();
        MiniGameFruitManager.Instance.SetUsedTrigger(this);
        MiniGameFruitManager.Instance.NewPrice();
        MiniGameFruitManager.Instance.AddFruits(stand.fruitInfo);
        FruitInfos.Instance.UpdateDisplayFruit();
    }
}
