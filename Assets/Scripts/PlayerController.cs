using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float walkSpeed = 2f;
    [SerializeField] float jumpSpeed = 3f;

    Rigidbody2D rb2D;
    Animator animator;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    Vector2 rawMove = Vector2.zero;
    bool mustJump = false;
    bool mustPunch = false;
    void Update()
    {
        UpdateRawMove();

        rb2D.linearVelocityX = rawMove.x * walkSpeed;

        if (rawMove.x != 0f)
        {
            animator.SetBool("IsWalking", true);
        } else
        {
            animator.SetBool("IsWalking", false);
        }

        if (rawMove.x < 0)
        {
            spriteRenderer.flipX = true;
        } else if (rawMove.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        if (mustJump)
        {
            mustJump = false;
            rb2D.linearVelocityY = jumpSpeed;
            animator.SetTrigger("PerformJump");
        }
        
        if (mustPunch)
        {
            mustPunch = false;
            animator.SetTrigger("PerformPunch");
        }
    }

    private void UpdateRawMove()
    {
        rawMove = Vector2.zero;

        if (Keyboard.current.aKey.isPressed)
        {
            rawMove += Vector2.left;
        } else if (Keyboard.current.dKey.isPressed)
        {
            rawMove += Vector2.right;
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            mustJump = true;
        }

        if (Keyboard.current.rightAltKey.wasPressedThisFrame)
        {
            mustPunch = true;
        }
    }
}
