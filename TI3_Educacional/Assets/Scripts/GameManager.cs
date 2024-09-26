using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void Config()
    {
        SceneManager.LoadScene("Config");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void BackToStart()
    {
        SceneManager.LoadScene("Start");
    }

    public void LevelSelection()
    {
        SceneManager.LoadScene("Level Selection");
    }

    public void LevelInfo()
    {
        SceneManager.LoadScene("Level Info");
    }

    public void StartFeira()
    {
        SceneManager.LoadScene("Feira");
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            // Sai do modo de jogo do editor
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Sai da aplicação
            Application.Quit();
        #endif
    }
}
