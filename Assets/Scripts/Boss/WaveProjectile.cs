using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class WaveProjectile : EnemyProjectile, IParryable
{
    [SerializeField] private float speed = 40f;

    private bool launched;

    private Transform target;

    private ObjectPool<WaveProjectile> pool;


    public void SetPool(ObjectPool<WaveProjectile> pool)
    {
        this.pool = pool;
    }

    public void Initialize(Transform player)
    {
        target = player;
    }

    public void Launch()
    {
        launched = true;
    }

    private void OnEnable()
    {
        launched = false;
    }

    private void Update()
    {
        if (!launched)
            return;

        Vector3 direction = (target.position - transform.position).normalized;

        transform.position += direction * speed * Time.deltaTime;
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

    public void Parry()
    {
        ScoreManager.Instance.AddScore(scoreReward);

        pool.Release(this);
    }
}
