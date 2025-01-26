using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private float attackCooldown;
    private Animator animator;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        cooldownTimer = Time.deltaTime;

        if(playerMovement.actions.Movement.Shoot.triggered)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (cooldownTimer > attackCooldown) {
            animator.SetTrigger("Shoot");
            cooldownTimer = 0;
        }
    }
}
