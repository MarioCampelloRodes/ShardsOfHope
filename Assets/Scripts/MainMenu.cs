using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI lastScoreText;
    public TextMeshProUGUI lastTimeText;
    [SerializeField] private string gameScene = "BossBattle";

    void Start()
    {
        int score = PersistentInfo.singleton.GetScore();
        float time = PersistentInfo.singleton.GetTime();

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int hundredths = Mathf.FloorToInt((time * 100) % 100);

        lastScoreText.text = score.ToString();
        lastTimeText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, hundredths);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(gameScene);
    }
}
