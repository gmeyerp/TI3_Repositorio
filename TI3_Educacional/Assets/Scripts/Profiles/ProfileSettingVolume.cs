using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileSettingVolume : MonoBehaviour
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
        switch (info)
        {
            case ProfileInfo.Info.intGeneralVolume:
                Gerenciador_Audio.SetVolumeGeral(value);
                break;

            case ProfileInfo.Info.intVoiceVolume:
                Gerenciador_Audio.SetVolumeVozes(value);
                break;

            case ProfileInfo.Info.intSfxVolume:
                Gerenciador_Audio.SetVolumeSFX(value);
                break;

            default:
                Debug.LogError($"{transform.parent.name}/{name}: Slider de volume configurado errado");
                break;
        }
        ProfileManager.SetCurrent(info, value);
        UpdateText(value);
    }
}
