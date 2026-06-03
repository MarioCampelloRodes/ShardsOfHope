using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public static BossHealth Instance { get; private set; }

    [Header("Health")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    [Header("UI")]
    [SerializeField] private Image healthBarFill;

    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject gameUI;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        UpdateUI();

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void UpdateUI()
    {
        healthBarFill.fillAmount = currentHealth / maxHealth;
    }

    private void Die()
    {
        PersistentInfo.singleton.UpdateTime(TimeScore.Instance.GetElapsedTime());
        Time.timeScale = 0f;
        winScreen.SetActive(true);
        gameUI.SetActive(false);
    }
}
