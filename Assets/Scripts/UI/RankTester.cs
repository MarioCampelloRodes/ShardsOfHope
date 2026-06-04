using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankTester : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private float xpBurst = 500f;   // XP ganada al presionar F
    [SerializeField] private KeyCode keyAddXP = KeyCode.F;
    [SerializeField] private KeyCode keyHurt = KeyCode.H;
    [SerializeField] private KeyCode keyReset = KeyCode.R;
    [SerializeField] private KeyCode keyParry = KeyCode.P;

    void Update()
    {
        if (RankSystem.Instance == null) return;

        //if (Input.GetKeyDown(keyAddXP))
        //    RankSystem.Instance.AddXPDebug(xpBurst);

        if (Input.GetKeyDown(keyParry))
            RankSystem.Instance.OnParry();

        if (Input.GetKeyDown(keyHurt))
            RankSystem.Instance.OnHurt();

        //if (Input.GetKeyDown(keyReset))
        //    RankSystem.Instance.ResetRank();
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 300, 20), "[F] +XP burst   [P] Parry   [H] Daño   [R] Reset");
    }
}
