using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WeaponHolder weaponHolder = other.gameObject.GetComponentInChildren<WeaponHolder>();
            weaponHolder.FillAllAmmo();
            this.gameObject.SetActive(false);   
        }
    }
}
