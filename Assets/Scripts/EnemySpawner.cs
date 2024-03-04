using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public void SpawnEnemy(ObjectPooler.ObjectsToSpawn enemy, Transform spawnPosition, Action callback)
    {
        GameObject enemyInstance = ObjectPooler.Instance.SpawnFromPool(enemy, spawnPosition.position, spawnPosition.rotation);
        enemyInstance.GetComponent<EnemyController>().Initialize(callback);
    }
}
