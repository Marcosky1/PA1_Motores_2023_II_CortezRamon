using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SeguimientoCamara : MonoBehaviour
{
    public Transform jugador;
    public float velocidadSeguimiento = 1f; 

    private CinemachineVirtualCamera virtualCamera; 

    private void Start()
    {
       
        virtualCamera = GetComponent<CinemachineVirtualCamera>();

       
        if (virtualCamera.Follow == null)
        {
           
            virtualCamera.Follow = jugador;
        }
    }
    private void Update()
    {
        Vector2 nuevaPosicion = Vector2.Lerp (transform.position, jugador.position, velocidadSeguimiento * Time.deltaTime);
        transform.position = new Vector3(nuevaPosicion.x, nuevaPosicion.y,-10);
    }
}

