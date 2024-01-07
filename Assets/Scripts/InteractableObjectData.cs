using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is for storing data for interactable objects (e.g. obj name, readable text, etc)
public class InteractableObjectData : MonoBehaviour
{
    [SerializeField] private string objectName; // the name of the in-universe object
    [SerializeField][TextArea] private string objectText; // toggleable text the player can read while interacting with an object that has words on it
    [SerializeField] private bool hasText; // A bool for checking if the object actually has text in it

    public string ObjectName
    {
        get { return objectName; }
    }

    public string ObjectText
    {
        get { return objectText; }
    }

    public bool HasText
    {
       get {  return hasText; }
    }
}
