using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/StaminaRegenPU")]

public class StaminaRegenPowerUp : PowerUp
{
    public float regenRate = 50f;
    public Color staminaColor = Color.green;

    public override void Use()
    {
        GameObject.FindWithTag("PowerUpManager").GetComponent<PowerUpManager>().StaminaRegen();
    }
}
