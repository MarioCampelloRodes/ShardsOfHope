using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _sprite3;
    [SerializeField] private GameObject _sprite2;
    [SerializeField] private GameObject _sprite1;
    [SerializeField] private string mainMenuScene = "MainMenu";
    private bool _isCountingDown = false;
    [SerializeField] private float _countdownDuration = 1f;

    //variable para no activar el menú de pausa durante el panel de GameOver
    [SerializeField] private GameObject gameOverPanel;

    void Start()
    {
        _pauseMenu.SetActive(false);
        _sprite3.SetActive(false);
        _sprite2.SetActive(false);
        _sprite1.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isCountingDown) return;
            if (gameOverPanel.activeSelf) return;

            if (_pauseMenu.activeSelf)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        if (_isCountingDown) return;
        if (gameOverPanel.activeSelf) return;
        _pauseMenu.SetActive(true);
        CursorManager.Instance.ShowCursor();
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        if (_isCountingDown) return;
        _pauseMenu.SetActive(false);
        CursorManager.Instance.HideCursor();
        Time.timeScale = 0f;
        StartCoroutine(CountdownCoroutine());
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("CIÉRRATE SÉSAMO");
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        CursorManager.Instance.ShowCursor();
        SceneManager.LoadScene(mainMenuScene);
    }
    private IEnumerator CountdownCoroutine()
    {
        _isCountingDown = true;
        _sprite3.SetActive(true);
        yield return new WaitForSecondsRealtime(_countdownDuration);
        _sprite3.SetActive(false);
        _sprite2.SetActive(true);
        yield return new WaitForSecondsRealtime(_countdownDuration);
        _sprite2.SetActive(false);
        _sprite1.SetActive(true);
        yield return new WaitForSecondsRealtime(_countdownDuration);
        _sprite1.SetActive(false);
        Time.timeScale = 1f;
        _isCountingDown = false;
    }
}
