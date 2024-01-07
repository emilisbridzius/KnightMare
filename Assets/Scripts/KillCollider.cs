using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillCollider : MonoBehaviour
{
    MovementController move;
    FirstPersonCam fpc;
    PatrolRandom patrol;
    [SerializeField] Animator anim;

    [SerializeField] public bool testTrig;


    private void Start()
    {
        fpc = GameObject.Find("Camera").GetComponent<FirstPersonCam>();
        move = GameObject.Find("Player").GetComponent<MovementController>();
        patrol = GameObject.Find("Monster").GetComponent <PatrolRandom>();
        testTrig = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerDead();
            anim.SetTrigger("Attack");
        }
    }

    private void Update()
    {
        if (testTrig)
        {
            anim.SetTrigger("Attack");

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
