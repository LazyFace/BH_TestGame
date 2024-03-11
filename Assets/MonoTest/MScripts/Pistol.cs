using System.Collections;
using UnityEngine;

public class Pistol : MonoBehaviour, IShootable
{
    [SerializeField] private Weapon_SO weaponData;
    [SerializeField] private Transform grabPosition;
    [SerializeField] private Transform firePosition;
    [SerializeField] private Bullet bullet;
    [SerializeField] private AudioSource shotSound;

    private float nextFire = 0.0f;

    private void Start()
    {
        weaponData.currentMagazineAmmo = weaponData.magazineSize;
        weaponData.totalAmmo = weaponData.maxAmmo;
    }

    private void Update()
    {
        Debug.DrawRay(firePosition.position, firePosition.forward, Color.red);
    }

    public void Fire()
    {
        //Debug.Log("Si entre");
        if (weaponData.currentMagazineAmmo > 0 && !weaponData.reloading)
        {
            if (Time.time > nextFire)
            {
                //instantiate bullet
                GameObject bulletInstance = ObjectPooler.Instance.SpawnFromPool(ObjectPooler.ObjectsToSpawn.BULLET, firePosition.position, firePosition.rotation);
                bulletInstance.GetComponent<Bullet>().SetBulletDamage(weaponData.gunDamage);
                if(shotSound != null)
                    shotSound.Play();

                weaponData.totalAmmo--;
                weaponData.currentMagazineAmmo--;
                nextFire = Time.time + weaponData.fireRate;
            }
        }
    }

    public void Reload()
    {
        weaponData.reloading = true;
        StartCoroutine(ReloadingGunCoroutine());
    }

    private IEnumerator ReloadingGunCoroutine()
    {
        if (weaponData.totalAmmo > weaponData.magazineSize)
        {
            weaponData.currentMagazineAmmo = weaponData.magazineSize;
        }
        else if (weaponData.totalAmmo <= weaponData.magazineSize)
        {
            weaponData.currentMagazineAmmo = weaponData.totalAmmo;
        }
        yield return new WaitForSeconds(weaponData.reloadTime);
        weaponData.reloading = false;
    }

    public Transform TakeGrabPosition()
    {
        return grabPosition;
    }

    public void FillAmmo()
    {
        weaponData.totalAmmo = weaponData.maxAmmo;
    }
}
