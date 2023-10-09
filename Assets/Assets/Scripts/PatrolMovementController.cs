using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMovementController : MonoBehaviour
{
    [SerializeField] private Transform[] checkpointsPatrol;
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float velocityModifier = 10f;
    [SerializeField] private LayerMask playerLayer;
    private Transform currentPositionTarget;
    private int patrolPos = 0;
    private bool playerInSight = false;
    private float originalVelocityModifier;
    private float raycastDistance = 5f;
    private void Start()
    {
        currentPositionTarget = checkpointsPatrol[patrolPos];
        transform.position = currentPositionTarget.position;
        originalVelocityModifier = velocityModifier;
    }

    private void Update()
    {
        CheckNewPoint();
        CheckForPlayer();
        if (playerInSight)
        {
            velocityModifier = originalVelocityModifier * 2;
            FollowPlayer();
        }
        else
        {
            velocityModifier = originalVelocityModifier;
        }

        animatorController.SetVelocity(velocityCharacter: myRBD2.velocity.magnitude);
    }
    private void CheckForPlayer()
    {
        Vector2 direction = spriteRenderer.flipX ? Vector2.left : Vector2.right;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 10f, playerLayer);

        if (hit.collider != null && hit.collider.gameObject.tag == "Player")
        {
            Debug.DrawRay(transform.position, direction * raycastDistance, Color.red);
            playerInSight = true;
        }
        else
        {
            Debug.DrawRay(transform.position, direction * raycastDistance, Color.white);
            playerInSight = false;
        }
    }

    private void FollowPlayer()
    {
        myRBD2.velocity = (currentPositionTarget.position - transform.position).normalized * velocityModifier;
        CheckFlip(myRBD2.velocity.x);
    }
    private void CheckNewPoint()
    {
        if (Mathf.Abs((transform.position - currentPositionTarget.position).magnitude) < 0.25)
        {
            patrolPos = patrolPos + 1 == checkpointsPatrol.Length ? 0 : patrolPos + 1;
            currentPositionTarget = checkpointsPatrol[patrolPos];
            myRBD2.velocity = (currentPositionTarget.position - transform.position).normalized * velocityModifier;
            CheckFlip(myRBD2.velocity.x);
        }

    }

    private void CheckFlip(float x_Position)
    {
        spriteRenderer.flipX = (x_Position - transform.position.x) < 0;
    }
}