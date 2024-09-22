using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarrouselSoundSprite : MonoBehaviour
{
    public GameObject[] images; // Manipula as imagens
    public Slider slider; // Manipula a slider

    public void Start()
    {
        slider.value = 0.5f;
        SwitchImage();
    }

    void Update()
    {
        SwitchImage();
    }

    void SwitchImage()
    {
        foreach (GameObject img in images)
        {
            img.SetActive(false); // Desativando todas as imagens
        }

        if(slider.value == 0f)
        {
            // Ativando primeira imagem
            images[0].SetActive(true);
        }
        else if(slider.value > 0f && slider.value < 0.4f)
        {
            // Ativando segunda imagem
            images[1].SetActive(true);
        }
        else if(slider.value > 0.4f && slider.value < 0.8f)
        {
            // Ativando terceira imagem
            images[2].SetActive(true);
        }
        else
        {
            // Ativando quarta imagem
            images[3].SetActive(true);
        }

    }

}
