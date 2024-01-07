using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolRandom : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float patrolInterval = 3f;
    public float wanderInterval = 2f;
    public float wanderRadius = 2f;

    private int currentPatrolIndex = 0;
    private NavMeshAgent navMeshAgent;
    private EnemyFOV enemyFOV;
    public float timeSinceLastPatrol = 0f;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyFOV = GetComponent<EnemyFOV>();

        // Set up NavMeshAgent settings
        navMeshAgent.autoBraking = false;
        navMeshAgent.stoppingDistance = 0.1f;  // Adjust stopping distance
        navMeshAgent.updateRotation = true;
        navMeshAgent.updatePosition = true;
    }

    private void Update()
    {

        if (!enemyFOV.canSeePlayer && !enemyFOV.hasSeenPlayer)
        {
            PatrolToNextPoint();
        }
        else if (enemyFOV.canSeePlayer || enemyFOV.hasSeenPlayer)
        {
            // If player is detected, reset timeSinceLastPatrol
            timeSinceLastPatrol = 0f;
        }

        // Check if it's time to wander
        if (!enemyFOV.canSeePlayer && !enemyFOV.hasSeenPlayer && timeSinceLastPatrol > patrolInterval)
        {
            Wander();
            timeSinceLastPatrol = 0f;
        }

        timeSinceLastPatrol += Time.deltaTime;
    }

    public void PatrolToNextPoint()
    {
        navMeshAgent.isStopped = false;

        if (patrolPoints.Length > 0 && navMeshAgent.remainingDistance < 0.5f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            navMeshAgent.SetDestination(patrolPoints[currentPatrolIndex].position);

            Debug.Log("Moving to point " + currentPatrolIndex);
        }
    }

    private void Wander()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, wanderRadius, NavMesh.AllAreas);

        navMeshAgent.SetDestination(navHit.position);
    }
}