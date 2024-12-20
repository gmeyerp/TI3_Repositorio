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
    [SerializeField] TabController tabController;
    [SerializeField] GameObject confirmationPanel;
    [SerializeField] VrModeController vrModeController;
    

    private void Start()
    {
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

        if (AnalyticsTest.instance != null)
        {
            AnalyticsTest.instance.AddAnalytics(gameObject.name, "Duração Cena", SceneManager.GetActiveScene().name + " para " + sceneName +
            ": " + (Time.time - AnalyticsTest.instance.sceneTimer).ToString());
            AnalyticsTest.instance.sceneTimer = Time.time;
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

    public void CloseWindow(GameObject close)
    {
        close.SetActive(false);
        ButtonClicked();
    }

    public void OpenWindow(GameObject close)
    {
        close.SetActive(true);
        ButtonClicked();
    }

    public void LevelSelection()
    {
        //if (AnalyticsTest.instance != null)
        //{
        //    AnalyticsTest.instance.Save();
        //}
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
    public void StartLevel()
    {
        ButtonClicked();
        if (tabController.sceneName == "None" || tabController.sceneName == null)
        {
            Color original = tabController.playButton.image.color;
            LeanTween.color(tabController.playButton.gameObject, Color.red, 0.2f);
            LeanTween.color(tabController.playButton.gameObject, original, 0.2f).setDelay(0.2f);
        }
        else
        {
            StartCoroutine(ChangeScene(tabController.sceneName, ScreenOrientation.LandscapeLeft));
        }
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

    public void ConfirmationPanelToggle()
    {
        if (confirmationPanel.activeSelf)
        {
            confirmationPanel.SetActive(false);
            HideChangeSize(confirmationPanel);
        }
        else
        {
            confirmationPanel.SetActive(true);
            PopChangeSize(confirmationPanel);
        }
        ButtonClicked();
    }

    public void ButtonClicked()
    {
        Gerenciador_Audio.TocarSFX(Gerenciador_Audio.SFX.buttonClick);
    }

    public void PopChangeSize(GameObject gameObject)
    {
        LeanTween.scale(gameObject, Vector3.zero, 0f);
        LeanTween.scale(gameObject, Vector3.one, 0.2f).setEaseInOutBounce();
    }

    public void HideChangeSize(GameObject gameObject)
    {
        LeanTween.scale(gameObject, Vector3.one, 0);
        LeanTween.scale(gameObject, Vector3.zero, 0.2f);
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

    public void UnpauseVR()
    {
        ButtonClicked();
        StartCoroutine(Unpause());
    }

    public IEnumerator Unpause()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        yield return new WaitForEndOfFrame();
        vrModeController.isPaused = false;
        vrModeController.EnterVR();
    }

    public void ReloadCurrentScene()
    {
        ButtonClicked();
        StartCoroutine(ChangeScene(SceneManager.GetActiveScene().name, ScreenOrientation.LandscapeLeft));
    }
}
