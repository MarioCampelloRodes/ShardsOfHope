using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class MeteoriteSpawner : MonoBehaviour
    {
    public float spawnRadius = 6f;
    public float distanciaSpawn = 50f;
    public GameObject[] meteoritoPrefabs; 
    public Vector2 SpawnTimeRange = Vector2.one;
    private Transform jugador;

    void Start()
    {
        //encontrar al jugador
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            jugador = playerObj.transform;
        }
        StartCoroutine(SpawnCO());
    }

    void SpawnThing()
    {
        if (jugador == null) return;

        //vrificar que hay prefabs en el array
        if (meteoritoPrefabs == null || meteoritoPrefabs.Length == 0)
        {
            Debug.LogWarning("No hay prefabs asignados en el array de meteoritos");
            return;
        }

        // Usar la posicion del jugador en X y Z, pero la Y del spawner
        Vector3 spawnOrigin = new Vector3(jugador.position.x, transform.position.y, jugador.position.z);

        // Calcular posición aleatoria en X (izquierda-derecha)
        Vector3 spawnPosition = Random.insideUnitSphere * spawnRadius + spawnOrigin;
        spawnPosition.y = transform.position.y; // Mantener la altura del spawner

        //mover la posición hacia adelante (en Z)
        spawnPosition.z += distanciaSpawn;

        //elegir un prefab aleatorio del array
        int randomIndex = Random.Range(0, meteoritoPrefabs.Length);
        GameObject prefabElegido = meteoritoPrefabs[randomIndex];

        //spawnear el meteorito
        Instantiate(prefabElegido, spawnPosition, prefabElegido.transform.rotation);
    }

    IEnumerator SpawnCO()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(SpawnTimeRange.x, SpawnTimeRange.y));
            SpawnThing();
        }
    }

    private void OnDrawGizmos()
    {
        // Buscar jugador para los gizmos
        if (jugador == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                jugador = playerObj.transform;
            }
        }

        if (jugador != null)
        {
            Gizmos.color = Color.magenta;
            //haser el dibujinchi del spawner alante del jugador
            Vector3 gizmoPosition = new Vector3(jugador.position.x, transform.position.y, jugador.position.z + distanciaSpawn);
            Gizmos.DrawWireSphere(gizmoPosition, spawnRadius);
        }
    }
}
