using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueMago : MonoBehaviour
{
    public float radioDeteccion = 5.0f;
    public GameObject proyectilPrefab;
    public Transform puntoDisparo;
    public float tiempoEntreDisparos = 5.0f;
    public float velocidadProyectil = 10.0f;
    public float tiempoDeVidaBala = 5.0f;

    private Transform jugador;
    private float tiempoUltimoDisparo = 5.0f;
    private Vector2 ultimaDireccionDisparo;

    public PlayerController playerController;

    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        tiempoUltimoDisparo = Time.time;
    }

    void Update()
    {
       
        float distanciaAlJugador = Vector2.Distance(transform.position, jugador.position);

        if (distanciaAlJugador <= radioDeteccion && Time.time - tiempoUltimoDisparo >= tiempoEntreDisparos)
        {
            ultimaDireccionDisparo = (jugador.position - puntoDisparo.position).normalized;
            Disparar();
            tiempoUltimoDisparo = Time.time;
        }
        
        if (playerController != null)
        {
            float velocidadJugador = playerController.velocityModifier;
        }
    }

    void Disparar()
    {
        if (proyectilPrefab != null && puntoDisparo != null)
        {
            Vector2 direccion = (jugador.position - puntoDisparo.position).normalized;
            float angulo = Mathf.Atan2(direccion.y, direccion.x);
            GameObject proyectil = Instantiate(proyectilPrefab, puntoDisparo.position, Quaternion.identity);
            Rigidbody2D rb = proyectil.GetComponent<Rigidbody2D>();
            Vector2 velocidad = new Vector2(Mathf.Cos(angulo), Mathf.Sin(angulo)) * velocidadProyectil;
            rb.velocity = velocidad;

            StartCoroutine(DestruirBala(proyectil, tiempoDeVidaBala));
        }
    }

    private IEnumerator DestruirBala(GameObject bala, float tiempoDeVida)
    {
        yield return new WaitForSeconds(tiempoDeVida);
        Destroy(bala);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radioDeteccion);
    }
}

