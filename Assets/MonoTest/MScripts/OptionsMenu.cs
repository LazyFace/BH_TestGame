using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    private void Start()
    {
        float musicVolume;
        mixer.GetFloat("musicVolume", out musicVolume);
        //Debug.Log("Music volume: " + musicVolume);
        musicVolumeSlider.value = musicVolume;

        float sfxVolume;
        mixer.GetFloat("sfxVolume", out sfxVolume);
        //Debug.Log("SFX volume: " + sfxVolume);
        sfxVolumeSlider.value = sfxVolume;
    }

    public void SetMusicVolume(float musicVolume)
    {
        mixer.SetFloat("musicVolume", musicVolume);
    }

    public void SetSFXVolume(float sfxVolume)
    {
        mixer.SetFloat("sfxVolume", sfxVolume);
    }

}
