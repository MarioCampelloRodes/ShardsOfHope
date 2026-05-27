using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentInfo : MonoBehaviour
{
    public static PersistentInfo singleton;

    [SerializeField] private int score;
    [SerializeField] private float time;
    public int GetScore() => score; 
    public float GetTime() => time;

    //para las instancias se usa el awake en vez del start
    private void Awake()
    {
        //cuando no hay nadie como singleton, se asigna y se marca para que no se destruya
        if(singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);

            //añadir una funcion al callback de datos cargados
            //este codigo tan feo D: es una funcion anonima. Es como una funcion normal
            //pero se crea en el momento para añadirla al callback
            //entre los parentesis hay que añadir un SaveData porque el callback lo usa como parametro
            SaveManager.OnLoadedData += (SaveData saveData) =>
            {
                //actualiza el score y el time
                score = saveData.playerScore;
                time = saveData.bestTime;
            };
        }
        //si al iniciar ya hay un singleton, este objeto debe destruirse para que no haya duplicados
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        //añadir la funcion de guardar al callback de guardar datos
        SaveManager.OnSaveData += Save;
    }
    public void Reset()
    {
        score = 0;
        time = 0f;
        SaveManager.Save();
    }
    private void OnDisable()
    {
        SaveManager.OnSaveData -= Save;
    }
    public void UpdateScore(int newScore)
    {
        score = newScore;
        SaveManager.Save();
    }
    public void UpdateTime(float newTime)
    {
        time = newTime;
        SaveManager.Save();
    }

    //se añade al callback de guardar info
    void Save(SaveData saveData)
    {
        //actualizar los datos de guardado con la puntuación del score y el time
        saveData.playerScore = score;
        saveData.bestTime = time;
    }

}
