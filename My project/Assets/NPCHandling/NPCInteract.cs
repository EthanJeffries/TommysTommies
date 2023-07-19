using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteract : MonoBehaviour, Interactable
{
    public void Interact()
    {
        Debug.Log("Interact!");
    }

    public string GetInteractText()
    {
        return "Testing";
    }
}
