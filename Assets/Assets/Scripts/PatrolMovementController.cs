
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMovementController : MonoBehaviour
{
    [SerializeField] private Transform[] checkpointsPatrol;
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float velocityModifier = 5f;
    private Transform currentPositionTarget;
    private int patrolPos = 0;

    // Variables para el raycast
    public float raycastRange = 5f;
    private bool playerDetected = false;
    public LayerMask obstacleLayer;

    private void Start()
    {
        currentPositionTarget = checkpointsPatrol[patrolPos];
        transform.position = currentPositionTarget.position;
    }

    private void Update()
    {
        CheckNewPoint();

        animatorController.SetVelocity(velocityCharacter: myRBD2.velocity.magnitude);

        CheckRaycast();
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

    private void CheckRaycast()
    {
        // Lanzar el rayo desde la posición del enemigo hacia la dirección de su velocidad
        RaycastHit2D hit = Physics2D.Raycast(transform.position, myRBD2.velocity.normalized, raycastRange, obstacleLayer, -Mathf.Infinity, Mathf.Infinity);

        // Dibujar el rayo en el editor con color verde
        Debug.DrawRay(transform.position, myRBD2.velocity.normalized * raycastRange, Color.green);

        // Comprobar si el rayo ha golpeado al jugador
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            // Cambiar el estado de detección a verdadero y aumentar la velocidad del enemigo
            playerDetected = true;
            myRBD2.velocity *= 1.5f;
        }
        else
        {
            // Cambiar el estado de detección a falso y restaurar la velocidad del enemigo
            playerDetected = false;
            myRBD2.velocity /= 1.5f;
        }
    }
}
