using UnityEngine;

public class LevelLoaderAuxiliar : MonoBehaviour
{
    public void LoadScene(int buildIndex)
    {
        //Debug.Log("Si entro " + buildIndex);
        LevelLoader.instance.ChangeScene(buildIndex);
    }

}
