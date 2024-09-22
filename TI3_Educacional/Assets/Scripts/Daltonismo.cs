using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Daltonismo : MonoBehaviour
{
    //Referencia butao e texto
    public Button buttonDaltonism;
    public TextMeshProUGUI textDaltonism;

    //Referencia textos
    public string[] texts = {"DESATIVADO", "PROTANOTOPIA", "DEUTRANOTOPIA", "TRITANOTOPIA"};
    
    //Referencia cor
    // public Color[] color;
    private int indice = 0;
 
    void Start()
    {
        buttonDaltonism.onClick.AddListener(ChangeIndice);

        TextAndColor();
    }

    void ChangeIndice()
    {
        indice = (indice + 1) % texts.Length; // Volta para o primeiro texto quando chegar no limite
        TextAndColor();
    }

    void TextAndColor()
    {
        textDaltonism.text = texts[indice];

        // textDaltonism.color = color[indice];
    }
}
