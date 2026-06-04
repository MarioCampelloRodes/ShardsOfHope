using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshProUGUI scoreText;

    public float pointsPerSecond = 15f;
    private float currentScore = 0f;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        //acumular puntos porque necesitamos estímulos constantes para prestar atención
        currentScore += GetCurrentPointsPerSecond() * Time.deltaTime;
        PersistentInfo.singleton.UpdateScore(GetScore());
        UpdateUI();
    }

    protected virtual float GetCurrentPointsPerSecond()
    {
        if (RankSystem.Instance == null) return pointsPerSecond;
        return pointsPerSecond * RankSystem.Instance.GetMultiplier();
    }

    //devolver el score como int
    public int GetScore() => Mathf.FloorToInt(currentScore);

    public void AddScore(int amount)
    {
        currentScore += amount;
        PersistentInfo.singleton.UpdateScore(GetScore());
        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = GetScore().ToString();
    }

}

