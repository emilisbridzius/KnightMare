using UnityEngine;
using UnityEngine.AI;

public class PatrolRandom : MonoBehaviour
{
    public Transform[] patrolPoints;
    private NavMeshAgent navMeshAgent;
    private int currentPatrolIndex;
    EnemyFOV fov;
    public bool canPatrol;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        fov = GetComponent<EnemyFOV>();
        canPatrol = true;

        if (patrolPoints.Length == 0)
        {
            Debug.LogError("No patrol points assigned. Please assign patrol points in the inspector.");
        }
        else
        {
            // Set the initial destination to the first patrol point
            currentPatrolIndex = 0;
            navMeshAgent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    void Update()
    {
        if (!fov.canSeePlayer && !fov.hasSeenPlayer && canPatrol)
        {
            // Check if the agent has reached the current patrol point
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
            {
                // Move to the next random point
                PatrolToRandomPoint();
            }

        }
    }

    public void PatrolToRandomPoint()
    {
        // Select a random index from the patrol points list
        int randomIndex = Random.Range(0, patrolPoints.Length);

        // Set the destination to the randomly selected point
        navMeshAgent.SetDestination(patrolPoints[randomIndex].position);

        // Update the current patrol index
        currentPatrolIndex = randomIndex;

        Debug.Log("Moving to random point: " + currentPatrolIndex);
    }
}