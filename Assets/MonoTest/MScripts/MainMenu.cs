using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

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
    public void MapSelected(int map)
    {
        LevelLoader.instance.ChangeScene(map);
    }

    /*
    public void PlayersNumber(int players)
    {
        Debug.Log("Number of players = " + players);

        //GameManager function to instantiate the number of players (Max 2 players)
    }*/
}
