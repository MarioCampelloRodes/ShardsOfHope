using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSaveSystem : MonoBehaviour
{
    private float currentTime = 0;
    private int currentScore = 0;

    void Update()
    {
        // 1. Simular el paso del tiempo
        currentTime += Time.deltaTime;

        // 2. Probar el SCORE: Al pulsar la tecla "S", sumamos 100 puntos y guardamos
        if (Input.GetKeyDown(KeyCode.S))
        {
            currentScore += 100;
            PersistenInfo.singleton.UpdateScore(currentScore);
            Debug.Log("Score guardado: " + currentScore);
        }

        // 3. Probar el TIEMPO: Al pulsar la tecla "T", guardamos el tiempo actual
        if (Input.GetKeyDown(KeyCode.T))
        {
            PersistenInfo.singleton.UpdateTime(currentTime);
            Debug.Log("Tiempo guardado: " + currentTime + " segundos");
        }

        // 4. Probar CARGA: Al pulsar "G", forzamos un guardado general
        if (Input.GetKeyDown(KeyCode.G))
        {
            SaveManager.Save();
            Debug.Log("Guardado manual forzado en: " + Application.persistentDataPath);
        }
    }

    private void Start()
    {
        // Esto te dirá en la consola exactamente dónde buscar el archivo .kebab
        Debug.Log("Ruta de guardado: " + Application.persistentDataPath);
        Debug.Log("Pulsa 'S' Score, 'T' Tiempo y 'G' para crear el archivo .kebab");
    }
}

