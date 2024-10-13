using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StandSpotTrigger : MonoBehaviour
{
    [SerializeField] Stand stand;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MiniGameManager.Instance.SetUsedTrigger(this);
            MiniGameManager.Instance.TakeLastPosition();
            MiniGameManager.Instance.NewPrice();
            MiniGameManager.Instance.TeleportToMiniGame();
        }
    }

    public void StandComplete()
    {
        FeiraLevelManager.instance.CollectedFruit(stand.GetChosenFruit());
    }
}
