using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    private AudioMixer audioMixer;

    //Talvez passar esses sliders para um possivel UI Manager
    [SerializeField]
    private Slider masterSlider;
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider SFXSlider;

    // Fiz ser um Singleton, vai acabar sendo...
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    /* Pelo inspector da unity eh possivel expor parametros de cada grupo
     do AudioMixer (so clicar com o botao direito em cima do volume de cada um)*/

    // O nome dos parametros sao os mesmos dos grupos

    // Lembrem que 0 decibeis eh o valor mais alto de um mixer, acima disso o audio picota
    // -80db espero que seja inaudivel

    // Funcoes para alterar os valores dos parametros com base nos valores dos sliders

    public void changeMasterVolume()
    {
        audioMixer.SetFloat("Master", masterSlider.value);
    }

    public void changeMusicVolume()
    {
        audioMixer.SetFloat("Music", musicSlider.value);
    }

    public void changeSFXVolume()
    {
        audioMixer.SetFloat("SFX", SFXSlider.value);
    }

}
