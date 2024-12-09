using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] bool isDodge;
    [SerializeField] bool isWater;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(gameObject.name);
            if (!isDodge)
            {
                GincanaLevelManager.instance.HitPlayer(gameObject.name);
            }
            else
            {
                GincanaLevelManager.instance.HitPlayer(ObstacleType.Dodge, gameObject.name);
            }
            if (isWater)
            {
                GincanaTutorial.instance.FallTutorial();
            }
        }        
    }

}
