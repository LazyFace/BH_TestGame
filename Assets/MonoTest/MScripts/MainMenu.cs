using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private AudioMixer mixer;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
