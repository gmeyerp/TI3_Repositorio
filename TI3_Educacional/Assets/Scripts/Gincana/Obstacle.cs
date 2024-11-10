using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        GincanaLevelManager.instance.HitPlayer();
    }

    public void OnCollisionEnter(Collision collision)
    {
        GincanaLevelManager.instance.HitPlayer();        
    }
}
