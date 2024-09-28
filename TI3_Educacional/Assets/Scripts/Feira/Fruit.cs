using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] Color fruitColor;
    [SerializeField] MeshRenderer myRenderer;

    private void Start()
    {
        myRenderer.material.color = fruitColor;
    }
}
