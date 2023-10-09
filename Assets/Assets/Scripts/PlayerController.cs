using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] public float velocityModifier = 5f;
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint; // el punto de disparo
    [SerializeField] private float bulletSpeed = 10f; 
    [SerializeField] private float fireRate = 0.5f; // la tasa de fuego

    //vida 
    public Text healthText;
    public int maxHealth = 100;
    private int currentHealth;


    //level
    public Text nivelText; 
    private int nivel = 1; 
    private int puntaje = 0;

    private int tiempoDeVidaBala = 5;

    private float fireTimer; // el temporizador de fuego

    public static PlayerController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


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
            Disparar(); 
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Left Click");
        }

        fireTimer -= Time.deltaTime;

        healthText.text = "Vida: " + currentHealth;
    }

    private void CheckFlip(float x_Position)
    {
        spriteRenderer.flipX = (x_Position - transform.position.x) < 0;
    }

    private void Disparar()
    {
        if (fireTimer <= 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, rayDistance);
            Vector2 initialPosition = transform.position;

            if (hit.collider != null)
            {
                Vector2 impactPoint = hit.point;
                Vector2 direction = (impactPoint - initialPosition).normalized;
                GameObject bullet = Instantiate(bulletPrefab, initialPosition, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

                rb.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);

                StartCoroutine(DestruirBala(bullet, tiempoDeVidaBala));
            }
            else
            {
                GameObject bullet = Instantiate(bulletPrefab, initialPosition, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

                rb.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);

                StartCoroutine(DestruirBala(bullet, tiempoDeVidaBala));
            }

            fireTimer = fireRate;
        }
    }

    private IEnumerator DestruirBala(GameObject bullet, float tiempoDeVida)
    {
        yield return new WaitForSeconds(tiempoDeVida);
        Destroy(bullet);
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
        int tipoEnemigo = DetermineTipoEnemigo(enemigo);

        if (tipoEnemigo == 1)
        {
            puntaje += 10; 
        }
        else if (tipoEnemigo == 2)
        {
            puntaje += 20; 
        }

        ActualizarNivel();
        Destroy(enemigo);
    }

    private IEnumerator ActualizarNivelCoroutine()
    {
        while (true) 
        {
            yield return new WaitForSeconds(20f); 
            nivel += 10; 
            nivelText.text = "Nivel: " + nivel;
        }
    }



    private void ActualizarNivel()
    {
        StartCoroutine(ActualizarNivelCoroutine());
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

