using UnityEngine;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance;

    #endregion

    [SerializeField] private TextMeshProUGUI pointsCountingUI;

    private int points;

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1.0f;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;    
    }

    public void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;
        pointsCountingUI.text = points.ToString();
    }

    public void GameLost()
    {
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
