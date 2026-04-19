using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    public float velocidad = 10f;
    private Transform jugador;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            jugador = playerObj.transform;
        }
        else
        {
            Debug.LogError("No se encontró el jugador con tag 'Player'");
        }
    }

    void Update()
    {
        if (jugador == null) return;

        // Mover hacia el jugador en X (izquierda-derecha)
        float direccionX = jugador.position.x - transform.position.x;

        // Mover hacia adelante (hacia la cámara/jugador) en Z
        Vector3 movimiento = new Vector3(direccionX, 0, -1f).normalized;
        transform.position += movimiento * velocidad * Time.deltaTime;

        // Rotar para efecto visual
        transform.Rotate(Vector3.up * 200f * Time.deltaTime);

        // Destruir si pasa al jugador
        if (transform.position.z < jugador.position.z - 5f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
