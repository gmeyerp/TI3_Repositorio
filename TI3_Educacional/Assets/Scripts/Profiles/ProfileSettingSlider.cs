using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileSettingSlider : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    [SerializeField] private ProfileInfo.Info info;

    private Slider slider;
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        ProfileManager.AddListener(info, (value) =>
        {
            slider.value = Convert.ToSingle(value);
            UpdateText(value);
        });
    }

    private void UpdateText(object value)
    {
        if (text == null) return;

        text.text = slider.wholeNumbers ? value.ToString() : string.Format("{0:N2}", value);
    }

    public void SetFloat(float value)
    {
        ProfileManager.SetCurrent(info, value);
        UpdateText(value);
    }
}
