using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private Slider slider;

    [SerializeField] private TMP_Text weaponInfo;
    [SerializeField] private TMP_Text scoreText;

    [SerializeField] private GameObject finalScreen;
    [SerializeField] private GameObject pauseScreen;

    public void UpdatePlayerHealth(int currentHealth)
    {
        slider.value = currentHealth;
    }

    public void UpdateScore(int score) 
    {
        scoreText.text = $"{score}";
    }

    public void UpdateWeaponInfo(string gunName, int currentMagazineAmmo, int currentTotalAmmo) 
    {
        weaponInfo.text = $"{gunName}: {currentMagazineAmmo} / {currentTotalAmmo}";
    }

    public void ActivateGameOverScreen()
    {
        finalScreen.SetActive(true);
    }

    public void GamePauseScreen(int toggle)
    {
        if(toggle == 1)
        {
            pauseScreen.SetActive(true);
        }
        if(toggle == 0)
        {
            pauseScreen.SetActive(false);
        }
    }
}
