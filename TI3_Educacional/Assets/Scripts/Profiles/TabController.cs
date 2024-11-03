using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    [SerializeField] Image[] tab;
    [SerializeField] GameObject[] configScrollAreas;
    [SerializeField] Image configMainPanel;

    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI levelInfo;
    [SerializeField] Image levelPreview;
    [SerializeField] LevelInfoSO[] infos;

    public Button playButton;
    public string sceneName;
    public void Start()
    {
        for (int i = 0; i < tab.Length; i++)
        {
            tab[i].color = infos[i].color;
        }
        gameObject.SetActive(false);
    }

    public void SwitchTab(int number)
    {
        Gerenciador_Audio.TocarSFX(Gerenciador_Audio.SFX.buttonClick);
        //troca da aba de configuracao
        configMainPanel.color = infos[number].color;

        foreach (GameObject config in configScrollAreas)
        {
            config.SetActive(false);
        }
        configScrollAreas[number].SetActive(true);
        SwitchLevel(number);
        ChangeSize(tab[number].gameObject);
        ChangeSize(configMainPanel.gameObject);
        sceneName = infos[number].sceneName;
    }


    public void ChangeSize(GameObject gameObject)
    {
        LeanTween.scale(gameObject, Vector3.one * 0.9f, 0f);
        LeanTween.scale(gameObject, Vector3.one, 0.2f).setEaseInOutBounce();
    }

    public void SwitchLevel(int number)
    {
        playButton.interactable = infos[number].playButtonState;
        playButton.image.color = infos[number].playButtonColor;
        title.text = infos[number].title;
        levelInfo.text = infos[number].description;
        levelPreview.sprite = infos[number].preview;
        ChangeSize(levelPreview.gameObject);
    }

    public void ConfigOn()
    {
        gameObject.SetActive(true);
    }

    public void ConfigOff()
    {
        gameObject.SetActive(false);
    }
}
