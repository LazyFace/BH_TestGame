using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnPoints;

    private void Start()
    {
        SpawnZombie();
    }

    private void SpawnZombie()
    {
        GameObject zombieInstance = 
            ObjectPooler.Instance.SpawnFromPool
            ("Zombie", spawnPoints[0].gameObject.transform.position, spawnPoints[0].gameObject.transform.rotation);
    }
}
