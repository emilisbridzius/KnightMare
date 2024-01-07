using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyFOV : MonoBehaviour
{
    [Header("FOV Settings")]
    public float radius;
    [Range(0, 360)]
    public float angle;

    [Header("GameObj + Transform Ref")]
    public GameObject playerRef;
    public Transform playerPos;
    public Transform enemyAgentTransform;

    [Header("NavMesh + LayerMask Settings")]
    public NavMeshAgent enemyAgent;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public float loseInterestAtSec = 4f;

    [Header("Debugging Tools for FOV")]
    [SerializeField] public bool canSeePlayer;
    public bool hasSeenPlayer;
    public float lostInterestForSec;

    PatrolRandom patrol;

    private void Start()
    {
        playerRef = GameObject.Find("Player");
        playerPos = playerRef.gameObject.transform;

        enemyAgentTransform = transform;

        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    public void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }

        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
    }


    public void Update()
    {
        if (canSeePlayer)
        {
            hasSeenPlayer = true;
            lostInterestForSec = loseInterestAtSec;
            Vector3 playerPosY = playerPos.position;
            playerPosY.y = enemyAgentTransform.position.y;
            enemyAgentTransform.LookAt(playerPosY);
        }
        else
        {
            lostInterestForSec -= Time.deltaTime;
            if (lostInterestForSec <= 0f)
            {
                hasSeenPlayer = false;
            }

        }

        if (hasSeenPlayer)
        {
            if (enemyAgent.isActiveAndEnabled) enemyAgent.isStopped = false;
            enemyAgent.SetDestination(playerPos.position);
        }
        //else if (!canSeePlayer && !hasSeenPlayer)
        //{
        //    patrol.PatrolToRandomPoint();
        //}
    }
}
