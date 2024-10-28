using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] TweenFade fade;
    public IEnumerator ChangeScene(string sceneName, ScreenOrientation orientation)
    {
        if (fade != null)
        {
            LeanTween.alpha(gameObject, 0, 0f);
            LeanTween.alpha(gameObject, 1, fade.time).setOnComplete(() => Screen.orientation = orientation);
        }
        else
        {
            Screen.orientation = orientation;
        }
        
        yield return new WaitForSeconds(1f + fade.time);
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
        StartCoroutine("Level Selection", ScreenOrientation.Portrait);
    }


    public void LevelInfo()
    {
        SceneManager.LoadScene("Level Info");
        ButtonClicked();
    }

    public void StartFeira()
    {
        ButtonClicked();
        StartCoroutine("Feira", ScreenOrientation.LandscapeLeft);
        
    }
    public void InfoFeira()
    {
        SceneManager.LoadScene("Feira Info");
        ButtonClicked();
    }

    public void StartAccelerometer()
    {
        ButtonClicked();
        StartCoroutine("Acelerometro", ScreenOrientation.LandscapeLeft);
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
