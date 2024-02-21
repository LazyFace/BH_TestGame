using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    private int gunsIndex = 0;
    private float switchGunTime = 0.5f;

    private List<GameObject> gunList = new List<GameObject>();

    [SerializeField] private WeaponHolder_SO weapon;

    private void Start()
    {
        InstantiateWeapons();
    }

    private void InstantiateWeapons()
    {
        for (int i = 0; i < weapon.weapons.Length; i++)
        {
            GameObject weaponInstantiated = Instantiate(weapon.weapons[i], transform);
            gunList.Add(weaponInstantiated);
            weaponInstantiated.transform.position = transform.position;
            if ( i == 0)
            {
                weaponInstantiated.SetActive(true);
            }
            else
            {
                weaponInstantiated.SetActive(false);    
            }
        }
    }

    public void ChangeWeapon()
    {
        StartCoroutine(ChangeWeaponCoroutine());
    }

    private IEnumerator ChangeWeaponCoroutine()
    {
        gunList[gunsIndex].SetActive(false);
        //Debug.Log(gunsIndex);
        gunsIndex++;
        if (gunsIndex >= weapon.weapons.Length)
        {
            gunsIndex = 0;
        }
        yield return new WaitForSeconds(switchGunTime);
        //Debug.Log(gunsIndex);
        gunList[gunsIndex].SetActive(true);

    }

    private IShootable TakeActualWeaponFunctions()
    {
        IShootable shootable = gunList[gunsIndex].GetComponentInChildren<IShootable>();
        return shootable;
    }

    public void Shoot()
    {
        IShootable shootable = TakeActualWeaponFunctions();
        shootable?.Fire();
    }

    public void Reload()
    {
        IShootable shootable = TakeActualWeaponFunctions();
        shootable?.Reload();
    }

}
