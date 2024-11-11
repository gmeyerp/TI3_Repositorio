using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    [SerializeField] VrModeController vrController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            vrController.ExitVR();
        }
    }
}
