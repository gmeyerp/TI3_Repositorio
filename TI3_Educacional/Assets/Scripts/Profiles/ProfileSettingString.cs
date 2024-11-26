using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileSettingString : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    [SerializeField] private ProfileInfo.Info info;

    private TMP_InputField input;
    private void Awake()
    {
        input = GetComponent<TMP_InputField>();
    }

    private void Start()
    {
        ProfileManager.AddListener(info, (value) =>
        {
            input.text = value.ToString();
            UpdateText(value);
        });
    }

    private void UpdateText(object value)
    {
        if (text == null) return;

        text.text = value.ToString();
    }

    public void SetString(string value)
    {
        ProfileManager.SetCurrent(info, value);
        UpdateText(value);
    }
}
