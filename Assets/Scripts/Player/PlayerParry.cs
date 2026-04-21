using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerParry : MonoBehaviour
{
    public GameObject parryHitbox;
    void Update()
    {
        if (Input.GetButton("Parry"))
        {
            StartCoroutine(AttackCrt());
        }
    }

    private IEnumerator AttackCrt()
    {
        parryHitbox.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        parryHitbox.SetActive(false);
    }
}
