using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class HomingProjectile : EnemyProjectile, IParryable
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private float homingDuration = 1.5f;

    private Transform target;
    private float timer;

    private bool locked;

    private Vector3 lockedDirection;

    private ObjectPool<HomingProjectile> pool;

    public void SetPool(ObjectPool<HomingProjectile> pool)
    {
        this.pool = pool;
    }

    public void Initialize(Transform player)
    {
        target = player;

        timer = homingDuration;

        locked = false;
    }

    private void Update()
    {
        if (!locked)
        {
            timer -= Time.deltaTime;

            Vector3 direction = (target.position - transform.position).normalized;

            transform.position += direction * speed * Time.deltaTime;

            if (timer <= 0)
            {
                lockedDirection = direction;
                locked = true;
            }
        }
        else
        {
            transform.position += lockedDirection * speed * Time.deltaTime;
        }
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
