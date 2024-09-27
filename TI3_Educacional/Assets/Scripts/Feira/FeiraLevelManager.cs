using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FeiraLevelManager : MonoBehaviour
{
    public static FeiraLevelManager instance;
    [SerializeField] List<Sprite> fruitSprites = new List<Sprite>();
    [SerializeField] Image[] chosenFruitsImages;
    [SerializeField] Image[] checkImage;
    float[] fruitTimer;
    bool[] collectedFruit = new bool[3];
    [SerializeField] int numberOfFruits = 3;
    [SerializeField] List<GameObject> chosenFruits = new List<GameObject>();
    [SerializeField] List<GameObject> fruits = new List<GameObject>();
    [SerializeField] List<Stand> stands = new List<Stand>();
    public float NPCSpeed = 3f;

    [SerializeField] GameObject gameplayCanvas;
    float timer;
    bool isBestTime;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI hitsText;
    [SerializeField] int hitTimes;
    [SerializeField] float invincibleTimer;
    [SerializeField] GameObject victoryCanvas;
    float bestTime = 90;
    bool isInvulnerable;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        { instance = this; }
        else { Destroy(gameObject); }
        timer = 0; 
        //bestTime = pegar do GameManager

    }

    private void Start()
    {
        PickFruits();
        GiveChosenFruits();
        GiveRemainingFruits();
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }


    public void PickFruits()
    {
        for (int i = 0; i < numberOfFruits; i++)
        {
            int fruit = Random.Range(0, fruits.Count);
            Debug.Log(fruit);
            chosenFruits.Add(fruits[fruit]);
            chosenFruitsImages[i].sprite = fruitSprites[fruit];
            fruits.RemoveAt(fruit);
            fruitSprites.RemoveAt(fruit);
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

    public void PlayerHit()
    {
        hitTimes++;
        SetInvincible(invincibleTimer);
    }

    IEnumerable SetInvincible(float timer)
    {
        yield return timer;
        isInvulnerable = true;
    }

    public void CollectedFruit(int collectedFruit)
    {
        this.collectedFruit[collectedFruit] = true;
        checkImage[collectedFruit].gameObject.SetActive(true);
        if (HasWin())
        {
            Victory();
        }
    }

    public bool HasWin()
    {
        bool win = true;
        foreach (bool b in collectedFruit)
        {
            if (b == false)
            {
                win = false;
            }
        }

        return win;
    }

    public void Victory()
    {
        if (timer < bestTime)
        {
            Debug.Log("New record"); //ajustar feedbacks no GameManager depois
            bestTime = timer;
            isBestTime = true;
        }
        gameplayCanvas.SetActive(false);
        victoryCanvas.SetActive(true);
        if (isBestTime)
        {
            timerText.gameObject.SetActive(true);
            timerText.text = "Recorde: " + timer.ToString("0.0");
        }
        hitsText.text = "Batidas: " + hitTimes.ToString();
    }
}
