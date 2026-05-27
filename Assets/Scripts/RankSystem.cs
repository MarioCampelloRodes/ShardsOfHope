using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankSystem : MonoBehaviour
{
    public static RankSystem Instance;

    public GameObject rankD;
    public GameObject rankC;
    public GameObject rankB;
    public GameObject rankA;
    public GameObject rankS;

    // 0=D, 1=C, 2=B, 3=A, 4=S
    private int currentRank = 0; 
    private float timer = 0f;
    private const float rankUpTime = 15f;

    // multiplicador actual de puntos
    public float pointsMultiplier = 1f;
    private const float multiplierIncrease = 1.015f;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        SetRank(0);
    }

    void Update()
    {
        //si ya esta en S, no sube 
        if (currentRank >= 4) return;

        timer += Time.deltaTime;
        if (timer >= rankUpTime)
        {
            timer = 0f;
            SetRank(currentRank + 1);
        }
    }

    void SetRank(int rank)
    {
        currentRank = rank;

        // desactivar todos los rangos
        rankD.SetActive(false);
        rankC.SetActive(false);
        rankB.SetActive(false);
        rankA.SetActive(false);
        rankS.SetActive(false);

        // activar el rango actual y actualizar el multiplicador
        switch (currentRank)
        {
            case 0:
                rankD.SetActive(true);
                pointsMultiplier = 1f;
                break;
            case 1:
                rankC.SetActive(true);
                pointsMultiplier *= multiplierIncrease;
                break;
            case 2:
                rankB.SetActive(true);
                pointsMultiplier *= multiplierIncrease;
                break;
            case 3:
                rankA.SetActive(true);
                pointsMultiplier *= multiplierIncrease;
                break;
            case 4:
                rankS.SetActive(true);
                pointsMultiplier *= multiplierIncrease;
                break;
        }
    }
}
