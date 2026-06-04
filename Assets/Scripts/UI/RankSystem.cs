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
    [SerializeField] private float xpToNextRank = 2200f;
    [SerializeField] private float xpRankMultiplier = 1.7f;
    [SerializeField] private float xpPerSecond = .3f;
    [SerializeField] private float xpPerParry = 20f;
    [SerializeField] private float currentXP = 0f;
    [SerializeField] private float xpLostOnHurt = 500f;
    [SerializeField] private float hurtMultiplier = 2f;

    private float currentXpToNextRank;
    private float currentXpLostOnHurt;

    [Header("UI")]
    [SerializeField] private Image xpBarFill;

    // 0=D, 1=C, 2=B, 3=A, 4=S
    [SerializeField] private int currentRank = 0;
    [SerializeField] private float pointsMultiplier = 1f;
    [SerializeField] private float multiplierIncrease = 1.5f;

    // flag: el medidor de S se llenó al menos una vez
    private bool sRankFull = false;

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
        if (currentRank == 4 && sRankFull) return;

        AddXP(xpPerSecond * Time.deltaTime);
    }

    public void OnParry()
    {
        if (currentRank == 4 && sRankFull) return;
        AddXP(xpPerParry);
    }

    void AddXP(float amount)
    {
        currentXP += amount;

        if (currentRank == 4)
        {
            if (currentXP >= currentXpToNextRank)
            {
                currentXP = currentXpToNextRank;
                sRankFull = true;
            }
            UpdateXPBar();
            return;
        }

        UpdateXPBar();

        if (currentXP >= currentXpToNextRank)
        {
            currentXP = 0f;
            SetRank(currentRank + 1);
        }
    }

    public void OnHurt()
    {
        if (currentRank == 4 && sRankFull)
        {
            sRankFull = false;
            currentXP = currentXpToNextRank; 
        }

        currentXP -= currentXpLostOnHurt;

        if (currentXP < 0f && currentRank > 0)
        {
            float overflow = currentXP;
            SetRank(currentRank - 1);
            currentXP = currentXpToNextRank + overflow;
            currentXP = Mathf.Max(currentXP, 0f);
        }
        else
        {
            currentXP = Mathf.Max(currentXP, 0f);
        }

        UpdateXPBar();
    }

    void UpdateXPBar()
    {
        if (xpBarFill != null)
            xpBarFill.fillAmount = currentXP / currentXpToNextRank;
    }

    void SetRank(int rank)
    {
        currentRank = rank;
        sRankFull = false; 

        rankD.SetActive(false);
        rankC.SetActive(false);
        rankB.SetActive(false);
        rankA.SetActive(false);
        rankS.SetActive(false);

        currentXpToNextRank = xpToNextRank * Mathf.Pow(xpRankMultiplier, currentRank);
        currentXpLostOnHurt = xpLostOnHurt * Mathf.Pow(hurtMultiplier, currentRank);

        switch (currentRank)
        {
            case 0: rankD.SetActive(true); pointsMultiplier = 1f; break;
            case 1: rankC.SetActive(true); pointsMultiplier = Mathf.Pow(multiplierIncrease, 1); break;
            case 2: rankB.SetActive(true); pointsMultiplier = Mathf.Pow(multiplierIncrease, 2); break;
            case 3: rankA.SetActive(true); pointsMultiplier = Mathf.Pow(multiplierIncrease, 3); break;
            case 4: rankS.SetActive(true); pointsMultiplier = Mathf.Pow(multiplierIncrease, 4); break;
        }

        PersistentInfo.singleton.UpdateRank(currentRank);
        UpdateXPBar();
    }

    //TESTEOS DEL SISTEMA DE RANGOS

    //public void AddXPDebug(float amount) => AddXP(amount);

    //public void ResetRank()
    //{
    //    currentXP = 0f;
    //    sRankFull = false;
    //    SetRank(0);
    //}
}
