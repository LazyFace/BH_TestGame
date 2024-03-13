using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator transitions;
    [SerializeField] private CanvasGroup loandingCanvas;

    public static LevelLoader instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeScene(int buildIndex)
    {
        StartCoroutine(LoadLevelCoroutine(buildIndex));
    }

    private IEnumerator LoadLevelCoroutine(int buildIndex)
    {
        transitions.SetTrigger("End");
        if (buildIndex == 0)
        {
            yield return new WaitForSecondsRealtime(0.75f);
            GameManager.Instance.ChangeTimeScale(1);      
        }
        else { yield return new WaitForSecondsRealtime(2); }
        
        SceneManager.LoadSceneAsync(buildIndex);
        transitions.SetTrigger("Start");
        loandingCanvas.alpha = 0.0f;
    }

}
