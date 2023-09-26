using System.Collections;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public float vidaMaxima = 100f;
    public float velocidadDisparo = 10f;
    public GameObject prefabDisparo;
    public Transform puntoDisparo;
    public float fuerzaImpulso = 500f;
    public Transform puntoInicio;

    private float vidaActual;

    private void Start()
    {
        vidaActual = vidaMaxima;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Disparar();
        }
    }

    private void Disparar()
    {
        GameObject disparo = Instantiate(prefabDisparo, puntoDisparo.position, Quaternion.identity);
        Rigidbody rb = disparo.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * velocidadDisparo;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            RestarVida(10f);

            Vector2 direccion = (transform.position - collision.transform.position).normalized;
            GetComponent<Rigidbody>().AddForce(direccion * fuerzaImpulso);
        }

    }

    private void RestarVida(float cantidad)
    {
        vidaActual -= cantidad;
        if (vidaActual <= 0)
        {
            vidaActual = vidaMaxima;
            transform.position = puntoInicio.position;
        }
    }
}

