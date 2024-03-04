using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "WaveData")]
public class Wave_SO : ScriptableObject
{
    [Header("Wave Info")]
    public int numZombies;
    public int numSkeletons;
    public int numGhosts;
}
