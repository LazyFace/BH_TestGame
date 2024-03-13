using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "EnemyData")]
public class EnemyConfig_SO : ScriptableObject
{
    [Header("Enemy Info")]
    public int health;
    public float speed;
    public int damage;
    public int points;
}
