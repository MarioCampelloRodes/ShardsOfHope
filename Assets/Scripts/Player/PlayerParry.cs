using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerParry : MonoBehaviour
{
    public GameObject parryHitbox;

    [Header("Stamina")]
    private float maxStamina = 100f;
    [SerializeField] private float currentStamina;
    public float staminaRegenRate = 15f;
    [SerializeField] private float staminaCost = 20f;

    [Header("UI")]
    [SerializeField] private Image staminaImageFill;
    [SerializeField] private CanvasGroup staminaCanvasGroup;

    [Header("Fade")]
    [SerializeField] private float timeBeforeFade = 2f;
    [SerializeField] private float fadeSpeed = 2f;

    [Header("LaserShoot")]
    public bool shootLaser = false;

    private bool isParrying;
    private float fullStaminaTimer;

    private void Start()
    {
        currentStamina = maxStamina;
        UpdateStaminaUI();
    }
    void Update()
    {
        RegenerateStamina();
        UpdateStaminaUI();
        HandleUIFade();

        if (Input.GetButtonDown("Parry"))
        {
            if (!isParrying && currentStamina >= staminaCost)
            {
                currentStamina -= staminaCost;
                StartCoroutine(AttackCrt());
            }
        }
    }

    private IEnumerator AttackCrt()
    {
        isParrying = true;

        parryHitbox.SetActive(true); 
        yield return new WaitForSeconds(0.15f);
        parryHitbox.SetActive(false);

        isParrying = false;
    }
    private void RegenerateStamina()
    {
        if (currentStamina < maxStamina)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        }
    }

    private void UpdateStaminaUI()
    {
        staminaImageFill.fillAmount = currentStamina / maxStamina;
    }

    private void HandleUIFade()
    {
        if (currentStamina >= maxStamina)
        {
            fullStaminaTimer += Time.deltaTime;

            if (fullStaminaTimer >= timeBeforeFade)
            {
                staminaCanvasGroup.alpha = Mathf.Lerp(
                    staminaCanvasGroup.alpha,
                    0f,
                    fadeSpeed * Time.deltaTime
                );
            }
        }
        else
        {
            fullStaminaTimer = 0f;

            staminaCanvasGroup.alpha = Mathf.Lerp(
                staminaCanvasGroup.alpha,
                1f,
                fadeSpeed * Time.deltaTime
            );
        }
    }

    public void ChangeRegenRateTemporarily(float newRate, float duration)
    {
        StartCoroutine(ChangeRegenRateCoroutine(newRate, duration));
    }

    private IEnumerator ChangeRegenRateCoroutine(float newRate, float duration)
    {
        float originalRate = staminaRegenRate;

        staminaRegenRate = newRate;

        yield return new WaitForSeconds(duration);

        staminaRegenRate = originalRate;
    }
}
