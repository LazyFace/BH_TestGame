using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapon")]
public class Weapon_SO : ScriptableObject
{
    [Header("Info")]
    public string name;

    [Header("Shooting")]
    public int gunDamage;
    public int MaxDistance;

    [Header("Reloading")]
    public float reloadTime;
    public int magazineAmmo;
    public int totalAmmo;
    
}
