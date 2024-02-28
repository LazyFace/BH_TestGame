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
    const string idleAnimation = "idle";
    const string walkingAnimation = "walk";
    const string attackRightAnimation = "attackRight";
    const string attackLeftAnimation = "attackLeft";
    const string dieAnimation = "die";

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
            Attack(other.gameObject);
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

    private void Attack(GameObject player)
    {
        if (!isDeath) 
        {
            isAttacking = true;
            player.gameObject.GetComponent<IDamageable>().GetDamaged(damage);
            ChangeAnimation(attackRightAnimation);
            Debug.Log("Atacó");
        }
    }

    private void EnemyAnimationsHandler()
    {
        float speedThreshold = 0.1f;

        if(!isAttacking) 
        {
            if (Mathf.Abs(navMeshAgent.velocity.x) > speedThreshold || Mathf.Abs(navMeshAgent.velocity.z) > speedThreshold)
            {
                ChangeAnimation(walkingAnimation);
            }
            else
            {
                ChangeAnimation(idleAnimation);
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
        ChangeAnimation(dieAnimation);
        while(isAnimationPlaying(animator, dieAnimation))
        {
            Debug.Log("Anim playing");
        }
        gameObject.SetActive(false);
    }

    private void ChangeAnimation(string newState)
    {
        if (newState == currentState)
        {
            return;
        }

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