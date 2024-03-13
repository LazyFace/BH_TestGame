using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;

    public void SetMusicVolume(float musicVolume)
    {
        mixer.SetFloat("musicVolume", musicVolume);
    }

    public void SetSFXVolume(float sfxVolume)
    {
        mixer.SetFloat("sfxVolume", sfxVolume);
    }

}
