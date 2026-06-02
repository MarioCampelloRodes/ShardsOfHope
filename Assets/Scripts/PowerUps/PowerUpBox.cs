using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBox : MonoBehaviour
{
    [Header("PowerUp que contiene esta caja")]
    [SerializeField] private PowerUp powerUp;

    public float speed = 10f;

    private void Update()
    {
        Vector3 movement = new Vector3(0, 0, -1f);

        transform.position += movement * speed * Time.deltaTime;

        //destruir si pasa al jugador
        if (transform.position.z < -5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            PowerUpSlot slot = other.GetComponent<PowerUpSlot>();

            // Sustituye el powerup anterior si existía
            slot.StorePowerUp(powerUp);

            Destroy(this.gameObject);
        }
    }
}
