using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum MenuState
{
    ACTIVE,
    INACTIVE
}

// This script manages the UI related parts of picking up an object and inspecting it.
public class ObjectPickupUI : MonoBehaviour
{
    [SerializeField] private TMP_Text itemText;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private GameObject interactionUI;
    [SerializeField] private GameObject textTogglePrompt;

    private InteractableObjectData currentObjData;

    private MenuState uiState;

    private bool textIsActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // toggle for text
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (uiState == MenuState.ACTIVE) 
            {
                if (currentObjData.HasText)
                {
                    if (textIsActive)
                    {
                        itemText.gameObject.SetActive(false);
                    }
                    else
                    {

                        itemText.gameObject.SetActive(true);
                    }

                }
            }

        }
    }

    public void ShowUI(InteractableObjectData current)
    {
        currentObjData = current;
        uiState = MenuState.ACTIVE;
        interactionUI.SetActive(true);
        itemName.text = currentObjData.ObjectName;
        
        if (currentObjData.HasText)
        {
            textTogglePrompt.SetActive(true);
            itemText.text = currentObjData.ObjectText;
        }
    }

    public void HideUI()
    {
        uiState = MenuState.INACTIVE;
        interactionUI.SetActive(false);
        itemText.gameObject.SetActive(false);
    }
}
