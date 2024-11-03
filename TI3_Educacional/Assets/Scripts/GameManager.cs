using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    #region Fases
    public void MiniGame()
    {
        SceneManager.LoadScene("Minigame");
        ButtonClicked();
    }
    public void StartFeira()
    {
        SceneManager.LoadScene("Feira");
        ButtonClicked();
    }
    #endregion

    #region Informacoes
    public void LevelInfo()
    {
        SceneManager.LoadScene("Level Info");
        ButtonClicked();
    }
    public void InfoFeira()
    {
        SceneManager.LoadScene("Feira Info");
        ButtonClicked();
    }
    public void InfoAccelerometer()
    {
        SceneManager.LoadScene("Accelerometer Info");
        ButtonClicked();
    }
    #endregion
    
    #region Menu
    public void BackToStart()
    {
        SceneManager.LoadScene("Start");
        ButtonClicked();
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

    public void LevelSelection()
    {
        SceneManager.LoadScene("Level Selection");
        ButtonClicked();
    }
    #endregion



    public void StartAccelerometer()
    {
        SceneManager.LoadScene("Acelerometro");
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
