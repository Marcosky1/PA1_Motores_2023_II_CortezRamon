using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] private float velocityModifier = 5f;
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject bulletPrefab; // el prefab de la bala
    [SerializeField] private Transform firePoint; // el punto de disparo
    [SerializeField] private float bulletSpeed = 10f; // la velocidad de la bala
    [SerializeField] private float fireRate = 0.5f; // la tasa de fuego

    private float fireTimer; // el temporizador de fuego

    private void Update()
    {
        Vector2 movementPlayer = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        myRBD2.velocity = movementPlayer * velocityModifier;

        animatorController.SetVelocity(velocityCharacter: myRBD2.velocity.magnitude);

        Vector2 mouseInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        CheckFlip(mouseInput.x);

        Debug.DrawRay(transform.position, mouseInput.normalized * rayDistance, Color.red);

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Right Click");
            Disparar(); // llamar al método de disparo
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Left Click");
        }

        // actualizar el temporizador de fuego
        fireTimer -= Time.deltaTime;
    }

    private void CheckFlip(float x_Position)
    {
        spriteRenderer.flipX = (x_Position - transform.position.x) < 0;
    }

    // el método de disparo
    private void Disparar()
    {
        // comprobar si el temporizador de fuego es menor o igual a cero
        if (fireTimer <= 0)
        {
            // instanciar el prefab de la bala en el punto de disparo
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            // aplicar una fuerza a la bala en la dirección del punto de disparo
            rb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
            // reiniciar el temporizador de fuego
            fireTimer = fireRate;
        }
    }
}
