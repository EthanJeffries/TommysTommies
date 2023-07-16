using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


//Class used to control player movement and aiming
//Created by Ethan Jeffries and last edited 7/10/2023
public class PlayerMovement : MonoBehaviour
{
    //Basic movement variables
    private UserInput input = null;
    private Vector2 moveVector = Vector2.zero;
    private Rigidbody2D playerRB = null;
    private Camera mainCamera;
    [SerializeField] private bool isController;

    //Move and sprint speed variables
    [SerializeField] private bool isSprinting;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float regularSpeed = 5f;
    [SerializeField] private float dualSpeed = 3f;

    //Dash variables
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.25f;
    [SerializeField] private bool isDashing;

    //Aiming variables
    private Vector2 aimDirection;
    [SerializeField] private float controllerDeadzone = 0.1f;

    private void Awake()
    {
        //Initializes important objects such as input, rigidbody, and camera
        input = new UserInput();
        playerRB = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        //Enable input
        input.Enable();
        
        //Methods for general movement
        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCanceled;

        //Method for Dashing
        input.Player.Dash.performed += x => StartDash();
    }

    private void OnDisable()
    {
        //Disable input
        input.Disable();

        //Methods for general movement
        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Movement.canceled -= OnMovementCanceled;

        //Method for Dashing
        input.Player.Dash.performed -= x => StartDash();
    }

    //Check input device type
    public void CheckDeviceInput(PlayerInput playerInput)
    {
        if (playerInput.currentControlScheme.Equals("Controller"))
        {
            isController = true;
        }
        else
        {
            isController = false;
        }
    }

    //Movement methods
    //Calculates the move vector to move the player
    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();
    }

    //Sets the move vector to 0 when there is no input
    private void OnMovementCanceled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;
    }

    //Dash methods
    private void StartDash()
    {
        isDashing = true;
    }

    private IEnumerator Dash()
    {
        playerRB.velocity = moveVector * dashSpeed;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
    }

    //Aiming Methods
    private void AimPlayer()
    {
        //Read in aiming direction to a Vector2 value
        aimDirection = input.Player.Aim.ReadValue<Vector2>();
        if (isController) //Controller Aiming
        {
            if (Mathf.Abs(aimDirection.x) > controllerDeadzone || Mathf.Abs(aimDirection.y) > controllerDeadzone)
            {
                float angleToTarget = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angleToTarget));
                
            }
        }
        else //MK Aiming
        {
            Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(aimDirection);
            Vector3 targetDirection = mouseWorldPosition - transform.position;
            float angleToTarget = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angleToTarget));
        }
    }


    private void FixedUpdate()
    {
        AimPlayer();
        
        //Check for dual wield
        if (GetComponent<PlayerCombat>().dualWield && GetComponent<PlayerCombat>().weaponReady)
        {
            moveSpeed = dualSpeed;
        }
        else
        {
            moveSpeed = regularSpeed;
        }

        playerRB.velocity = moveVector * moveSpeed;
        
        if (isDashing) //Check for dash input
        {
            StartCoroutine(Dash());
        }
    }

}
