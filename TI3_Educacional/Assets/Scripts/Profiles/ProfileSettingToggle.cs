using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileSettingToggle : MonoBehaviour
{
    [SerializeField] private ProfileManager profileManager;
    [SerializeField] private ProfileInfo.Info info;

    private Toggle toggle;
    private void Awake()
    {
        toggle = GetComponent<Toggle>();
    }

    private void Start()
    {
        profileManager.AddListener(info, (value) =>
        {
            toggle.isOn = Convert.ToBoolean(value);
        });
    }

    public void SetBool(bool value)
    {
        profileManager.SetCurrent(info, value);
    }
}
