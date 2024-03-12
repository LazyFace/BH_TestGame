using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoaderAuxiliar : MonoBehaviour
{
    public void ChangeScene(int buildIndex)
    { 
        LevelLoader.instance.ChangeScene(buildIndex);
    }

}
