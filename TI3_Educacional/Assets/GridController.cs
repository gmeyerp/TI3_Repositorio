using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridController : MonoBehaviour
{
    [SerializeField] GridLayout gridLayout;
    [SerializeField] RectTransform rectTransform;
    // Start is called before the first frame update
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        gridLayout = GetComponent<GridLayout>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
