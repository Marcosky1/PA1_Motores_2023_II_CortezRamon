using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Text healthText;

    //vida 
    public int maxHealth = 100;
    private int currentHealth;


    //level
    public Text nivelText; 
    private int nivel = 1; 
    private int puntaje = 0;


    private float fireTimer; // el temporizador de fuego

    private void Start()
    {
        currentHealth = maxHealth;

        ActualizarNivel();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemigo"))
        {
            DerrotarEnemigo(other.gameObject);
        }
    }

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
            Disparar(); // llamar al m�todo de disparo
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Left Click");
        }

        // actualizar el temporizador de fuego
        fireTimer -= Time.deltaTime;

        healthText.text = "Vida: " + currentHealth;
    }

    private void CheckFlip(float x_Position)
    {
        spriteRenderer.flipX = (x_Position - transform.position.x) < 0;
    }

    // el m�todo de disparo
    private void Disparar()
    {
        // comprobar si el temporizador de fuego es menor o igual a cero
        if (fireTimer <= 0)
        {
            // instanciar el prefab de la bala en el punto de disparo
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            // aplicar una fuerza a la bala en la direcci�n del punto de disparo
            rb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
            // reiniciar el temporizador de fuego
            fireTimer = fireRate;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            
        }
    }

    private void DerrotarEnemigo(GameObject enemigo)
    {
        // Supongamos que tienes una manera de determinar si un enemigo es de tipo 1 o 2.
        int tipoEnemigo = DetermineTipoEnemigo(enemigo);

        if (tipoEnemigo == 1)
        {
            puntaje += 10; // Enemigo tipo 1 otorga 10 puntos.
        }
        else if (tipoEnemigo == 2)
        {
            puntaje += 20; // Enemigo tipo 2 otorga 20 puntos.
        }

        ActualizarNivel();
        Destroy(enemigo);
    }

    private void ActualizarNivel()
    {

        if (puntaje % 100 == 0)
        {
            nivel++;
        }

        nivelText.text = "Nivel: " + nivel;
    }
    private int DetermineTipoEnemigo(GameObject enemigo)
    {
        if (enemigo.CompareTag("Enemigo1"))
        {
            return 1;
        }
        else if (enemigo.CompareTag("Enemigo2"))
        {
            return 2;
        }
        return 0;
    }


}