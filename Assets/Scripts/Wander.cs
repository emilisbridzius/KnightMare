using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : MonoBehaviour
{
    [SerializeField] float wanderRadius;
    [SerializeField] float wanderTimer;

    private NavMeshAgent agent;
    private float timer;

    private void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }
    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        // Get a random point within a sphere who's size is multiplied by the distance.
        Vector3 randomDirection = Random.insideUnitSphere * distance;

        // Add the random points' location to the origin so the distance to the point
        // remains relative to the origin.
        randomDirection += origin;
        NavMeshHit navHit;

        // Instruct the agent to move towards the random point within the bounds of 
        // its movement radius (distance) and the NavMesh areas that are allowed when finding the point.
        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);
        return navHit.position;
    }
}
