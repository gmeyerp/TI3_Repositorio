using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    [SerializeField] Color[] tabColors;
    [SerializeField] Image[] tab;
    [SerializeField] GameObject[] configScrollAreas;
    [SerializeField] Image configMainPanel;
    public void Start()
    {
        for (int i = 0; i < tab.Length; i++)
        {
            tab[i].color = tabColors[i];
        }
        gameObject.SetActive(false);
    }

    public void SwitchTab(int number)
    {
        gameObject.SetActive(true);
        configMainPanel.color = tab[number].color;
        foreach (GameObject config in configScrollAreas)
        {
            config.SetActive(false);
        }
        configScrollAreas[number].SetActive(true);
    }
}
