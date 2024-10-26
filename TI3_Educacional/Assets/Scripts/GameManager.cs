using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Level Selection");
        ButtonClicked();
    }

    public void LevelInfo()
    {
        SceneManager.LoadScene("Level Info");
        ButtonClicked();
    }

    public void StartFeira()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.LoadScene("Feira");
        ButtonClicked();
    }
    public void InfoFeira()
    {
        SceneManager.LoadScene("Feira Info");
        ButtonClicked();
    }

    public void StartAccelerometer()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.LoadScene("Acelerometro");
        ButtonClicked();
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
