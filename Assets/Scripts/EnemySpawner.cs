using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, ISpawnable
{
    public void SpawnEnemy(string enemy, Transform enemyPosition)
    {
        ObjectPooler.Instance.SpawnFromPool(enemy, enemyPosition.position, enemyPosition.rotation);
    }
}
