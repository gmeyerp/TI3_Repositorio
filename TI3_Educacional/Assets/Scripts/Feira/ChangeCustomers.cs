using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ChangeCustomers : MonoBehaviour
{
    UnityEvent toChange = new UnityEvent();
    int difficulty;

    private void Start()
    {
        toChange.AddListener(FeiraLevelManager.instance.StartCustomers);
    }
    public void Activate()
    {
        FeiraLevelManager.instance.SetCustomerDifficulty(difficulty);
        toChange.Invoke();
    }

    public void ChangeTo(int i)
    {
        difficulty = i;
        Debug.Log((FeiraCustomers)i);
        toChange.RemoveAllListeners();
        toChange.AddListener(FeiraLevelManager.instance.StartCustomers);
    }
}
