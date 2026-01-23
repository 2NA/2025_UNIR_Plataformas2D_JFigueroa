using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MovementController
{
    [Header("Player Movement Settings")]
    [SerializeField] Transform feet;
    [SerializeField] float groundDistance = 0.15f;
    [SerializeField] LayerMask jumpable;
    [SerializeField] bool hasDoubleJump = false;
    [SerializeField] public bool hasKey = false;

    [Header("Controls")]
    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference jump;
    [SerializeField] InputActionReference run;
    [SerializeField] InputActionReference attack;

    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        move.action.Enable();
        move.action.started += OnMove;
        move.action.performed += OnMove;
        move.action.canceled += OnMove;

        jump.action.Enable();
        jump.action.performed += OnJump;

        attack.action.Enable();
        attack.action.performed += OnAttack;
        
        run.action.Enable();
        run.action.performed += OnRun;
        run.action.canceled += OnRunCanceled;
    }
    
    private void OnDisable()
    {
        move.action.Disable();
        move.action.started -= OnMove;
        move.action.performed -= OnMove;
        move.action.canceled -= OnMove;

        jump.action.Disable();
        jump.action.performed -= OnJump;

        attack.action.Disable();
        attack.action.performed -= OnAttack;

        run.action.Disable();
        run.action.performed -= OnRun;
        run.action.canceled -= OnRunCanceled;
    }

    protected override void Update()
    {
        UpdateRawMove();

        base.Update();
    }

    private bool OnTheGround()
    {
        return Physics2D.Raycast(feet.position, Vector3.down, groundDistance, jumpable);
    }

    private bool canDoubleJump = false; 
    private void UpdateRawMove()
    {     
        desiredMove = rawMove;      
    }

    // public override void NotifyHit(HitBox2D hitBox2D)
    // {
    //     Debug.Log("Me han dado");
    //     gameObject.SetActive(false);
    //     Invoke(nameof(ActivatePlayer), 3f);
    // }

    private void ActivatePlayer()
    {
        gameObject.SetActive(true);
    }

    public void CollectItem(string itemType)
    {
        Debug.Log($"{itemType} collected");

        switch(itemType)
        {
            case "Double Jump":
                {
                    hasDoubleJump = true;
                    break;
                }
            case "Key":
                {
                    hasKey = true;
                    break;
                }
            case "Goal":
                {
                    Debug.Log("Fin del juego");
                    break;
                }
        }
    }

    #region Callbacks
    Vector2 rawMove;
    private void OnMove(InputAction.CallbackContext context)
    {
        rawMove = context.ReadValue<Vector2>();
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        PerformPunch();
    }

    // private void OnReset(InputAction.CallbackContext context)
    // {
    //     OnDisable();
    //     SceneManager.LoadScene(0);
    // }

    
    private void OnJump(InputAction.CallbackContext context)
    {
        if (OnTheGround() || canDoubleJump)
        {
            mustJump = true;

            if (hasDoubleJump)
            {
                canDoubleJump = !canDoubleJump;
            }
        }
    }
    
    private void OnRun(InputAction.CallbackContext context)
    {
        mustRun = true;
    }
    
    private void OnRunCanceled(InputAction.CallbackContext context)
    {
        mustRun = false;
    }
    #endregion
}
