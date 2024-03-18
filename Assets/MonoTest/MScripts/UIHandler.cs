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

    [SerializeField] private GameObject finalScreen;
    [SerializeField] private GameObject pauseScreen;

    [SerializeField] private Material healthBarMaterial;
    [SerializeField] private Material regenHealthBarMaterial;

    private Coroutine ReturnHealthBarToNormalState;

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
