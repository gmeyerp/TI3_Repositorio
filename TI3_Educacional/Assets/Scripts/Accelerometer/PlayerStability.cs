using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStability : MonoBehaviour
{
    [SerializeField] float stableAngle;
    public bool isStable;


    // Update is called once per frame
    void Update()
    {
        if (Input.acceleration.x * 90  > stableAngle || Input.acceleration.x * 90 < -stableAngle)
        {
            isStable = false;
        }
        else
        {
            isStable = true;
        }        
    }
}
