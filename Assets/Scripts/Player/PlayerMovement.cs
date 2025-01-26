using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] public float speed;
    [SerializeField] public float jumpForce;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;

    public PlayerActions actions;
    private Rigidbody2D rigidBody;
    private Vector2 moveDirection;
    private bool isGrounded;
    private Animator animator;
    private bool isFacingRight = true;

    void Awake()
    {
        actions = new PlayerActions();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if(rigidBody == null)
        {
            Debug.LogError("RigidBody2D is null, please assign one");
        }

        actions.Movement.Jump.performed += ctx => Jump(ctx);

        if (isGrounded)
        {
            animator.SetBool("isJumping", !isGrounded);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ReadMovement();

        FlipSprite();

        animator.SetFloat("xVelocity", Math.Abs(rigidBody.velocity.x));
        animator.SetFloat("yVelocity", Math.Abs(rigidBody.velocity.y));
    }

    bool canAttack()
    {
        return true; // Change this for the extended GDD conditions
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void FlipSprite()
    {
        if(isFacingRight && rigidBody.velocity.x < 0f || !isFacingRight && rigidBody.velocity.x > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
    }

    private void Move()
    {
        moveDirection = actions.Movement.Move.ReadValue<Vector2>();
        rigidBody.velocity = new Vector2(moveDirection.x * speed, rigidBody.velocity.y);
    }

    private void ReadMovement()
    {
        moveDirection = actions.Movement.Move.ReadValue<Vector2>().normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
        animator.SetBool("isJumping", !isGrounded);
    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }
}
