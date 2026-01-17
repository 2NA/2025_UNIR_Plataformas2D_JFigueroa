using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float walkSpeed = 2f;
    [SerializeField] float jumpSpeed = 3f;

    [Header("Combat Settings")]
    [SerializeField] Transform punchHit;
    [SerializeField] float punchHitDuration = 0.25f;

    Rigidbody2D rb2D;
    Animator animator;
    SpriteRenderer spriteRenderer;

    protected virtual void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected Vector2 desiredMove = Vector2.zero;
    protected bool mustJump = false;
    protected bool mustPunch = false;
    protected virtual void Update()
    {
        rb2D.linearVelocityX = desiredMove.x * walkSpeed;

        if (desiredMove.x != 0f)
        {
            animator.SetBool("IsWalking", true);
        } else
        {
            animator.SetBool("IsWalking", false);
        }

        if (desiredMove.x < 0)
        {
            // spriteRenderer.flipX = true;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        } else if (desiredMove.x > 0)
        {
            // spriteRenderer.flipX = false;
            transform.localScale = Vector3.one;
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

    protected void PerformPunch()
    {
        mustPunch = true;
        punchHit.gameObject.SetActive(true);
        Invoke(nameof(DeactivatePunchHit), punchHitDuration);
    }

    private void DeactivatePunchHit()
    {
        punchHit.gameObject.SetActive(false);
    }

    public virtual void NotifyHit(HitBox2D hitBox2D)
    {
        Destroy(gameObject);
    }
}
