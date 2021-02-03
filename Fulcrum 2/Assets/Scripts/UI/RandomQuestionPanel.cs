using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomQuestionPanel : MonoBehaviour
{
    public Text QuestionText;
    public RandomQuestionOptionButton[] OptionButtons;

    private Question _currentQuestion = null;
    
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ShowQuestion(Question question)
    {
        _currentQuestion = question;

        QuestionText.text = _currentQuestion.question;

        for (int i = 0; i < OptionButtons.Length; i++)
        {
            if (OptionButtons[i].optionIndex < _currentQuestion.options.Count)
            {
                OptionButtons[i].UpdateOption(_currentQuestion.options[OptionButtons[i].optionIndex]);
            }
        }

        GameManager.Instance.PauseTime();
        gameObject.SetActive(true);
    }

    public void OnOptionSelected(RandomQuestionOptionButton optionButton)
    {
        Question.Option selectedOption = _currentQuestion.options[optionButton.optionIndex];
        GameManager.Instance.ModifyStats(selectedOption.effects.esteem, selectedOption.effects.satisfaction,
            selectedOption.effects.risk);

        GameManager.Instance.ResumeTime();
        gameObject.SetActive(false);
    }
}