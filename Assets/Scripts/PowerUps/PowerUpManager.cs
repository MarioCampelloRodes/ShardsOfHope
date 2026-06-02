using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerParry playerParry;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Image staminaFill;

    public void StaminaRegen(float tempRegenRate, Color tempStaminaColor, float duration)
    {
        StartCoroutine(StaminaBoostCO(tempRegenRate, tempStaminaColor, duration));
    }
    private IEnumerator StaminaBoostCO(float regenRate, Color staminaColor, float duration)
    {
        float originalRate = playerParry.staminaRegenRate;
        Color originalStaminaColor = staminaFill.color;

        playerParry.staminaRegenRate = regenRate;
        staminaFill.color = staminaColor;

        yield return new WaitForSeconds(duration);

        playerParry.staminaRegenRate = originalRate;
        staminaFill.color = originalStaminaColor;
    }

    public void SpeedBoost(float tempSpeedMultiplier, float tempZRecoveryAmount, float tempZRecoveryTime, float duration)
    {
        StartCoroutine(SpeedBoostCO(tempSpeedMultiplier, tempZRecoveryAmount, tempZRecoveryTime, duration));
    }
    private IEnumerator SpeedBoostCO(float speedMultiplier, float zRegenAmount, float zRegenTime, float duration)
    {
        float originalSpeed = playerController.moveSpeed;
        float originalZRegenAmount = playerController.passiveZRecoveryAmount;
        float originalZRegenTime = playerController.passiveZRecoveryTime;

        playerController.moveSpeed *= speedMultiplier;
        playerController.passiveZRecoveryAmount *= zRegenAmount;
        playerController.passiveZRecoveryTime *= zRegenTime;


        yield return new WaitForSeconds(duration);

        playerController.moveSpeed *= originalSpeed;
        playerController.passiveZRecoveryAmount *= originalZRegenAmount;
        playerController.passiveZRecoveryTime *= originalZRegenTime;
    }

    public void ShootLasers(float duration)
    {
        StartCoroutine(LaserParryCO(duration));
    }
    private IEnumerator LaserParryCO(float duration)
    {
        playerParry.shootLaser = true;

        yield return new WaitForSeconds(duration);

        playerParry.shootLaser = false;
    }
}
