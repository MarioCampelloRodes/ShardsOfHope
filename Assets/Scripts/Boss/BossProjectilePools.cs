using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BossProjectilePools : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private HomingProjectile homingPrefab;
    [SerializeField] private WaveProjectile wavePrefab;
    [SerializeField] private FallingProjectile fallingPrefab;

    public ObjectPool<HomingProjectile> HomingPool { get; private set; }
    public ObjectPool<WaveProjectile> WavePool { get; private set; }
    public ObjectPool<FallingProjectile> FallingPool { get; private set; }

    private void Awake()
    {
        CreateHomingPool();
        CreateWavePool();
        CreateFallingPool();
    }

    private void CreateHomingPool()
    {
        HomingPool = new ObjectPool<HomingProjectile>(CreateHoming, GetHoming, ReleaseHoming, DestroyHoming, true, 20, 50);
    }

    private void CreateWavePool()
    {
        WavePool = new ObjectPool<WaveProjectile>(CreateWave, GetWave, ReleaseWave, DestroyWave, true, 2, 10);
    }

    private void CreateFallingPool()
    {
        FallingPool = new ObjectPool<FallingProjectile>(CreateFalling, GetFalling, ReleaseFalling, DestroyFalling, true, 3, 10);
    }

//HOMING PROJECTILES

    private HomingProjectile CreateHoming()
    {
        HomingProjectile p = Instantiate(homingPrefab);
        p.SetPool(HomingPool);
        p.gameObject.SetActive(false);
        return p;
    }

    private void GetHoming(HomingProjectile p)
    {
        p.gameObject.SetActive(true);
    }

    private void ReleaseHoming(HomingProjectile p)
    {
        p.gameObject.SetActive(false);
    }

    private void DestroyHoming(HomingProjectile p)
    {
        Destroy(p.gameObject);
    }

//WAVE PROJECTILES

    private WaveProjectile CreateWave()
    {
        WaveProjectile p = Instantiate(wavePrefab);
        p.SetPool(WavePool);
        p.gameObject.SetActive(false);
        return p;
    }

    private void GetWave(WaveProjectile p)
    {
        p.gameObject.SetActive(true);
    }

    private void ReleaseWave(WaveProjectile p)
    {
        p.gameObject.SetActive(false);
    }

    private void DestroyWave(WaveProjectile p)
    {
        Destroy(p.gameObject);
    }

//FALLING PROJECTILES

    private FallingProjectile CreateFalling()
    {
        FallingProjectile p = Instantiate(fallingPrefab);
        p.SetPool(FallingPool);
        p.gameObject.SetActive(false);
        return p;
    }

    private void GetFalling(FallingProjectile p)
    {
        p.gameObject.SetActive(true);
    }

    private void ReleaseFalling(FallingProjectile p)
    {
        p.gameObject.SetActive(false);
    }

    private void DestroyFalling(FallingProjectile p)
    {
        Destroy(p.gameObject);
    }
}
