using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponHolder : MonoBehaviour
{
    private int gunsIndex = 0;
    private float switchGunTime = 0.5f;

    
    private List<GameObject> gunList = new List<GameObject>();

    [SerializeField] private List<Weapon_SO> weaponsData = new List<Weapon_SO>();
    [SerializeField] private WeaponHolder_SO weapon;

    [SerializeField] private UnityEvent<string, int, int> onWeaponUsed;

    private void Start()
    {
        InstantiateWeapons();
        weapon.isSwitchingWeapon = false;
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
        StartCoroutine(InitializeWeaponDataHUD());
    }

    private IEnumerator InitializeWeaponDataHUD()
    {
        yield return new WaitForSeconds(0.5f);
        onWeaponUsed?.Invoke(weaponsData[gunsIndex].weaponName, weaponsData[gunsIndex].currentMagazineAmmo, weaponsData[gunsIndex].totalAmmo);
    }

    public void ChangeWeapon()
    {
        weapon.isSwitchingWeapon = true;
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
        weapon.isSwitchingWeapon = false;

        onWeaponUsed?.Invoke(weaponsData[gunsIndex].weaponName, weaponsData[gunsIndex].currentMagazineAmmo, weaponsData[gunsIndex].totalAmmo);
    }

    public void Shoot()
    {
        if(!weapon.isSwitchingWeapon) 
        {
            IShootable shootable = gunList[gunsIndex].GetComponentInChildren<IShootable>();           
            shootable?.Fire();

            onWeaponUsed?.Invoke(weaponsData[gunsIndex].weaponName, weaponsData[gunsIndex].currentMagazineAmmo, weaponsData[gunsIndex].totalAmmo);
        }
    }

    public void Reload()
    {
       if(!weapon.isSwitchingWeapon && !weaponsData[gunsIndex].reloading && weaponsData[gunsIndex].currentMagazineAmmo < weaponsData[gunsIndex].magazineSize)
        {
            IShootable shootable = gunList[gunsIndex].GetComponentInChildren<IShootable>();
            shootable?.Reload();

            onWeaponUsed?.Invoke(weaponsData[gunsIndex].weaponName, weaponsData[gunsIndex].currentMagazineAmmo, weaponsData[gunsIndex].totalAmmo);
            
        }
    }

    public void FillAllAmmo()
    {
        foreach(GameObject gun in gunList)
        {
            IShootable shootable = gun.GetComponentInChildren<IShootable>();
            shootable.FillAmmo();

            onWeaponUsed?.Invoke(weaponsData[gunsIndex].weaponName, weaponsData[gunsIndex].currentMagazineAmmo, weaponsData[gunsIndex].totalAmmo);
        }
    }

}
