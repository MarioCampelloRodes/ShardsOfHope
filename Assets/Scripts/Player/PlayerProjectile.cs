using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerProjectile : MonoBehaviour
{
    public ObjectPool<PlayerProjectile> pool;

    [SerializeField] private float speed = 20f;
    [SerializeField] private float damage = 10f;

    private Transform target;

    public void Shoot(Transform boss)
    {
        target = boss;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boss"))
        {
            //Evento recibir daño boss (Vida, actualizar UI, aumentar score)
            pool.Release(this);
        }
    }
}
