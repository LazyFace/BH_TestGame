using UnityEngine;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance;

    #endregion

    [SerializeField] private UIHandler uiHandler;

    private int points = 0;

    private void Awake()
    {
        Instance = this;
        ChangeTimeScale(1f);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;    
    }

    public void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;
        uiHandler.UpdateScore(points);
    }

    public void GameLost()
    {
        ChangeTimeScale(0f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        uiHandler.ActivateGameOverScreen();
    }

    public void ChangeTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
        Debug.Log("Time Scale " + Time.timeScale);
    }
}
