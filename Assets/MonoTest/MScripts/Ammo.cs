using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IShootable shootable = other.gameObject.GetComponentInChildren<IShootable>();
            shootable.FillAmmo();
        }
    }
}
