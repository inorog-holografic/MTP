using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;

public class MoveController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private float xInput;
    private float lastFacingDirection = 1; 

    [SerializeField] private int maxJumps = 5;
    private int jumpsRemaining;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;

    [SerializeField] private LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        jumpsRemaining = maxJumps;

        lastFacingDirection = spriteRenderer.flipX ? -1 : 1;
    }

    void Update()
    {

        Jump();
        Movement();
        UpdateAnimations();

        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            
            anim.SetTrigger("turn");
        }

        RaycastHit2D hit = Physics2D.Raycast(
        groundCheck.position,
        Vector2.down,
        1f,
        groundLayer);

        if (hit)
        {
            float angle = Vector2.SignedAngle(Vector2.up, hit.normal);

            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.Euler(0, 0, angle),
                Time.deltaTime * 10f);
        }
    }

    private void Movement()
    {
        bool grounded = IsGrounded();
        xInput = 0;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) xInput = -1;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) xInput = 1;
        }

        
        rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);

       
        if (xInput != 0)
        {
            if (xInput != lastFacingDirection)
            {
                

                lastFacingDirection = xInput;
                spriteRenderer.flipX = (xInput < 0);

                anim.SetTrigger("turn");
            }
        }

        if (xInput != 0)
        {
            
        }
        //else if (!grounded && xInput != 0)
        //{
        //    spriteRenderer.flipX = (xInput < 0);
        //    lastFacingDirection = xInput;
        //}
    }

 

    private void UpdateAnimations()
    {
       
        bool grounded = IsGrounded();
       
        bool strictlyRunning = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        anim.SetBool("isRunning", strictlyRunning);
       
        anim.SetBool("isGrounded", grounded);

        
        float verticalVelocity = rb.linearVelocity.y;

        if (grounded)
        {
            verticalVelocity = 0f;
        }
        else if (Mathf.Abs(verticalVelocity) < 0.1f)
        {
            verticalVelocity = 0f;
        }

        if (Mathf.Abs(verticalVelocity) < 1f)
        {
            verticalVelocity = 0f;
        }

        anim.SetFloat("yVelocity", verticalVelocity);
        

    }

    private void Jump()
    {
        if (IsGrounded())
        {
            jumpsRemaining = maxJumps;
        }


        if (Keyboard.current.spaceKey.wasPressedThisFrame && jumpsRemaining > 0)
        {

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            jumpsRemaining--;
            Debug.Log("Jumps Left: " + jumpsRemaining);
        }
    }



    private bool IsGrounded()
    {
        CapsuleCollider2D capsule = GetComponent<CapsuleCollider2D>();

        RaycastHit2D hit = Physics2D.BoxCast(
            capsule.bounds.center,
            capsule.bounds.size * 0.95f,
            0f,
            Vector2.down,
            0.1f,
            groundLayer
        );

        return hit.collider != null;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}