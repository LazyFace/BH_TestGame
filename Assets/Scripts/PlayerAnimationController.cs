using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Rigidbody rb;

    private Animator animator;
    private string currentState;

    //Audio
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float speedThreshold = 0.1f;


        if (Mathf.Abs(rb.velocity.x) > speedThreshold || Mathf.Abs(rb.velocity.z) > speedThreshold)
        {
            audioSource.Play();
            animator.SetBool("isWalking", true);
        }
        else
        {
            audioSource.Stop();
            animator.SetBool("isWalking", false);
        }

        //Debug.Log("Velocidad X  " + rb.velocity.x);
        //Debug.Log("Velocidad Z  " + rb.velocity.z);
    }
}
