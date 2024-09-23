using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPointCollider : MonoBehaviour
{
    [SerializeField] float rotateSpeed;
    [SerializeField] Rigidbody rb;

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        TriggerScore(other);
    }

    private static void TriggerScore(Collider other)
    {
        if (other.gameObject.CompareTag("Target") == true)
        {
           
            SpinTarget target = other.gameObject.GetComponent<SpinTarget>();
            GameTracker.instance.IncreaseScore(target.GetScore());
            target.DestroyTarget();
        }
    }

    public bool IsRigidbodySteady()
    {
        if (rb.angularVelocity.z > rotateSpeed)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
