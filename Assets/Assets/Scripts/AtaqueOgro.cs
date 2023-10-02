using UnityEngine;

public class AtaqueOgro : MonoBehaviour
{
    // Variables públicas
    public float radio = 5f;
    public float velocidad = 2f;
    public GameObject jugador; // Referencia al jugador

    // Variables privadas
    private Vector3 posicionInicial; // Posición inicial del enemigo
    private bool jugadorEnRango = false; // Indicador de si el jugador está en rango

    void Start()
    {
        posicionInicial = transform.position;
        // No es necesario buscar al jugador por su tag, ya que se ha asignado desde el inspector
    }

    void Update()
    {
        // Llamar a las funciones de comprobar rango y mover enemigo
        ComprobarRango();
        MoverEnemigo();
    }

    void ComprobarRango()
    {
        // Crear un círculo invisible con el centro en la posición del enemigo y el radio dado
        Collider2D col = Physics2D.OverlapCircle(transform.position, radio);

        // Comprobar si el círculo ha detectado al jugador
        if (col != null && col.tag == "Player")
        {
            // Cambiar el indicador a verdadero
            jugadorEnRango = true;
        }
        else
        {
            // Cambiar el indicador a falso
            jugadorEnRango = false;
        }
    }

    void MoverEnemigo()
    {
        // Usar una estructura condicional según el valor del indicador
        if (jugadorEnRango)
        {
            // Mover al enemigo hacia el jugador usando transform.position
            transform.position = Vector3.MoveTowards(transform.position, jugador.transform.position, velocidad * Time.deltaTime);
        }
        else
        {
            // Mover al enemigo hacia su posición inicial
            transform.position = Vector3.MoveTowards(transform.position, posicionInicial, velocidad * Time.deltaTime);
        }
    }
}
