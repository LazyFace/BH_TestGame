using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Pistol : MonoBehaviour, IShootable
{
    [SerializeField] private Weapon_SO pistol;
    [SerializeField] private Transform grabPosition;
    [SerializeField] private Transform firePosition;

    private float nextFire = 0.0f;

    private void Update()
    {
        Debug.DrawRay(firePosition.position, firePosition.forward, Color.red);
    }

    public void Fire()
    {
        if (pistol.currentMagazineAmmo > 0 && !pistol.reloading)
        {
            if(Time.time > nextFire)
            {
                RaycastHit hit;
                if (Physics.Raycast(firePosition.position, firePosition.forward, out hit, pistol.maxDistance))
                {
                    Debug.Log(hit.transform.name);
                }

                //instantiate bullet

                pistol.totalAmmo--;
                pistol.currentMagazineAmmo--;
                nextFire = Time.time + pistol.fireRate;
                Debug.Log("Piuu");
            }

        }
        else
        {
            Debug.Log("Not Ammo");
        }
    }

    public void Reload()
    {
        StartCoroutine(ReloadingGunCoroutine());
    }

    private IEnumerator ReloadingGunCoroutine()
    {
        yield return new WaitForSeconds(pistol.reloadTime);
        if (pistol.totalAmmo > pistol.magazineSize)
        {
            pistol.currentMagazineAmmo = pistol.magazineSize;
        }
        else if (pistol.totalAmmo <= pistol.magazineSize)
        {
            pistol.currentMagazineAmmo = pistol.totalAmmo;
        }
    }

    public Transform TakeGrabPosition()
    {
        return grabPosition;
    }
}
