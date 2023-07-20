using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private UserInput input = null;
    private float interactRange = 2f;

    private void Awake()
    {
        input = new UserInput();
    }

    private void OnEnable()
    {
        input.Enable();

        input.Player.Interact.performed += x => Interact();
    }

    private void OnDisable()
    {
        input.Disable();

        input.Player.Interact.performed -= x => Interact();
    }

    private void Interact()
    {
        Debug.Log("Trying to interact");

        Collider2D[] colliderItems = Physics2D.OverlapCircleAll(transform.position, interactRange);
        foreach (Collider2D item in colliderItems)
        {
            if (item.TryGetComponent(out IInteractable itemInteract))
            {
                itemInteract.Interact();
            }
        }

    }
}
