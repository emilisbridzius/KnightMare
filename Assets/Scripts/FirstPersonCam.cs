using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCam : MonoBehaviour
{
    [SerializeField] float xSensitivity, ySensitivity, bobSpeed, bobAmount;
    [SerializeField] Transform orientation;

    float xRotation, yRotation;
    float timer = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // mouse input
        float mouseX = (Input.GetAxisRaw("Mouse X") * xSensitivity);
        float mouseY = (Input.GetAxisRaw("Mouse Y") * ySensitivity);

        yRotation += mouseX;
        xRotation -= mouseY;

        // prevent breaking your neck
        xRotation = Mathf.Clamp(xRotation, -90f, 90);

        // rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        Bobbing();
    }

    void Bobbing()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        if (Mathf.Abs(verticalInput) > 0.1f || Mathf.Abs(horizontalInput) > 0.1f)
        {
            // Calculate the bobbing motion
            float waveSlice = Mathf.Sin(timer);
            timer += bobSpeed;

            if (timer > Mathf.PI * 2)
            {
                timer = timer - (Mathf.PI * 2);
            }

            // Apply bobbing to the camera's Y position
            float bob = waveSlice * bobAmount;
            Vector3 cameraPosition = transform.localPosition;
            cameraPosition.y = bob;
            transform.localPosition = cameraPosition;
        }
        else
        {
            // If the player is not moving, reset the timer and camera position
            timer = 0f;
            Vector3 cameraPosition = transform.localPosition;
            cameraPosition.y = 0f;
            transform.localPosition = cameraPosition;
        }
    }
}
