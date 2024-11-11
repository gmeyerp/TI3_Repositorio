using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] bool isDodge;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(gameObject.name);
            if (!isDodge)
            {
                GincanaLevelManager.instance.HitPlayer();
            }
            else
            {
                GincanaLevelManager.instance.HitPlayer(ObstacleType.Dodge);
            }
        }        
    }
}
