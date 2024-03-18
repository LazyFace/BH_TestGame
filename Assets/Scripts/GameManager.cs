using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance;

    #endregion

    [SerializeField] private UIHandler uiHandler;

    private int points = 0;
    private bool isPause;
    private bool isPlayerAlive;

    private void Awake()
    {
        Instance = this;
        ChangeTimeScale(1f);
    }

    private void Start()
    {
        Cursor.visible = false;
        isPause = false;
        isPlayerAlive = true;
    }

    private void Update()
    {
        GamePause();
    }

    public void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;
        uiHandler.UpdateScore(points);
    }

    public void GameLost()
    {
        isPlayerAlive = false;
        ChangeTimeScale(0f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        uiHandler.ActivateGameOverScreen();
    }

    private void GamePause()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && isPlayerAlive)
        {
            if (!isPause)
            {
                isPause = true;
                Cursor.visible = true;
                ChangeTimeScale(0.0f);
                uiHandler.GamePauseScreen(1);
            }

            else if (isPause)
            {
                isPause = false;
                Cursor.visible = false;
                ChangeTimeScale(1.0f);
                uiHandler.GamePauseScreen(0);
            }
        }
    }

    public void ChangeTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
        //Debug.Log("Time Scale " + Time.timeScale);
    }
}
