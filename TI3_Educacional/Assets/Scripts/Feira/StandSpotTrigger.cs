using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StandSpotTrigger : MonoBehaviour
{
    [SerializeField] Stand stand;
    private Vector3 lastPosition;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MiniGameManager.instance.TakeLastPosition();
            MiniGameManager.instance.NewPrice();
            MiniGameManager.instance.TeleportToMiniGame();
        }
    }

    public void StandComplete()
    {
        FeiraLevelManager.instance.CollectedFruit(stand.GetChosenFruit());
    }
}
