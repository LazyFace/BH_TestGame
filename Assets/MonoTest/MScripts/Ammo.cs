using System.Collections;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private Animator ammoCrateAnimator;
    [SerializeField] AudioSource ammoBoxAudioSource;

    private void OnEnable()
    {
        ammoCrateAnimator.SetBool("isDestoyed", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WeaponHolder weaponHolder = other.gameObject.GetComponentInChildren<WeaponHolder>();
            weaponHolder.FillAllAmmo();
            ammoCrateAnimator.SetBool("isDestoyed", true);
            ammoBoxAudioSource.Play();
            StartCoroutine(WaitForSoundToDissapear());
        }
    }

    private IEnumerator WaitForSoundToDissapear()
    {
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);
    }
}
