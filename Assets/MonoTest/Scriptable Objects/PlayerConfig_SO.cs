using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "PlayerData")]
public class PlayerConfig_SO : ScriptableObject
{
    [Header("Player Info")]
    public int maxHealth;
    public int currentHealth;
    public float speed;

    [Header("Input Buttons")]
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    public KeyCode reloadGun;
    public KeyCode shootGun;
    public KeyCode ChangeGun;
}
