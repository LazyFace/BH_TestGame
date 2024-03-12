using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Rigidbody rb;

    private Animator animator;

    private float speedAnimation;

    //Audio
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float SPEEDTHRESHOLD = 0.1f;


        if (Mathf.Abs(rb.velocity.x) > SPEEDTHRESHOLD || Mathf.Abs(rb.velocity.z) > SPEEDTHRESHOLD)
        {
            audioSource.Play();
            animator.SetBool("isWalking", true);
            animator.SetFloat("speed", Mathf.Lerp(speedAnimation, 1f, Time.deltaTime * 10f));
        }
        else
        {
            audioSource.Stop();
            animator.SetFloat("speed", Mathf.Lerp(speedAnimation, 0f, Time.deltaTime * 10f));
            animator.SetBool("isWalking", false);
        }

        //Debug.Log("Velocidad X  " + rb.velocity.x);
        //Debug.Log("Velocidad Z  " + rb.velocity.z);
    }

    public void PlayerShootAnimation()
    {
        animator.SetTrigger("shoot");
    }
}
