using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    public float speed = 10f;
    public bool followPlayer = true; 

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
            //mover hacia el jugador en X
            float directionX = player.position.x - transform.position.x;
            //mover hacia adelante (en z)
            movement = new Vector3(directionX, 0, -1f).normalized;
        }
        else
        {
            //solo mover recto hacia adelante (sin seguir al jugador)
            movement = new Vector3(0, 0, -1f);
        }

        transform.position += movement * speed * Time.deltaTime;

        //rotar para efecto visual
        transform.Rotate(Vector3.up * 200f * Time.deltaTime);

        //destruir si pasa al jugador
        if (transform.position.z < player.position.z - 5f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //si choca con el jugador
        if (other.CompareTag("Player"))
        {
            if (PlayerHurtbox.Instance != null)
            {
                PlayerHurtbox.Instance.onHurt?.Invoke(10f);
            }

            //reiniciar la escena
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }

        // El meteorito se destruye siempre al chocar
        Destroy(gameObject);
    }
}
