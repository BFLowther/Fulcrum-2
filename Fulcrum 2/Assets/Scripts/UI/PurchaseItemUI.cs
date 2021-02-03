using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PurchaseItemUI : MonoBehaviour
{
    public Text ItemNameText;
    public Button PurchaseButton;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Disable()
    {
        PurchaseButton.interactable = false;
    }
}
