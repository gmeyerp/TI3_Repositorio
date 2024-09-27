using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : MonoBehaviour
{
    [SerializeField] Transform[] fruitSpot;
    [SerializeField] GameObject interactSpot;
    int chosenFruitIndex = -1;
    public bool isChosen;
    public bool hasFruit = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopulateStand(GameObject fruit)
    {
        foreach (Transform spot in fruitSpot)
        {
            Instantiate(fruit, spot.position, fruit.transform.rotation, transform);
        }
        hasFruit = true;
        interactSpot.SetActive(false);
    }

    public void PopulateStand(GameObject fruit, int chosenFruitIndex)
    {
        foreach (Transform spot in fruitSpot)
        {
            Instantiate(fruit, spot.position, fruit.transform.rotation, transform);
        }
        hasFruit = true;
        isChosen = true;
        this.chosenFruitIndex = chosenFruitIndex;
        interactSpot.SetActive(true);
    }

    public int GetChosenFruit()
    {
        return chosenFruitIndex;
    }
}
