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
        
        Collider2D collider = Physics2D.OverlapCircle(transform.position, interactRange);
        if (collider.TryGetComponent(out Interactable interactable))
        {
            interactable.Interact();
        }
    }
}
