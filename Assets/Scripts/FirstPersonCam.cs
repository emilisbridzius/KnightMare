using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCam : MonoBehaviour
{
    [SerializeField] float xSensitivity;
    [SerializeField] float ySensitivity;

    [SerializeField] Transform orientation;
    
    private float xRotation;
    private float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
            // mouse input
            float mouseX = (Input.GetAxisRaw("Mouse X") * xSensitivity) * Time.deltaTime;
            float mouseY = (Input.GetAxisRaw("Mouse Y") * ySensitivity) * Time.deltaTime;

            yRotation += mouseX;
            xRotation -= mouseY;

            // prevent breaking your neck
            xRotation = Mathf.Clamp(xRotation, -90f, 90);

            // rotate cam and orientation
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        
    }
}
