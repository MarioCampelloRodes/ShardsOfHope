using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;


public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private List<Platform> platformPrefabs;
    //longitud de la plataforma en Z (es el valor de la escala en Z si utilizas un cubo)
    [SerializeField] private float segmentLength = 20f;
    //segmentos visibles a la vez
    [SerializeField] private int visibleSegments = 5;
    //punto Z desde donde aparecen los segmentos (poner en 0 si el spawner está a los pies del jugador)
    [SerializeField] private float spawnZ = 0f;
 
    public ObjectPool<Platform> platformPool;

    private bool isFirstPlatform = true;
 
    // Z donde se colocara el proximo segmento
    private float nextSpawnZ;

    private void Start()
    {

        platformPool = new ObjectPool<Platform>(
            CreatePlatform,
            GetPlatform,
            ReleasePlatform
        );
 
        nextSpawnZ = spawnZ;
 
        // Llenar el suelo inicial con tantos segmentos como sean visibles
        for (int i = 0; i < visibleSegments; i++)
        {
            SpawnSegment();
        }
    }

    private Platform GetRandomPrefab()
    {
        if (platformPrefabs == null || platformPrefabs.Count == 0)
        {
            Debug.LogError("No hay prefabs de plataforma asignados en el Inspector.");
            return null;
        }

        if (isFirstPlatform)
        {
            isFirstPlatform = false;
            return platformPrefabs[0];
        }

        return platformPrefabs[Random.Range(0, platformPrefabs.Count)];
    }
    //esta funcion se llama al crear el pool por tantas veces como objetos pueda tener
    //por ejemplo, si se especifica un tamaño de 20 para el pool, llama a la funcion 20 veces
    private Platform CreatePlatform()
    {
        Platform prefab = GetRandomPrefab();
        //crear una nueva plataforma
        Platform platform = Instantiate(prefab);
        //asignar el pool de la plataforma
        platform.platformPool = platformPool;
        //desactivar la plataforma para que esté oculta
        platform.gameObject.SetActive(false);
        return platform;
    }
 
    //al sacar un segmento del pool, se coloca justo donde terminó el anterior
    private void GetPlatform(Platform platform)
    {
        //activar la plataforma después de sacarla del pool
        platform.gameObject.SetActive(true);
        //colocar la plataforma en su posición en Z
        //la division entre dos es para compensar que el pivote de los cubos está en el centro
        platform.transform.position = new Vector3(transform.position.x, transform.position.y, nextSpawnZ + segmentLength / 2);
        //hacer que el nuevo segmento se coloque detrás
        nextSpawnZ += segmentLength;
    }
 
    private void ReleasePlatform(Platform platform)
    {
        //resetear la plataforma pa que salga nuevecita y fresca
        platform.ResetPlatform();
        platform.gameObject.SetActive(false);
        nextSpawnZ = platform.transform.position.z + (segmentLength * (visibleSegments - 1));
        //En cuanto uno se libera, generamos el siguiente por delante 
        //para que el suelo no tenga huecos
        SpawnSegment();
    }
 
    private void SpawnSegment()
    {
        platformPool.Get();
    }
}
