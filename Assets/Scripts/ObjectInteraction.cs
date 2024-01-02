using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    [SerializeField] Transform pickedUpObject, heldAtPos;
    [SerializeField] RaycastHit hit;
    [SerializeField] Camera cam;
    [SerializeField] float xTurnRate, yTurnRate;
    [SerializeField] FirstPersonCam camScript;
    [SerializeField] MovementController moveScript;
    [SerializeField] float currentTime;

    float objectXRot, objectYRot, desiredRot;
    float cooldown = 0.5f;
    bool isObjectPickedUp, objectCanBeReleased;
    Vector3 playerInput;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 1f, -1))
            {
                if (hit.collider.CompareTag("PickupObject"))
                {
                    if (!isObjectPickedUp)
                    {
                        PickUpObject();
                        isObjectPickedUp = true;
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(0) && currentTime <= 0)
        {
            ReleaseObject();
        }

        if (Input.GetMouseButton(1) && isObjectPickedUp)
        {
            // Rotate the picked up object with the mouse right-click
            playerInput.x = (Input.GetAxisRaw("Mouse X") * xTurnRate) * Time.deltaTime;
            playerInput.y = (Input.GetAxisRaw("Mouse Y") * yTurnRate) * Time.deltaTime;
            playerInput.Normalize();

            objectYRot -= playerInput.x;
            objectXRot += playerInput.y;

            //desiredRot = mouseX + mouseY;

            pickedUpObject.rotation = Quaternion.Euler(objectXRot, objectYRot, 0);
        }

        RunTimer();
    }

    void PickUpObject()
    {
        // Picks up the object and locks the camera so the player can rotate the object
        pickedUpObject = hit.collider.transform;
        pickedUpObject.position = heldAtPos.position;
        camScript.enabled = false;
        moveScript.canMove = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        currentTime = cooldown;

        Debug.Log("picked up");
    }

    void ReleaseObject()
    {
        isObjectPickedUp = false;
        pickedUpObject = null;
        camScript.enabled = true;
        moveScript.canMove = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log("released");
    }

    void RunTimer()
    {
        if (currentTime > 0) 
        { 
            currentTime -= Time.deltaTime;
        } 
    }
}
