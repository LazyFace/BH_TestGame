using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    public bool areEnemiesInView = false;
    private List<Transform> enemiesInFieldOfView = new List<Transform>();

    private void Update()
    {
        if (enemiesInFieldOfView.Count > 0)
        {
            areEnemiesInView = true;
            DetectDeathEnemies();
        }
        else
        {
            areEnemiesInView = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInFieldOfView.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInFieldOfView.Remove(other.transform);
        }
    }

    private void DetectDeathEnemies()
    {
        for (int i = 0; i <  enemiesInFieldOfView.Count; ++i)
        {
            if (enemiesInFieldOfView[i].transform.GetComponent<EnemyController>().isDeath) 
            {
                enemiesInFieldOfView.Remove(enemiesInFieldOfView[i]);
            }
        }
    }

    public Transform GetClosestEnemy()
    {
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform enemy in enemiesInFieldOfView)
        {
            float distance = Vector3.Distance(playerTransform.position, enemy.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }
}
