using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CustomerTrigger : MonoBehaviour
{
    [SerializeField] FeiraLevelManager manager;
    [SerializeField] FeiraTutorial tutorial;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            if (manager != null && manager.isTutorial && manager.GetCustomerDifficulty() != FeiraCustomers.Nenhum)
            {
                tutorial.CustomerTrigger();
                Destroy(gameObject);
            }
        }
    }
}
