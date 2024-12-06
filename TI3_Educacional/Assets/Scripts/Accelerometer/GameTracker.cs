using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTracker : MonoBehaviour
{
    public static GameTracker instance;
    [SerializeField] TextMeshProUGUI scoreText;
    int score;
    int misses;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        { 
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void IncreaseScore(int score)
    {
        this.score += score;
        scoreText.text = this.score.ToString();
    }

    public int GetScore()
    {
        return score;
    }

    public int GetMisses()
    {
        return misses;
    }

    public void AddMiss()
    {
        misses++;
    }
}
