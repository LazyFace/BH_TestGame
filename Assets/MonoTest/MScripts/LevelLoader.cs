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
        Debug.Log("Si entro aca" + buildIndex);
        transitions.SetTrigger("End");
        yield return new WaitForSeconds(2);
        SceneManager.LoadSceneAsync(buildIndex);
        transitions.SetTrigger("Start");
        Debug.Log("Si termino?");
        loandingCanvas.alpha = 0.0f;
    }

}
