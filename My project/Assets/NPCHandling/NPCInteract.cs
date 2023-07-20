using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteract : MonoBehaviour, IInteractable
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
