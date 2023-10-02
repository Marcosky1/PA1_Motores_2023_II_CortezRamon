using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueOgro : MonoBehaviour
{
    public float velocidadMovimiento = 2.0f; // Velocidad de movimiento del ogro.
    public float radioDeteccion = 5.0f; // Radio de detecci�n del jugador.

    private Transform jugador; // Referencia al transform del jugador.
    private bool jugadorDetectado = false; // Indica si el jugador ha sido detectado.

    private void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;

        if (jugador == null)
        {
            Debug.LogError("No se encontr� al jugador. Aseg�rate de etiquetar al jugador como 'Player'.");
        }
    }

    private void Update()
    {
        if (jugador == null)
        {
            return;
        }

        // Calcula la distancia entre el ogro y el jugador.
        float distanciaAlJugador = Vector2.Distance(transform.position, jugador.position);

        // Si el jugador est� dentro del radio de detecci�n, sigue al jugador.
        if (distanciaAlJugador <= radioDeteccion)
        {
            jugadorDetectado = true;

            // Calcula la direcci�n hacia el jugador.
            Vector3 direccionAlJugador = (jugador.position - transform.position).normalized;

            // Mueve al ogro hacia el jugador.
            transform.Translate(direccionAlJugador * velocidadMovimiento * Time.deltaTime);
        }
        else
        {
            jugadorDetectado = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Dibuja un gizmo esf�rico para visualizar el radio de detecci�n en el editor.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioDeteccion);
    }
}
