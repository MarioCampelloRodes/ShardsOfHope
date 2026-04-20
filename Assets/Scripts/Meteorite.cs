using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    public float speed = 10f;
    public bool followPlayer = true; // Bool para activar/desactivar el seguimiento

    private Transform player;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("No se encontró el jugador con tag 'Player'");
        }
    }

    void Update()
    {
        if (player == null) return;

        Vector3 movement;

        if (followPlayer)
        {
            // Mover hacia el jugador en X (izquierda-derecha)
            float directionX = player.position.x - transform.position.x;
            // Mover hacia adelante (hacia la cámara/jugador) en Z
            movement = new Vector3(directionX, 0, -1f).normalized;
        }
        else
        {
            // Solo mover recto hacia adelante (sin seguir al jugador)
            movement = new Vector3(0, 0, -1f);
        }

        transform.position += movement * speed * Time.deltaTime;

        // Rotar para efecto visual
        transform.Rotate(Vector3.up * 200f * Time.deltaTime);

        // Destruir si pasa al jugador
        if (transform.position.z < player.position.z - 5f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
