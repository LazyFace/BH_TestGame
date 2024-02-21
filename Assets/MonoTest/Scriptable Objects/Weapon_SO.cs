using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapon")]
public class Weapon_SO : ScriptableObject
{
    [Header("Info")]
    //public string name;

    [Header("Shooting")]
    public float fireRate;
    public int gunDamage;
    public int maxDistance;

    [Header("Reloading")]
    public int currentMagazineAmmo;
    public float reloadTime;
    public int magazineSize;
    public int maxAmmo;
    public int totalAmmo;
    public bool reloading;
    
}
