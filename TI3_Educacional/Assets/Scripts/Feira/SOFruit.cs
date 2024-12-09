using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fruit")]
public class SOFruit : ScriptableObject
{
    public GameObject prefab;
    public Sprite sprite;
    public AudioClip announceClip;
    public GameObject spritePrefab;
}
