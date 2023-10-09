using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueOgro : MonoBehaviour
{
    public float velocidadMovimiento = 2.0f; 
    public float radioDeteccion = 5.0f;

    private Transform jugador; 
    private bool jugadorDetectado = false; // Indica si el jugador ha sido detectado.

    public PlayerController playerController;

    private void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;

        if (jugador == null)
        {
            Debug.LogError("No se encontró al jugador. Asegúrate de etiquetar al jugador como 'Player'.");
        }
    }

    private void Update()
    {
        if (jugador == null)
        {
            return;
        }

        float distanciaAlJugador = Vector2.Distance(transform.position, jugador.position);

        if (distanciaAlJugador <= radioDeteccion)
        {
            jugadorDetectado = true;

            Vector3 direccionAlJugador = (jugador.position - transform.position).normalized;

            transform.Translate(direccionAlJugador * velocidadMovimiento * Time.deltaTime);
        }
        else
        {
            jugadorDetectado = false;
        }

        if (playerController != null)
        {
            float velocidadJugador = playerController.velocityModifier;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioDeteccion);
    }
}
