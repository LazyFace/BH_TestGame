using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private string currentState;
    const string idleAnimation = "idle";
    const string walkingAnimation = "walk";

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
            ChangeAnimation(walkingAnimation);
        }
        else
        {
            ChangeAnimation(idleAnimation);
        }

        //Debug.Log("Velocidad X  " + rb.velocity.x);
        //Debug.Log("Velocidad Z  " + rb.velocity.z);
    }

    private void ChangeAnimation(string newState)
    {
        if (newState == currentState)
        {
            return;
        }

        animator.Play(newState);

        currentState = newState;
    }

    //Check if an animation is playing
    private bool isAnimationPlaying(Animator animator, string animName)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animName) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
