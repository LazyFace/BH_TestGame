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
    public bool isDeath = false;
    private bool isAttacking = false;
    private bool isAttackingRight = true;
    [SerializeField] private Collider enemyCollider;
    [SerializeField] private float rotationSpeed = 10f;

    //Sound
    [SerializeField] private AudioSource enemyAudioSource;

    //Animation references
    private Animator animator;
    private string currentState;

    private Action OnEnemyDeath;

    //Animations
    static readonly AnimationData idleAnimation = new AnimationData("idle", true);
    static readonly AnimationData walkingAnimation = new AnimationData("walk", true);
    static readonly AnimationData attackRightAnimation = new AnimationData("attackRight", false);
    static readonly AnimationData attackLeftAnimation = new AnimationData("attackLeft", false);
    static readonly AnimationData dieAnimation = new AnimationData("die", false);

    private Coroutine attackCoroutine;

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

    private void OnEnable()
    {
        enemyCollider.enabled = true;
        isDeath = false;
        navMeshAgent.isStopped = false;
    }

    private void Update()
    {
        if (!isDeath)
        {
            EnemyAnimationsHandler();
            if (player != null)
            {
                FollowPlayer(player);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDeath && other.gameObject.CompareTag("Player"))
        {
            attackCoroutine = StartCoroutine(Attack(other.gameObject));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isDeath && other.gameObject.CompareTag("Player"))
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
            isAttacking = false;
        }
    }

    private void FollowPlayer(GameObject player)
    {
        float distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (!isDeath && distanceFromPlayer > 2f)
        {
            navMeshAgent.SetDestination(player.transform.position);
        }
        else
        {
            LookPlayer(player);
        }
    }

    private void LookPlayer(GameObject player)
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0;

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    private IEnumerator Attack(GameObject player)
    {
        if (!isDeath) 
        {
            isAttacking = true;
            player.gameObject.GetComponent<IDamageable>().GetDamaged(damage);

            if (isAttackingRight)
            {
                ChangeAnimation(attackRightAnimation.animationName, attackRightAnimation.isLoop);
            }
            else
            {
                ChangeAnimation(attackLeftAnimation.animationName, attackLeftAnimation.isLoop);
            }

            isAttackingRight = !isAttackingRight;
        }
        yield return new WaitForSeconds(1f);
        attackCoroutine = StartCoroutine(Attack(player));
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
        }
    }

    public void GetDamaged(int damageAmount)
    {
        health -= damageAmount;
        enemyAudioSource.PlayOneShot(enemyData.gotHitSound);
        if (!isDeath && health < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        StartCoroutine(DieCoroutine());
    }

    private IEnumerator DieCoroutine()
    {
        isDeath = true;
        enemyCollider.enabled = false;
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0f;
        ChangeAnimation(dieAnimation.animationName, dieAnimation.isLoop);
        GameManager.Instance.AddPoints(enemyData.points);
        yield return new WaitForSeconds(1.2f); //new WaitUntil(() => !isAnimationPlaying(animator, dieAnimation.animationName));
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
        currentState = newState;
        animator.Play(newState);
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