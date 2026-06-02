using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ShootLasersPU")]
public class ShootLasersPowerUp : PowerUp
{
    public override void Use()
    {
        GameObject.FindWithTag("PowerUpManager").GetComponent<PowerUpManager>().ShootLasers(duration);
    }
}
