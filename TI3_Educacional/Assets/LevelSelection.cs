using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] Toggle toggle;
    bool wasPlayed;

    public void TurnToggle()
    {
        wasPlayed = true;
        toggle.isOn = true;
        Debug.Log("Level Played");
    }
}
