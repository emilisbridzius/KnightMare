using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectInteraction : MonoBehaviour
{
    [SerializeField] Transform pickedUpObject, heldAtPos, sleepText, blurEffect;
    [SerializeField] GameObject crosshair, player;
    [SerializeField] RaycastHit hit;
    [SerializeField] Camera cam;
    [SerializeField] float xTurnRate, yTurnRate, pickupRange, volumeSetting;
    [SerializeField] FirstPersonCam camScript;
    [SerializeField] MovementController moveScript;
    [SerializeField] AudioSource artifactSound;
    [SerializeField] AudioClip artifactClip;
    [SerializeField] private TMP_Text artifactCountUI;
    [SerializeField] private GameObject interactionPrompt;
    [SerializeField] private GameObject bedroomDemoEndFadeIn;

    float currentTime;
    float cooldown = 0.5f;
    bool objectPickedUp, lockRotation;
    Vector3 playerInput, previousObjPos;
    Quaternion previousObjRot;

    ObjectPickupUI ui;

    private void Start()
    {
        ui = FindObjectOfType<ObjectPickupUI>();
        objectPickedUp = false;
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
                        lockRotation = true;
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
            // if dropping objects doesn't work anymore then put the code in here outside of this if statement
            if (objectPickedUp)
            {
                if (pickedUpObject.GetComponent<InteractableObjectData>() != null)
                {
                    if (pickedUpObject.GetComponent<InteractableObjectData>().ObjectName == "Key") 
                    {
                        // temp maybe until someone decides that a cutscene is way cooler
                        StartCoroutine(EndOfDemoSequence());
                    }

                    if (pickedUpObject.GetComponent<InteractableObjectData>().IsArtifact)
                    {
                        PickupArtifact();
                    }
                    else
                    {
                        ResetHeldObjPosAndRot();
                        ReleaseObject();
                    }
                }
                
                
            }
            
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

        if (lockRotation)
        {
            LockRotation();
        }
    }

    // demo placeholder sequence remove in the final game ?????
    private IEnumerator EndOfDemoSequence()
    {
        bedroomDemoEndFadeIn.SetActive(true);

        yield return new WaitForSeconds(2.2f);

        SceneManager.LoadScene("EndOfDemo");
    }


    private void PickupArtifact()
    {
        FindObjectOfType<Player>().ArtifactCount++;
        Destroy(pickedUpObject.gameObject);
        FindObjectOfType<Player>().CheckArtifactCount();
        objectPickedUp = false;

        pickedUpObject = null;
        camScript.enabled = true;
        moveScript.canMove = true;

        LockMouse();
        crosshair.SetActive(true);
        blurEffect.gameObject.SetActive(false);

        Debug.Log("released");

        ui.HideUI();

        lockRotation = false;
        StartCoroutine(ArtifactUI());
    }


    private IEnumerator ArtifactUI()
    {
        artifactCountUI.gameObject.SetActive(true);
        artifactCountUI.text = FindObjectOfType<Player>().ArtifactCount + "/5";

        yield return new WaitForSeconds(1f);
        artifactCountUI.gameObject.SetActive(false);
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
        //blurEffect.gameObject.SetActive(true);

        currentTime = cooldown;

        Debug.Log("picked up");
        if (pickedUpObject.GetComponent<BoxCollider>() != null)
        {
            pickedUpObject.GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            pickedUpObject.GetComponent<MeshCollider>().enabled = false;
        }
        

        AudioSource.PlayClipAtPoint(artifactClip, artifactSound.transform.position, volumeSetting);


        // lewis code
        // looks for the data component on the selected object
        InteractableObjectData objData = pickedUpObject.GetComponent<InteractableObjectData>();
        
        // checks if null and then activates ui
        if (objData != null)
        {
            ui.ShowUI(objData);
        }

        LockRotation();
        
    }

    void ReleaseObject()
    {
        if (pickedUpObject.GetComponent<BoxCollider>() != null)
        {
            pickedUpObject.GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            pickedUpObject.GetComponent<MeshCollider>().enabled = true;
        }
        
        objectPickedUp = false;

        pickedUpObject = null;
        camScript.enabled = true;
        moveScript.canMove = true;

        LockMouse();
        crosshair.SetActive(true);
        blurEffect.gameObject.SetActive(false);

        Debug.Log("released");

        ui.HideUI();

        lockRotation = false;
    }

    void ResetHeldObjPosAndRot()
    {
        if (pickedUpObject != null)
        {
            pickedUpObject.position = previousObjPos;
            pickedUpObject.rotation = previousObjRot;
        }
        
    }

    void BedActivation()
    {
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(false);
        }

        sleepText.gameObject.SetActive(true);

        camScript.enabled = false;
        moveScript.canMove = false;

        ReleaseMouse();
    }

    void RunTimer()
    {
        if (currentTime > 0) 
        { 
            currentTime -= Time.deltaTime;
        } 
    }

    private void ReleaseMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void CloseSleepUI()
    {
        sleepText.gameObject.SetActive(false);
        LockMouse();
        camScript.enabled = true;
        moveScript.canMove = true;
    }

    public void LockRotation()
    {
        if (lockRotation)
        {
            player.transform.rotation = Quaternion.Euler(player.transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y, player.transform.rotation.eulerAngles.z);
        }
    }
}
