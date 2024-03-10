using System.Collections;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private int currentRound = 1;
    [SerializeField] private float timeBetweenRounds = 10f;
    private int enemiesRemaining;

    [SerializeField] private EnemySpawner spawner;

    [SerializeField] private GameObject[] enemySpawnPoints;

    [SerializeField] private Wave_SO normalWave;
    [SerializeField] private Wave_SO zombiesWave;
    [SerializeField] private Wave_SO skeletonsWave;
    [SerializeField] private Wave_SO ghostsWave;

    private int roundType = 1;

    private void Start()
    {
        StartRound();
    }

    private void StartRound()
    {
        Wave_SO currentWave = SelectWaveType();

        StartCoroutine(GenerateEnemies(ObjectPooler.ObjectsToSpawn.ZOMBIE, currentWave.numZombies));
        StartCoroutine(GenerateEnemies(ObjectPooler.ObjectsToSpawn.SKELETON, currentWave.numSkeletons));
        StartCoroutine(GenerateEnemies(ObjectPooler.ObjectsToSpawn.GHOST, currentWave.numGhosts));
    }

    private IEnumerator WaitAndStartNextRound(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        currentRound++;
        StartRound();
    }

    public void EnemyDefeated()
    {
        enemiesRemaining--;

        if (enemiesRemaining <= 0)
        {
            StartCoroutine(WaitAndStartNextRound(timeBetweenRounds));
        }
    }

    private Wave_SO SelectWaveType()
    {
        Wave_SO selectedWave = normalWave;

        if (currentRound % 5 == 0)
        {
            switch (roundType)
            {
                case 1:
                    selectedWave = zombiesWave;
                    roundType++;
                    break;
                case 2:
                    selectedWave = skeletonsWave;
                    roundType++;
                    break;
                case 3:
                    selectedWave = ghostsWave;
                    roundType++;
                    break;
            }
            if(roundType > 3)
            {
                roundType = 1;
            }
        }

        enemiesRemaining = selectedWave.numZombies + selectedWave.numSkeletons + selectedWave.numGhosts;

        return selectedWave;
    }

    private IEnumerator GenerateEnemies(ObjectPooler.ObjectsToSpawn enemyToSpawn, int enemiesToSpawn)
    {
        int lastSpawnIndex = -1;

        for (int i = 0; i < (enemiesToSpawn + (1 * currentRound)); i++)
        {
            int spawnIndex = GetUniqueSpawnIndex(lastSpawnIndex, enemySpawnPoints.Length);
            lastSpawnIndex = spawnIndex;
            spawner.SpawnEnemy(enemyToSpawn, enemySpawnPoints[spawnIndex].transform, EnemyDefeated);
            yield return new WaitForSeconds(1f);
        }
    }

    private int GetUniqueSpawnIndex(int lastIndex, int numOfSpawnPoints)
    {
        int newIndex;
        do
        {
            newIndex = Random.Range(0, numOfSpawnPoints);
        } while (newIndex == lastIndex);
        return newIndex;
    }
}
