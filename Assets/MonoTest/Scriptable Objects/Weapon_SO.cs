using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapon")]
public class Weapon_SO : ScriptableObject
{
    [Header("Info")]
    public string weaponName;

    [Header("Shooting")]
    public float fireRate;
    public int gunDamage;
    public int maxDistance;
    public AudioClip shootAudioClip;

    [Header("Reloading")]
    public int currentMagazineAmmo;
    public float reloadTime;
    public int magazineSize;
    public int maxAmmo;
    public int totalAmmo;
    public bool reloading;
    public AudioClip reloadAudioClip;
}
