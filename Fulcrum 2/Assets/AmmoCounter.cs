using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour
{
    private Slider mySlider;
    private bool reloading = false;
    private Text myText;

    private void Awake()
    {
        myText = gameObject.GetComponentInChildren<Text>();
        mySlider = gameObject.GetComponent<Slider>();
    }
    private void Update()
    {
        reloading = PlayerController.SharedInstance.IsReloading();
        
        if (!reloading)
        {
            mySlider.maxValue = PlayerController.SharedInstance.magazineSize;
            mySlider.value = PlayerController.SharedInstance.GetMagazineCount();
        }
        if (reloading)   
        {
            mySlider.maxValue = PlayerController.SharedInstance.reloadSpeed;
            mySlider.value = PlayerController.SharedInstance.reloadSpeed - PlayerController.SharedInstance.GetReloadTime();
        }

        if (mySlider.maxValue != 0)
            myText.text = (int) (mySlider.value / mySlider.maxValue * PlayerController.SharedInstance.magazineSize) + " - " + PlayerController.SharedInstance.magazineSize;
    }
    
}
