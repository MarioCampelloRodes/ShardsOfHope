using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankSystem : MonoBehaviour
{
    public static RankSystem Instance;

    [Header("Rangos")]
    [SerializeField] private GameObject rankD;
    [SerializeField] private GameObject rankC;
    [SerializeField] private GameObject rankB;
    [SerializeField] private GameObject rankA;
    [SerializeField] private GameObject rankS;

    [Header("XP")]
    [SerializeField] private float xpToNextRank = 2200f; //XP necesaria para subir de rango
    [SerializeField] private float xpRankMultiplier = 1.7f; //multiplicador de XP (hace que los niveles más altos requieran más puntos)
    [SerializeField] private float xpPerSecond = .3f; //XP ganada por tiempo
    [SerializeField] private float xpPerParry = 20f; //XP ganada por parry 
    [SerializeField] private float currentXP = 0f;
    [SerializeField] private float xpLostOnHurt = 800f;
    [SerializeField] private float hurtMultiplier = 2f; //cuanto más alto el rango, más puntos te quitan al hacerte daño

    // XP necesaria para el rango actual
    private float currentXpToNextRank;
    // XP que pierdes al recibir daño en el rango actual
    private float currentXpLostOnHurt;

    [Header("UI")]
    [SerializeField] private Image xpBarFill;                

    //IDs de cada rango
    //0=D, 1=C, 2=B, 3=A, 4=S
    [SerializeField] private int currentRank = 0;            
    [SerializeField] private float pointsMultiplier = 1f;
    [SerializeField] private float multiplierIncrease = 1.5f;
    public float GetMultiplier() => pointsMultiplier;


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
        if (currentRank >= 4) return;

        // ganar XP por tiempo
        AddXP(xpPerSecond * Time.deltaTime);
    }

    // llamar desde PlayerParry cuando se hace un parry
    public void OnParry()
    {
        AddXP(xpPerParry);
    }

    void AddXP(float amount)
    {
        if (currentRank >= 4) return;

        currentXP += amount;
        UpdateXPBar();

        if (currentXP >= xpToNextRank)
        {
            currentXP = 0f;
            SetRank(currentRank + 1);
        }
    }
    public void OnHurt()
    {
        currentXP -= xpLostOnHurt;
        currentXP = Mathf.Max(currentXP, 0f); //que no baje de 0
        UpdateXPBar();
    }

    void UpdateXPBar()
    {
        if (xpBarFill != null)
            xpBarFill.fillAmount = currentXP / xpToNextRank;
    }


    void SetRank(int rank)
    {
        currentRank = rank;

        rankD.SetActive(false);
        rankC.SetActive(false);
        rankB.SetActive(false);
        rankA.SetActive(false);
        rankS.SetActive(false);

        // escalar la XP necesaria y el daño recibido con el rango
        currentXpToNextRank = xpToNextRank * Mathf.Pow(xpRankMultiplier, currentRank);
        currentXpLostOnHurt = xpLostOnHurt * Mathf.Pow(hurtMultiplier, currentRank);

        switch (currentRank)
        {
            case 0: rankD.SetActive(true); pointsMultiplier = 1f; break;
            case 1: rankC.SetActive(true); pointsMultiplier *= multiplierIncrease; break;
            case 2: rankB.SetActive(true); pointsMultiplier *= multiplierIncrease; break;
            case 3: rankA.SetActive(true); pointsMultiplier *= multiplierIncrease; break;
            case 4: rankS.SetActive(true); pointsMultiplier *= multiplierIncrease; break;
        }

        PersistentInfo.singleton.UpdateRank(currentRank);
        UpdateXPBar();
    }
}
