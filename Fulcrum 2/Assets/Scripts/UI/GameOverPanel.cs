﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowGameOver()
    {
        GameManager.Instance.PauseTime();
        gameObject.SetActive(true);
    }
}
