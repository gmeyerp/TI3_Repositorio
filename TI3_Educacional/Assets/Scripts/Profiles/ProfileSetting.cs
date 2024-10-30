using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileSetting : MonoBehaviour
{
    [SerializeField] private ProfileManager profileManager;

    [SerializeField] private ProfileInfo.Info info;

    public void SetInt(int value) => profileManager.SetCurrent(info, value);
    public void SetFloat(float value) => profileManager.SetCurrent(info, value);
    public void SetBool(bool value) => profileManager.SetCurrent(info, value);
    public void SetString(string value) => profileManager.SetCurrent(info, value);
}
