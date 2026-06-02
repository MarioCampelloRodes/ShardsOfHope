using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/SpeedBoostPU")]
public class SpeedPowerUp : PowerUp
{
    public float speedMultiplier = 2;
    public float passiveZRecoveryAmount = 30f;
    public float passiveZRecoveryTime = 0.75f;

    public override void Use()
    {
        GameObject.FindWithTag("PowerUpManager").GetComponent<PowerUpManager>().SpeedBoost(speedMultiplier, passiveZRecoveryAmount, passiveZRecoveryTime, duration);
    }
}
