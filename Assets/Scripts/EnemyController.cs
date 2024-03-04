using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyConfig_SO enemyData;

    //Player reference
    private GameObject player;

    //Enemy reference
    private NavMeshAgent navMeshAgent;
    private int health;
    private float speed;
    private int damage;
    private bool isDeath = false;
    private bool isAttacking = false;

    //Animation references
    private Animator animator;
    private string currentState;

    private Action OnEnemyDeath;

    static readonly AnimationData idleAnimation = new AnimationData("idle", true);
    static readonly AnimationData walkingAnimation = new AnimationData("walk", true);
    static readonly AnimationData attackRightAnimation = new AnimationData("attackRight", false);
    static readonly AnimationData attackLeftAnimation = new AnimationData("attackLeft", false);
    static readonly AnimationData dieAnimation = new AnimationData("die", false);

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player");
        health = enemyData.health;
        speed = enemyData.speed;
        damage = enemyData.damage;
    }

    private void Start()
    {
        navMeshAgent.speed = speed;
    }

    private void Update()
    {
        if (!isDeath)
        {
            EnemyAnimationsHandler();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Attack(other.gameObject));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StopCoroutine(Attack(other.gameObject));
            isAttacking = false;
        }
    }

    private void FollowPlayer(GameObject player)
    {
        float distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceFromPlayer > 2f)
        {
            GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
        }
    }

    private IEnumerator Attack(GameObject player)
    {
        if (!isDeath) 
        {
            isAttacking = true;
            player.gameObject.GetComponent<IDamageable>().GetDamaged(damage);
            ChangeAnimation(attackRightAnimation.animationName, attackRightAnimation.isLoop);
            Debug.Log("Atacó");
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(Attack(player));
    }

    private void EnemyAnimationsHandler()
    {
        float speedThreshold = 0.1f;

        if(!isAttacking) 
        {
            if (Mathf.Abs(navMeshAgent.velocity.x) > speedThreshold || Mathf.Abs(navMeshAgent.velocity.z) > speedThreshold)
            {
                ChangeAnimation(walkingAnimation.animationName, walkingAnimation.isLoop);
            }
            else
            {
                ChangeAnimation(idleAnimation.animationName, idleAnimation.isLoop);
            }

            if (player != null)
            {
                FollowPlayer(player);
            }
        }
    }

    public void GetDamaged(int damageAmount)
    {
        health -= damageAmount;
        if(health < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDeath = true;
        ChangeAnimation(dieAnimation.animationName, dieAnimation.isLoop);
        while(isAnimationPlaying(animator, dieAnimation.animationName))
        {
            Debug.Log("Anim playing");
        }
        OnEnemyDeath?.Invoke();
        gameObject.SetActive(false);
    }

    private void ChangeAnimation(string newState, bool isLoop)
    {
        if(isLoop && newState == currentState)
        {
            return;
        }
        animator.Rebind();
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

    private void OnDisable()
    {
        OnEnemyDeath = null;
    }

    public void Initialize(Action callback)
    {
        OnEnemyDeath += callback;
    }
}