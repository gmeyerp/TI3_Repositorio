using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSound : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    public void AudioTrigger()
    {
        audioSource.Stop();
        audioSource.Play();
    }
}
