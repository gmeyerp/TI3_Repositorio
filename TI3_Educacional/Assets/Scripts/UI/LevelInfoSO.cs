using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[CreateAssetMenu(fileName = "Level Info", menuName = "Level Info")]
public class LevelInfoSO : ScriptableObject
{
    public Color color;

    public string title;
    public string description;
    public Sprite preview;

    public string sceneName;

}
