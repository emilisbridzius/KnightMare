using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform camOrientation;

    Vector3 forward, right;

    public bool canMove;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        canMove = true;
    }

    private void Update()
    {
        if (canMove)
        {
            forward = camOrientation.forward;
            right = camOrientation.right;

            forward.y = 0f;
            right.y = 0f;

            Vector3 movementDirection = forward * Input.GetAxis("Vertical") + right * Input.GetAxis("Horizontal");


            transform.Translate(movementDirection * moveSpeed * Time.deltaTime, Space.World);

        }
    }
}
