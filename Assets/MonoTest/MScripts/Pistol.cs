using System.Collections;
using UnityEngine;

public class Pistol : MonoBehaviour, IShootable
{
    [SerializeField] private Weapon_SO pistol;
    [SerializeField] private Transform grabPosition;
    [SerializeField] private Transform firePosition;
    [SerializeField] private Bullet bullet;
    [SerializeField] private AudioSource shotSound;

    private float nextFire = 0.0f;

    private void Start()
    {
        pistol.currentMagazineAmmo = pistol.magazineSize;
        pistol.totalAmmo = pistol.maxAmmo;
    }

    private void Update()
    {
        Debug.DrawRay(firePosition.position, firePosition.forward, Color.red);
    }

    public void Fire()
    {
        //Debug.Log("Si entre");
        if (pistol.currentMagazineAmmo > 0 && !pistol.reloading)
        {
            if(Time.time > nextFire)
            {
                //instantiate bullet
                GameObject bulletInstance = ObjectPooler.Instance.SpawnFromPool(ObjectPooler.ObjectsToSpawn.BULLET, firePosition.position, firePosition.rotation);
                bulletInstance.GetComponent<Bullet>().SetBulletDamage(pistol.gunDamage);
                if(shotSound != null)
                    shotSound.Play();

                pistol.totalAmmo--;
                pistol.currentMagazineAmmo--;
                nextFire = Time.time + pistol.fireRate;
            }
        }
    }

    public void Reload()
    {
        pistol.reloading = true;
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
        pistol.reloading = false;
    }

    public Transform TakeGrabPosition()
    {
        return grabPosition;
    }
}
