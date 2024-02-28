using System.Collections;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private int currentRound = 1;
    [SerializeField] private float timeBetweenRounds = 10f; // Tiempo de espera entre rondas
    private int enemiesThisRound;
    private int enemiesRemaining;

    void Start()
    {
        StartRound();
    }

    void Update()
    {
        // Verifica si todos los enemigos han sido derrotados para terminar la ronda
        if (enemiesRemaining <= 0)
        {
            StartCoroutine(WaitAndStartNextRound(timeBetweenRounds));
        }
    }

    void StartRound()
    {
        enemiesThisRound = CalculateEnemiesForRound(currentRound);
        enemiesRemaining = enemiesThisRound;
        Debug.Log($"Ronda {currentRound} comenzó con {enemiesThisRound} enemigos.");

        // Aquí puedes añadir la lógica para generar los enemigos de esta ronda
        GenerateEnemies(enemiesThisRound);
    }

    IEnumerator WaitAndStartNextRound(float waitTime)
    {
        Debug.Log("Ronda terminada. Esperando para la siguiente ronda...");
        yield return new WaitForSeconds(waitTime);

        currentRound++;
        StartRound();
    }

    public void EnemyDefeated()
    {
        enemiesRemaining--;
        Debug.Log("Enemigo derrotado. Enemigos restantes: " + enemiesRemaining);
    }

    int CalculateEnemiesForRound(int round)
    {
        // Aquí puedes definir cómo aumenta el número de enemigos por ronda
        // Por ejemplo, empezando con 5 enemigos y aumentando en 5 cada ronda
        return 5 + (5 * (round - 1));
    }

    void GenerateEnemies(int numberOfEnemies)
    {
        // Implementa la lógica de generación de enemigos aquí
        Debug.Log($"Generando {numberOfEnemies} enemigos.");
    }
}
