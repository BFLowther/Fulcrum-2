using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomQuestionOptionButton : MonoBehaviour
{
    public int optionIndex;

    public Text _text;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateOption(Question.Option option)
    {
        _text.text = option.optionName;
    }
}
