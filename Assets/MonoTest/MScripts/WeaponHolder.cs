using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    private enum guns {Pistol, SMG}
    
    private Dictionary<guns, GameObject> gunsDictionary = new Dictionary<guns, GameObject>();

    [SerializeField] private WeaponHolder_SO weapon;

    private void Start()
    {
        for(int i= 0; i < weapon.weapons.Length-1; i++)
        {
            gunsDictionary.Add((guns)i, weapon.weapons[i]);
        }
    }


}
