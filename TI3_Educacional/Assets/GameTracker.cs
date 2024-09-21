using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTracker : MonoBehaviour
{
    public static GameTracker instance;
    int score;
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
    }

    public int GetScore()
    {
        return score;
    }
}
