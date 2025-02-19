using UnityEngine;
using UnityEngine.AI;

public class GuardAI : MonoBehaviour
{
    public Transform[] patrolPoints; // Puntos de patrulla
    public Transform player; // Referencia al jugador
    private NavMeshAgent agent;
    private int currentPoint = 0;
    private bool chasing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(patrolPoints[currentPoint].position);
    }

    void Update()
    {
        if (chasing)
        {
            agent.SetDestination(player.position);
            if (Vector3.Distance(transform.position, player.position) > 10f)
            {
                chasing = false;
                GoToNextPatrolPoint();
            }
        }
        else
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GoToNextPatrolPoint();
        }
    }

    void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;
        agent.SetDestination(patrolPoints[currentPoint].position);
        currentPoint = (currentPoint + 1) % patrolPoints.Length;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chasing = true;
        }
    }
}