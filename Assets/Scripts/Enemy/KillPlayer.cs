using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{
    public static KillPlayer Instance;
    public GameObject gameOverPanel;
    public GameObject gameUI;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        Die();
    }

    public void Die()
    {
        PersistentInfo.singleton.UpdateTime(TimeScore.Instance.GetElapsedTime());
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        gameUI.SetActive(false);
    }
}

