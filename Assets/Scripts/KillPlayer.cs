using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{
    public static KillPlayer Instance;
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
        //if (ScoreManager.Instance != null)
        //{
        //    ScoreManager.Instance.StopScore();
        //}

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

