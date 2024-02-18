using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "PlayerData")]
public class PlayerConfig_SO : ScriptableObject
{
    [Header("Player Info")]
    public int health;
    public float speed;

    [Header("Input Buttons")]
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    public KeyCode reloadGun;
    public KeyCode shootGun;
}
