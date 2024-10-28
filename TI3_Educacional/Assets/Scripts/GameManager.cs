using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] CanvasGroup fadeGroup;
    [SerializeField] float fadeOutTime = 0.5f;
    [SerializeField] float fadeInTime = 0.5f;

    private void Start()
    {
        Debug.Log("Teste");
        fadeGroup.alpha = 1f;
        LeanTween.init();
        LeanTween.alphaCanvas(fadeGroup, 1, 0f);
        LeanTween.alphaCanvas(fadeGroup, 0, fadeInTime);
    }
    public IEnumerator ChangeScene(string sceneName, ScreenOrientation orientation)
    {
        if (fadeGroup != null)
        {
            LeanTween.alphaCanvas(fadeGroup, 0, 0f);
            LeanTween.alphaCanvas(fadeGroup, 1, fadeOutTime).setOnComplete(() => Screen.orientation = orientation);
        }
        else
        {
            Screen.orientation = orientation;
        }
        
        yield return new WaitForSeconds(1f + fadeOutTime);
        SceneManager.LoadScene(sceneName);
    }
    public void Config()
    {
        SceneManager.LoadScene("Config");
        ButtonClicked();
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
        ButtonClicked();
    }

    public void BackToStart()
    {
        SceneManager.LoadScene("Start");
        ButtonClicked();
    }

    public void LevelSelection()
    {
        ButtonClicked();
        StartCoroutine(ChangeScene("Level Info", ScreenOrientation.Portrait));
    }


    public void LevelInfo()
    {
        SceneManager.LoadScene("Level Info");
        ButtonClicked();
    }

    public void StartFeira()
    {
        ButtonClicked();
        StartCoroutine(ChangeScene("Feira", ScreenOrientation.LandscapeLeft));
        
    }
    public void InfoFeira()
    {
        SceneManager.LoadScene("Feira Info");
        ButtonClicked();
    }

    public void StartAccelerometer()
    {
        ButtonClicked();
        StartCoroutine(ChangeScene("Acelerometro", ScreenOrientation.LandscapeLeft));
    }

    public void InfoAccelerometer()
    {
        SceneManager.LoadScene("Accelerometer Info");
        ButtonClicked();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ButtonClicked();
    }

    public void ButtonClicked()
    {
        Gerenciador_Audio.TocarSFX(Gerenciador_Audio.SFX.buttonClick);
    }

    

    public void Quit()
    {
        ButtonClicked();
        #if UNITY_EDITOR
        // Sai do modo de jogo do editor
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Sai da aplicação
            Application.Quit();
        #endif
    }
}
