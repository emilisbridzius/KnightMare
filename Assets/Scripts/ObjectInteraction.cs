using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectInteraction : MonoBehaviour
{
    [SerializeField] Transform pickedUpObject, heldAtPos, sleepText, blurEffect;
    [SerializeField] GameObject crosshair;
    [SerializeField] RaycastHit hit;
    [SerializeField] Camera cam;
    [SerializeField] float xTurnRate, yTurnRate, pickupRange;
    [SerializeField] FirstPersonCam camScript;
    [SerializeField] MovementController moveScript;

    float currentTime;
    float cooldown = 0.5f;
    bool objectPickedUp;
    Vector3 playerInput, previousObjPos;
    Quaternion previousObjRot;

    ObjectPickupUI ui;

    private void Start()
    {
        ui = GameObject.Find("Canvas").GetComponent<ObjectPickupUI>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, pickupRange))
            {
                if (hit.collider.CompareTag("PickupObject"))
                {
                    if (!objectPickedUp)
                    {
                        PickUpObject();
                        objectPickedUp = true;
                    }
                }
                else if (hit.collider.CompareTag("Bed"))
                {
                    BedActivation();
                }
            }
        }

        if (Input.GetMouseButtonDown(0) && currentTime <= 0)
        {
            ResetHeldObjPosAndRot();
            ReleaseObject();
        }

        if (Input.GetMouseButton(1) && objectPickedUp)
        {
            // Rotate the picked up object with the mouse right-click
            playerInput.x = (Input.GetAxisRaw("Mouse X") * xTurnRate);
            playerInput.y = (Input.GetAxisRaw("Mouse Y") * yTurnRate);

            pickedUpObject.Rotate(Vector3.up, -playerInput.x, Space.World);
            pickedUpObject.Rotate(Vector3.right, playerInput.y, Space.World);
        }

        RunTimer();
    }

    void PickUpObject()
    {
        // Assigns the picked up object variable and its previous position and rotation
        pickedUpObject = hit.collider.transform;
        previousObjPos = pickedUpObject.position;
        previousObjRot = pickedUpObject.rotation;

        // Picks up the object and locks the camera so the player can rotate the object
        pickedUpObject.position = heldAtPos.position;
        camScript.enabled = false;
        moveScript.canMove = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        crosshair.SetActive(false);
        blurEffect.gameObject.SetActive(true);

        currentTime = cooldown;

        Debug.Log("picked up");

        // lewis code
        // looks for the data component on the selected object
        InteractableObjectData objData = pickedUpObject.GetComponent<InteractableObjectData>();
        
        // checks if null and then activates ui
        if (objData != null)
        {
            ui.ShowUI(objData);
        }
        
    }

    void ReleaseObject()
    {
        objectPickedUp = false;
        pickedUpObject = null;
        camScript.enabled = true;
        moveScript.canMove = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        crosshair.SetActive(true);
        blurEffect.gameObject.SetActive(false);

        Debug.Log("released");

        ui.HideUI();
    }

    void ResetHeldObjPosAndRot()
    {
        pickedUpObject.position = previousObjPos;
        pickedUpObject.rotation = previousObjRot;
    }

    void BedActivation()
    {
        sleepText.gameObject.SetActive(true);

        camScript.enabled = false;
        moveScript.canMove = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void RunTimer()
    {
        if (currentTime > 0) 
        { 
            currentTime -= Time.deltaTime;
        } 
    }
}
