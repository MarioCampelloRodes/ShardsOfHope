using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryHitbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<IParryable>()?.Parry();
        RankSystem.Instance.OnParry(); //añadir xp cada vez que haces parry
    }
}
