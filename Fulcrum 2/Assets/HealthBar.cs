using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider myslider;

    private void Awake()
    {
        myslider = gameObject.GetComponent<Slider>();
    }
    private void Update()
    {
        if (myslider.maxValue == 0)
        {
            SetMaxHealth();
        }
        else
        myslider.value = PlayerController.SharedInstance.health;
    }

    private void SetMaxHealth()
    {
        myslider.maxValue = PlayerController.SharedInstance.health;
    }
}