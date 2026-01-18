using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerController : MovementController
{
    [Header("Player Movement Settings")]
    [SerializeField] Transform feet;
    [SerializeField] float groundDistance = 0.15f;
    [SerializeField] LayerMask jumpable;
    [SerializeField] bool hasDoubleJump = false;

    protected override void Awake()
    {
        base.Awake();
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
        Vector2 rawMove = Vector2.zero;

        if (Keyboard.current.aKey.isPressed)
        {
            rawMove += Vector2.left;
        } else if (Keyboard.current.dKey.isPressed)
        {
            rawMove += Vector2.right;
        }

        desiredMove = rawMove;

        if (Keyboard.current.shiftKey.wasPressedThisFrame)
        {
            mustRun = true;
        }

        if (Keyboard.current.shiftKey.wasReleasedThisFrame)
        {
            mustRun = false;
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame && (OnTheGround() || canDoubleJump))
        {
            mustJump = true;

            if (hasDoubleJump)
            {
                canDoubleJump = !canDoubleJump;
            }
        }

        if (Keyboard.current.rightAltKey.wasPressedThisFrame)
        {
            PerformPunch();
        } 
    }

    public override void NotifyHit(HitBox2D hitBox2D)
    {
        gameObject.SetActive(false);
        Invoke(nameof(ActivatePlayer), 3f);
    }

    private void ActivatePlayer()
    {
        gameObject.SetActive(true);
    }
}
