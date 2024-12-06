using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReportButton : MonoBehaviour
{
    private Button button;
    private void Start()
    {
        Button button = GetComponent<Button>();
        UpdateButton();
    }

    public void UpdateButton()
    {
        button.interactable = ProfileManager.IsManaging && IsEmailValid();
    }

    private bool IsEmailValid()
    {
        string email = ProfileManager.GetCurrent(ProfileInfo.Info.stringEmail).ToString();

        return (email != null && email.Contains('@'));
    }
}
