using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class FallingProjectile : EnemyProjectile
{
    [SerializeField] private float delay = 1.5f;
    [SerializeField] private float speed = 20f;
    [SerializeField] private float spawnHeight = 10f;

    private bool falling;

    private ObjectPool<FallingProjectile> pool;

    public void SetPool(ObjectPool<FallingProjectile> pool)
    {
        this.pool = pool;
    }

    public void Initialize(Transform player)
    {
        transform.position = new Vector3(player.position.x, 0,player.position.z) + Vector3.up * spawnHeight;

        StopAllCoroutines();
        StartCoroutine(FallRoutine());
    }

    private IEnumerator FallRoutine()
    {
        falling = false;

        yield return new WaitForSeconds(delay);

        falling = true;
    }

    private void Update()
    {
        if (!falling)
            return;

        transform.position += Vector3.down *
            speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pool.Release(this);
        }
    }

    private void OnBecameInvisible()
    {
        pool.Release(this);
    }
}
