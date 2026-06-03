using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackPatterns : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform firePoint;

    [SerializeField] private BossProjectilePools pools;

    public IEnumerator HomingBurst()
    {
        for (int i = 0; i < 7; i++)
        {
            HomingProjectile projectile = pools.HomingPool.Get();

            projectile.transform.position = firePoint.position;

            projectile.Initialize(player);

            yield return new WaitForSeconds(0.15f);
        }

        yield return new WaitForSeconds(1f);
    }

    public IEnumerator WaveAttack()
    {
        WaveProjectile left = pools.WavePool.Get();
        left.Initialize(player);

        left.transform.position = firePoint.position + Vector3.left;

        WaveProjectile right = pools.WavePool.Get();
        right.Initialize(player);

        right.transform.position = firePoint.position + Vector3.right;

        yield return new WaitForSeconds(1f);

        left.Launch();
        right.Launch();

        yield return new WaitForSeconds(1f);
    }

    public IEnumerator FallingAttack()
    {
        FallingProjectile projectile = pools.FallingPool.Get();

        projectile.Initialize(player);

        yield return new WaitForSeconds(1.5f);
    }
}
