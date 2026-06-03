using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryHitbox : MonoBehaviour
{
    [SerializeField] private PlayerParry playerParry;
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<IParryable>()?.Parry();
        RankSystem.Instance.OnParry(); //añadir xp cada vez que haces parry

        if (other.CompareTag("Returnable"))
        {
            playerParry.ShootLaser();
        }
    }
}
