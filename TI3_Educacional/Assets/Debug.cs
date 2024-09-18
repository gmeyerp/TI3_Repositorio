using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Management;


public class Debug : MonoBehaviour
{
    [SerializeField] Text text;

    private void Awake()
    {
        StartCoroutine("StartVR");
    }
    Coroutine StartVR()
    {
        return StartCoroutine(startVRRoutine());
        IEnumerator startVRRoutine()
        {
            // Add error handlers for both Instance and Manager
            var xrManager = XRGeneralSettings.Instance.Manager;
            if (!xrManager.isInitializationComplete)
                yield return xrManager.InitializeLoader();
            if (xrManager.activeLoader != null)
                xrManager.StartSubsystems();
            // Add else with error handling
        }
    }
    // Update is called once per frame
    void Update()
    {
        text.text = Time.deltaTime.ToString();
    }
}
