using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentInfo : MonoBehaviour
{
    public static PersistentInfo singleton;

    [SerializeField] private int score;
    [SerializeField] private float time;
    [SerializeField] private int rank;
    public int GetScore() => score; 
    public float GetTime() => time;

    public int GetRank() => rank;

    //para las instancias usar el awake en vez del start
    private void Awake()
    {
        //cuando no hay nadie como singleton, se asigna y se marca para que no se destruya
        if(singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);

            SaveManager.OnLoadedData += (SaveData saveData) =>
            {
                //actualiza el score, time y rank
                score = saveData.playerScore;
                time = saveData.bestTime;
                rank = saveData.bestRank;
            };
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
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
    public void UpdateRank(int newRank)
    {
        rank = newRank;
        SaveManager.Save();
    }

    //se añade al callback de guardar info
    void Save(SaveData saveData)
    {
        //actualizar los datos de guardado con la puntuación del score y el time
        saveData.playerScore = score;
        saveData.bestTime = time;
        saveData.bestRank = rank;
    }

}
