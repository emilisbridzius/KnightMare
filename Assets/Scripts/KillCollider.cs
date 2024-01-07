using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillCollider : MonoBehaviour
{
    MovementController move;
    FirstPersonCam fpc;
    PatrolRandom patrol;


    private void Start()
    {
        fpc = GameObject.Find("Camera").GetComponent<FirstPersonCam>();
        move = GameObject.Find("Player").GetComponent<MovementController>();
        patrol = GameObject.Find("Monster").GetComponent <PatrolRandom>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerDead();
        }
    }

    void PlayerDead()
    {
        fpc.enabled = false;
        move.canMove = false;
        patrol.canPatrol = false;

        SceneManager.LoadScene("BedroomScene");
    }
}
