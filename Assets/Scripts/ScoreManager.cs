using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    //singleton pa los que vienen después (grande Gustave)
    public static ScoreManager Instance;

    public TextMeshProUGUI scoreText;

    public int pointsPerInterval = 75;
    public float intervalSeconds = 5f;

    private int currentScore = 0;
    private float timer = 0f;

    void Awake()
    {
        //configurasao del Singleton
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        //cntador de tiempo
        timer += Time.deltaTime;

        if (timer >= intervalSeconds)
        {
            AddScore(pointsPerInterval);
            timer = 0f; //reiniciar el segundero
        }
    }

    //funcion para sumar puntos
    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore.ToString();
        }
    }
}
