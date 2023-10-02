using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueMago : MonoBehaviour
{
    public float radioDeteccion = 5.0f; // Radio de detecci�n del jugador.
    public GameObject proyectilPrefab; // Prefab del proyectil a disparar.
    public Transform puntoDisparo; // Punto de origen del disparo.
    public float tiempoEntreDisparos = 5.0f; // Tiempo entre disparos en segundos.

    private Transform jugador; // Referencia al transform del jugador.
    private bool jugadorDetectado = false; // Indica si el jugador ha sido detectado.
    private float tiempoUltimoDisparo; // Tiempo del �ltimo disparo.

    void Start()
    {
        // Encuentra al jugador por su etiqueta (aseg�rate de etiquetar al jugador como "Player").
        jugador = GameObject.FindGameObjectWithTag("Player").transform;

        if (jugador == null)
        {
            Debug.LogError("No se encontr� al jugador. Aseg�rate de etiquetar al jugador como 'Player'.");
        }

        // Inicializa el tiempo del �ltimo disparo.
        tiempoUltimoDisparo = Time.time;
    }

    void Update()
    {
        if (jugador == null)
        {
            return;
        }

        // Calcula la distancia entre el mago y el jugador.
        float distanciaAlJugador = Vector2.Distance(transform.position, jugador.position);

        // Si el jugador est� dentro del radio de detecci�n y ha pasado suficiente tiempo, dispara.
        if (distanciaAlJugador <= radioDeteccion && Time.time - tiempoUltimoDisparo >= tiempoEntreDisparos)
        {
            jugadorDetectado = true;
            Disparar();
            tiempoUltimoDisparo = Time.time; // Actualiza el tiempo del �ltimo disparo.
        }
        else
        {
            jugadorDetectado = false;
        }
    }

    void Disparar()
    {
        // Comprueba si se ha asignado un prefab de proyectil y un punto de disparo.
        if (proyectilPrefab != null && puntoDisparo != null)
        {
            // Calcula la direcci�n hacia el jugador.
            Vector2 direccion = (jugador.position - puntoDisparo.position).normalized;

            // Calcula el �ngulo de rotaci�n basado en la direcci�n.
            float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;

            // Instancia un proyectil en el punto de disparo con la direcci�n adecuada.
            GameObject proyectil = Instantiate(proyectilPrefab, puntoDisparo.position, Quaternion.Euler(0, 0, angulo));
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Dibuja un gizmo esf�rico para visualizar el radio de detecci�n en el editor.
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radioDeteccion);
    }
}
