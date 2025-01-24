using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] public float speed;
    [SerializeField] public float jumpForce;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private PlayerActions actions;
    private Rigidbody2D rigidBody;
    private Vector2 moveDirection;
    private bool isGrounded;

    void Awake()
    {
        actions = new PlayerActions();
        rigidBody = GetComponent<Rigidbody2D>();

        if(rigidBody == null )
        {
            Debug.LogError("RigidBody2D is null, please assign one");
        }

        actions.Movement.Jump.performed += ctx => Jump(ctx);
    }

    // Update is called once per frame
    void Update()
    {
        ReadMovement();
        CheckGround();
    }

    private void FixedUpdate()
    {
        Move();
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

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
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
