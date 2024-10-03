using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fruit")]
public class SOFruit : ScriptableObject
{
    [SerializeField] GameObject prefab;
    [SerializeField] Sprite sprite;
    [SerializeField] AudioClip clip;
}
