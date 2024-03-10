using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Rigidbody rb;

    private Animator animator;
    private string currentState;

    //Animations
    static readonly AnimationData idleAnimation = new AnimationData("idle", true);
    static readonly AnimationData walkingAnimation = new AnimationData("walk", true);
    static readonly AnimationData holdingRight = new AnimationData("holdingRight", false);
    static readonly AnimationData rightShoot = new AnimationData("holdinRightShoot", false);

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
            ChangeAnimation(walkingAnimation.animationName, idleAnimation.isLoop);
        }
        else
        {
            audioSource.Stop();
            ChangeAnimation(idleAnimation.animationName, idleAnimation.isLoop);
        }

        //Debug.Log("Velocidad X  " + rb.velocity.x);
        //Debug.Log("Velocidad Z  " + rb.velocity.z);
    }

    private void ChangeAnimation(string newState, bool isLoop)
    {
        if (isLoop && newState == currentState)
        {
            return;
        }
        animator.Rebind();
        currentState = newState;
        animator.Play(newState);
    }

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
