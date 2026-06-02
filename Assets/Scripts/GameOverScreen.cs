using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    private void OnEnable()
    {
        //actualizar los datos
        int score = PersistentInfo.singleton.GetScore();
        float time = PersistentInfo.singleton.GetTime();

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int hundredths = Mathf.FloorToInt((time * 100) % 100);

        scoreText.text = score.ToString();
        timeText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, hundredths);
    }

    public void OnContinue()
    {
        PersistentInfo.singleton.Reset();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
