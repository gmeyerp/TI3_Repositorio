using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileSettingToggle : MonoBehaviour
{
    [SerializeField] private ProfileInfo.Info info;

    private Toggle toggle;
    private void Awake()
    {
        toggle = GetComponent<Toggle>();
    }

    private void Start()
    {
        ProfileManager.AddListener(info, (value) =>
        {
            toggle.isOn = Convert.ToBoolean(value);
        });
    }

    public void SetBool(bool value)
    {
        ProfileManager.SetCurrent(info, value);
    }
}
