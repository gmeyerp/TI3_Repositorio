using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandSpotTrigger : MonoBehaviour
{
    [SerializeField] Stand stand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           StandComplete();
        }
    }

    public void StandComplete()
    {
        FeiraLevelManager.instance.CollectedFruit(stand.GetChosenFruit());
    }
}
