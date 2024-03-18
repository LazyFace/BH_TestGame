using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image sliderImage;

    [SerializeField] private TMP_Text weaponInfo;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text roundCountText;
    [SerializeField] private TMP_Text finalScoreText;

    [SerializeField] private GameObject searchAmmoText;
    [SerializeField] private GameObject finalScreen;
    [SerializeField] private GameObject pauseScreen;

    [SerializeField] private Material healthBarMaterial;
    [SerializeField] private Material regenHealthBarMaterial;

    private Coroutine ReturnHealthBarToNormalState;
    private int scoreCounter;

    public void UpdatePlayerHealth(int currentHealth)
    {
        if(ReturnHealthBarToNormalState != null) 
        { 
            StopCoroutine(ReturnHealthBarToNormalState);
        }
        slider.value = currentHealth;
        sliderImage.material = regenHealthBarMaterial;
        ReturnHealthBarToNormalState = StartCoroutine(ResetHealthBarMaterial());
    }

    private IEnumerator ResetHealthBarMaterial()
    {
        yield return new WaitForSeconds(1);

        sliderImage.material = healthBarMaterial;
    }

    public void UpdateScore(int score) 
    {
        scoreText.text = $"{score}";
    }

    public void UpdateWeaponInfo(string gunName, int currentMagazineAmmo, int currentTotalAmmo) 
    {
        weaponInfo.text = $"{gunName}: {currentMagazineAmmo} / {currentTotalAmmo}";
    }

    public void ActivateGameOverScreen(int score)
    {
        finalScreen.SetActive(true);
        finalScoreText.text = "YOUR SCORE: " + score;  
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

    public void UpdateRoundCount(int round)
    {
        roundCountText.text = "ROUND " + round;
    }

    public void ShowAmmoMessage()
    {
        StartCoroutine(SearchAmmoAdvise());
    }

    private IEnumerator SearchAmmoAdvise()
    {
        searchAmmoText.SetActive(true);
        yield return new WaitForSeconds(3);
        searchAmmoText.SetActive(false);
    }
}
