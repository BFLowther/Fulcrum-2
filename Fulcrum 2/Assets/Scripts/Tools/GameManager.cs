using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance; }
    }

    public RandomQuestionPanel randomQuestionPanel;
    public RandomInformationPanel randomInformationPanel;
    public PurchasePanel purchasePanel;
    public GameOverPanel gameOverPanel;

    public float TotalTime = 300f;
    public float CondomRiskReductionFactor = 0.5f;

    public float maxStatValue = 100f;
    
    [SerializeField] private float esteem = 0;
    [SerializeField] private float satisfaction = 0;
    [SerializeField] private float risk = 0;

    public float Esteem
    {
        get { return esteem; }
    }

    public float Satisfaction
    {
        get { return satisfaction; }
    }

    public float Risk
    {
        get { return risk; }
    }

    [SerializeField]
    private float timeLeft = 0;

    public float TimeLeft
    {
        get { return timeLeft; }
    }

    [SerializeField] private float money = 25f;
    [SerializeField] private bool hasCondom = false;

    public float Money
    {
        get { return money; }
    }

    public bool HasCondom
    {
        get { return hasCondom; }
    }


    void Awake()
    {
        if (_instance != null)
        {
            DestroyImmediate(this);
            return;
        }

        _instance = this;
    }

    void Start()
    {
        //timeFactor = 1;
        Time.timeScale = 1;
        ResetTimer();
    }

    //makes sure esteem, risk, and satisfaction do not go beyond 100 or below 0
    void Update()
    {
        ClampStats();
        UpdateTimer();
    }

    void ClampStats()
    {
        if (esteem >= maxStatValue)
        {
            esteem = maxStatValue;
        }

        if (risk >= maxStatValue)
        {
            risk = maxStatValue;
        }

        if (satisfaction >= maxStatValue)
        {
            satisfaction = maxStatValue;
        }

        if (esteem <= 0)
        {
            esteem = 0;
        }

        if (risk <= 0)
        {
            risk = 0;
        }

        if (satisfaction <= 0)
        {
            satisfaction = 0;
        }
    }

    void ResetTimer()
    {
        timeLeft = TotalTime;
    }

    void UpdateTimer()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            timeLeft = 0;
            OnTimerEnd();
        }
    }

    public void OnMoneySpent(float moneySpent)
    {
        money -= moneySpent;
        if (money < 0)
        {
            money = 0;
        }
    }

    public void OnCondomPurchased()
    {
        hasCondom = true;
    }

    public void OnCondomUsed()
    {
        hasCondom = false;
    }

    public void ModifyStats(float esteemDiff, float satisfactionDiff, float riskDiff)
    {
        esteem += esteemDiff;
        satisfaction += satisfactionDiff;

        if (riskDiff > 0)
        {
            if (hasCondom)
            {
                riskDiff *= CondomRiskReductionFactor;
                OnCondomUsed();
            }
        }
        risk += riskDiff;
    }

    public void PauseTime()
    {
        Time.timeScale = 0;
    }

    public void ResumeTime()
    {
        Time.timeScale = 1;
    }

    void OnTimerEnd()
    {
        gameOverPanel.ShowGameOver();
    }
}