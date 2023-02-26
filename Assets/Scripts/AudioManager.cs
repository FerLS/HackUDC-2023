using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public  static  AudioManager Instance { get; private set; }
    
    private AudioSource _audioSource;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayOneShot(AudioClip clip,float volume)
    {
        _audioSource.pitch = Random.Range(0.95f,1.05f);
        _audioSource.PlayOneShot(clip,volume);
    }

    public void Mute(Slider slider)
    {
        _audioSource.volume = slider.value;
    }
}
