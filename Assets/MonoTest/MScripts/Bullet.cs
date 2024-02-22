using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    private int weaponDamage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        BulletInitialVelocity();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IDamageable damage = collision.gameObject.GetComponent<IDamageable>();
            damage.GetDamaged(weaponDamage);
        }
    }

    public void SetBulletDamage(int gunDamage)
    {
        weaponDamage = gunDamage;
    }

    private void BulletInitialVelocity()
    {
        rb.AddForce(transform.forward * 15f, ForceMode.Force);
    }
}
