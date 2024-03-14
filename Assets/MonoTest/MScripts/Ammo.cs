using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] AudioSource ammoBoxAudioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WeaponHolder weaponHolder = other.gameObject.GetComponentInChildren<WeaponHolder>();
            weaponHolder.FillAllAmmo();
            StartCoroutine(WaitForSoundToDissapear());
        }
    }

    private IEnumerator WaitForSoundToDissapear()
    {
        ammoBoxAudioSource.Play();
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
    }
}
