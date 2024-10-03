using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Gerenciador_Audio : MonoBehaviour
{
    static private Gerenciador_Audio instance;

    [SerializeField] private AudioClip musica;
    [SerializeField] private bool ApenasUmaVez;

    [SerializeField] private AudioSource gerenciadorMusicas;
    [SerializeField] private AudioSource gerenciadorSFX;
    [SerializeField] private AudioMixer audioMixer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            TocarPredefinida();
        }
        else
        {
            instance.musica = musica;
            instance.ApenasUmaVez = ApenasUmaVez;
            TocarPredefinida();
            Destroy(gameObject);
        }
    }


    static public _SFX SFX => instance.sFX;
    [SerializeField] public _SFX sFX;
    [System.Serializable]
    public class _SFX
    {
        [SerializeField] public AudioClip buttonClick;
    }


    static public void TocarSFX(AudioClip sfx)
    => instance._TocarSFX(sfx);
    private void _TocarSFX(AudioClip sfx)
    => gerenciadorSFX.PlayOneShot(sfx);


    static public void TocarPredefinida()
    => instance._TocarPredefinida();
    private void _TocarPredefinida()
    {
        if (ApenasUmaVez) TocarMusicaUmaVez(musica);
        else TocarMusicaEmLoop(musica);
    }

    static public void TocarMusicaUmaVez(AudioClip musica)
    => instance._TocarMusicaUmaVez(musica);
    private void _TocarMusicaUmaVez(AudioClip musica)
    {
        gerenciadorMusicas.loop = false;
        gerenciadorMusicas.clip = musica;
        gerenciadorMusicas.Play();
    }

    static public void TocarMusicaEmLoop(AudioClip musica)
    => instance._TocarMusicaDeFundo(musica);
    private void _TocarMusicaDeFundo(AudioClip musica)
    {
        if (gerenciadorMusicas.clip == musica && gerenciadorMusicas.isPlaying) return;

        gerenciadorMusicas.loop = true;
        gerenciadorMusicas.clip = musica;
        gerenciadorMusicas.Play();
    }

    static public void PararMusica()
    => instance._PararMusica();
    private void _PararMusica()
    { gerenciadorMusicas.Stop(); }

    static private float ConverteVolume(float porcentagem)
    {
        float valorDecimal = porcentagem / 100;
        // o humano escuta em uma escala logarítmica
        // a conta abaixo converte o valor linear para a escala humana
        return Mathf.Log10(valorDecimal) * 20;
    }

    static private int volumeMusica = 100;
    static public int GetVolumeMusica() => volumeMusica;
    static public void SetVolumeMusica(float porcentagem)
    => instance._SetVolumeMusica(porcentagem);
    private void _SetVolumeMusica(float porcentagem)
    {
        float volume = ConverteVolume(porcentagem);
        audioMixer.SetFloat("VolumeMusica", volume);
    }

    static private int volumeSFX = 100;
    static public int GetVolumeSFX() => volumeSFX;
    static public void SetVolumeSFX(float porcentagem)
    => instance._SetVolumeSFX(porcentagem);
    private void _SetVolumeSFX(float porcentagem)
    {
        float volume = ConverteVolume(porcentagem);
        audioMixer.SetFloat("VolumeSFX", volume);
    }
}
