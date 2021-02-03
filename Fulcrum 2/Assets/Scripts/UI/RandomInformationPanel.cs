using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomInformationPanel : MonoBehaviour {

    public Text QuestionText;
    public Text AnswerText;
    public Button RevealAnswerButton;
    private Question _currentQuestion = null;

    public void ShowInformation(Question question)
    {
        _currentQuestion = question;

        QuestionText.text = _currentQuestion.question;

        AnswerText.text = "";
        AnswerText.gameObject.SetActive(false);
        RevealAnswerButton.gameObject.SetActive(true);

        GameManager.Instance.PauseTime();
        gameObject.SetActive(true);
    }

    public void RevealAnswer()
    {
        RevealAnswerButton.gameObject.SetActive(false);
        AnswerText.text = "Answer: " + _currentQuestion.GetBestOption().optionName;
        AnswerText.gameObject.SetActive(true);
    }

    public void Close()
    {
        GameManager.Instance.ResumeTime();
        gameObject.SetActive(false);
    }
}
