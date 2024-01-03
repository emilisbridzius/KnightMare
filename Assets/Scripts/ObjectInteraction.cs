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
    bool objectPickedUp;
    Vector3 playerInput;
    public Vector3 previousObjPos;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 1f, -1))
            {
                if (hit.collider.CompareTag("PickupObject"))
                {
                    if (!objectPickedUp)
                    {
                        PickUpObject();
                        objectPickedUp = true;
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(0) && currentTime <= 0)
        {
            ResetHeldObjPos();
            ReleaseObject();
        }

        if (Input.GetMouseButton(1) && objectPickedUp)
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
        // Assigns the picked up object variable and its previous position
        pickedUpObject = hit.collider.transform;
        previousObjPos = pickedUpObject.position;

        // Picks up the object and locks the camera so the player can rotate the object
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
        objectPickedUp = false;
        pickedUpObject = null;
        camScript.enabled = true;
        moveScript.canMove = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log("released");
    }

    void ResetHeldObjPos()
    {
        pickedUpObject.position = previousObjPos;
    }

    void RunTimer()
    {
        if (currentTime > 0) 
        { 
            currentTime -= Time.deltaTime;
        } 
    }
}
