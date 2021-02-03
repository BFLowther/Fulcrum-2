using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public StatManager statManager;

    public Text moneyText;
    public Text timerText;
    public Image condomIcon;

    // Use this for initialization
    void Start()
    {
        statManager.UpdateStatMeters(false);
    }

    // Update is called once per frame
    void Update()
    {
        statManager.UpdateStatMeters();
        UpdateTimer();
        UpdateCondomsCount();
        UpdateMoney();
    }

    void UpdateTimer()
    {
        int secsLeft = (int) GameManager.Instance.TimeLeft;
        int minsLeft = secsLeft / 60;
        secsLeft %= 60;

        timerText.text = HelperUtilities.GetFormattedValue(minsLeft) + ":" +
                         HelperUtilities.GetFormattedValue(secsLeft);
    }

    void UpdateCondomsCount()
    {
        if (GameManager.Instance.HasCondom)
        {
            condomIcon.color = Color.white;
        }
        else
        {
            Color newColor = Color.grey;
            newColor.a = 0.5f;
            condomIcon.color = newColor;
        }
    }

    void UpdateMoney()
    {
        moneyText.text = "$" + GameManager.Instance.Money;
    }

    [Serializable]
    public class StatManager
    {
        public Image RiskMeterImage;
        public Image SatisfactionMeterImage;
        public Image EsteemMeterImage;

        public float SliderSpeed = 2f;

        public void UpdateStatMeters(bool animate = true)
        {
            UpdateStatMeter(SatisfactionMeterImage, GameManager.Instance.Satisfaction, animate);
            UpdateStatMeter(EsteemMeterImage, GameManager.Instance.Esteem, animate);
            UpdateStatMeter(RiskMeterImage, GameManager.Instance.Risk, animate);
        }

        private void UpdateStatMeter(Image statImage, float statValue, bool animate)
        {
            float lerpDeadzone = 0.005f;
            if (statImage)
            {
                float target = HelperUtilities.Remap(statValue, 0, GameManager.Instance.maxStatValue, 0, 1);

                float lerpSpeed = animate && (Mathf.Abs(target - statImage.fillAmount) > lerpDeadzone)
                    ? Time.deltaTime * SliderSpeed
                    : 1;

                statImage.fillAmount = Mathf.Lerp(statImage.fillAmount, target, lerpSpeed);
            }
        }
    }
}