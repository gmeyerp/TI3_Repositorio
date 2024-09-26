using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeiraLevelManager : MonoBehaviour
{
    public static FeiraLevelManager instance;
    float time;
    float[] fruitTimer;
    [SerializeField] int numberOfFruits = 3;
    [SerializeField] List<GameObject> chosenFruits = new List<GameObject>();
    [SerializeField] List<GameObject> fruits = new List<GameObject>();
    [SerializeField] List<Stand> stands = new List<Stand>();
    public float NPCSpeed = 3f;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        { instance = this; }
        else { Destroy(gameObject); }

    }

    private void Start()
    {
        PickFruits();
        GiveChosenFruits();
        GiveRemainingFruits();
    }


    public void PickFruits()
    {
        for (int i = 0; i < numberOfFruits; i++)
        {
            int fruit = Random.Range(0, fruits.Count);
            chosenFruits.Add(fruits[fruit]);
            fruits.RemoveAt(fruit);
        }       
    }

    public void GiveChosenFruits()
    {
        for (int i = 0; i < numberOfFruits; i++)
        {
            int s = Random.Range(0, stands.Count);
            stands[s].PopulateStand(chosenFruits[i], i);
            stands.RemoveAt(s);
        }
    }

    public void GiveRemainingFruits()
    {
        foreach (Stand s in stands)
        {
            int fruit = Random.Range(0, fruits.Count);
            if (!s.hasFruit)
            {
                s.PopulateStand(fruits[fruit]);
            }
        }
    }
}
