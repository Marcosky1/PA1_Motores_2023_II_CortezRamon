using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueOgro : MonoBehaviour
{
    public float velocidadMovimiento = 2.0f; // Velocidad de movimiento del ogro.
    public float radioDeteccion = 5.0f; // Radio de detección del jugador.

    private Transform jugador; // Referencia al transform del jugador.
    private bool jugadorDetectado = false; // Indica si el jugador ha sido detectado.

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

        // Calcula la distancia entre el ogro y el jugador.
        float distanciaAlJugador = Vector2.Distance(transform.position, jugador.position);

        // Si el jugador está dentro del radio de detección, sigue al jugador.
        if (distanciaAlJugador <= radioDeteccion)
        {
            jugadorDetectado = true;

            // Calcula la dirección hacia el jugador.
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
        // Dibuja un gizmo esférico para visualizar el radio de detección en el editor.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioDeteccion);
    }
}
