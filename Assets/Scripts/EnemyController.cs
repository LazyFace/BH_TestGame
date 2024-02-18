using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        FollowPlayer(player);
    }

    private void FollowPlayer(GameObject player)
    {
        float distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceFromPlayer > 2f) 
        {
            GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
        }
    }
}
