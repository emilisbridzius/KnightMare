using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform camOrientation;
    [SerializeField] AudioClip[] walkSounds;
    [SerializeField] AudioSource playerSound;

    Vector3 forward, right;
    public float soundTimer, soundInterval;

    public bool canMove;
    public bool isMoving;
    public float volumeSetting;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        canMove = true;
    }

    private void Update()
    {
        if (canMove)
        {
            // Get input for movement
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Calculate movement direction relative to camera
            Vector3 forward = Camera.main.transform.forward;
            Vector3 right = Camera.main.transform.right;

            // Ensure vertical movement is only along the horizontal plane
            forward.y = 0f;
            right.y = 0f;

            // Normalize vectors to ensure consistent speed in all directions
            forward.Normalize();
            right.Normalize();

            // Calculate movement direction based on input and camera orientation
            Vector3 moveDirection = forward * verticalInput + right * horizontalInput;

            // Apply movement to the Rigidbody using MovePosition
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);
        }

        // Check for player movement (you can replace this with your own movement detection logic)
        float horizontalInput2 = Input.GetAxis("Horizontal");
        float verticalInput2 = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontalInput2) > 0.1f || Mathf.Abs(verticalInput2) > 0.1f)
        {
            // Player is moving
            if (isMoving)
            {
                // Start playing the random walk sound
                PlayRandomWalkSound();
                isMoving = true;
            }

            // Update the timer
            soundTimer += Time.deltaTime;
            if (soundTimer >= soundInterval)
            {
                // Play the random walk sound every half-second
                PlayRandomWalkSound();
                soundTimer = 0.0f;
            }
        }
        else
        {
            // Player is not moving
            if (!isMoving)
            {
                // Stop playing the walk sound
                isMoving = false;
            }

            // Reset the timer when the player is not moving
            soundTimer = 0.0f;
        }
    }

    void PlayRandomWalkSound()
    {
        // Choose a random walk sound from the array
        AudioClip randomWalkSound = walkSounds[Random.Range(0, walkSounds.Length)];

        // Start playing the random walk sound
        AudioSource.PlayClipAtPoint(randomWalkSound, rb.position, volumeSetting);
    }
}
