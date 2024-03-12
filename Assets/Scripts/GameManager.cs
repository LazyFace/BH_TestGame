using UnityEngine;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance;

    #endregion

    [SerializeField] private TextMeshProUGUI pointsCountingUI;
    [SerializeField] private GameObject finishScreen;

    private int points;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1.0f;
    }

    public void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;
        pointsCountingUI.text = points.ToString();
    }

    public void GameLost()
    {
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        finishScreen.SetActive(true);
    }
}
