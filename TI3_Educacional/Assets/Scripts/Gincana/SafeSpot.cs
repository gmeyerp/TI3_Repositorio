using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeSpot : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GincanaLevelManager.instance.safeSpot = transform.position;
        }
    }
}
