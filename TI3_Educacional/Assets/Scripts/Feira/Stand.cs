using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : MonoBehaviour
{
    [SerializeField] SOFruit fruitInfo;
    [SerializeField] Transform[] fruitSpot;
    [SerializeField] GameObject interactSpot;
    [SerializeField] AudioClip fruitAudio;
    [SerializeField] AudioSource audioSource;
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

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7 && isChosen) //PlayerLayer
        {
            audioSource.PlayOneShot(fruitInfo.announceClip);
        }
    }

    public void PopulateStand(SOFruit fruit)
    {
        fruitInfo = fruit;
        foreach (Transform spot in fruitSpot)
        {
            Instantiate(fruit.prefab, spot.position, fruit.prefab.transform.rotation, transform);
        }
        hasFruit = true;
        interactSpot.SetActive(false);
    }

    public void PopulateStand(SOFruit fruit, int chosenFruitIndex)
    {
        fruitInfo = fruit;
        foreach (Transform spot in fruitSpot)
        {
            Instantiate(fruit.prefab, spot.position, fruit.prefab.transform.rotation, transform);
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
