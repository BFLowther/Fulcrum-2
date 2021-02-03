using System.Collections;
using System.Collections.Generic;
using BasicTools.ButtonInspector;
using UnityEngine;

public class Location : MonoBehaviour
{
    public enum LocationMode
    {
        None,
        AskQuestionOnEnter,
        RevealInformationOnEnter,
        Purchase
    }

    public string LocationName;
    public QuestionSet questionSet;
    public PurchaseItemSet purchaseItemSet;

    [SerializeField]
    private LocationMode _locationMode = LocationMode.None;

    private bool _playerInLocation = false;

    // Use this for initialization
    void Start()
    {
        if (questionSet)
        {
            questionSet.Init();
        }
    }

    void AskQuestion()
    {
        Question question = questionSet.GetRandomQuestion();
        if (question != null)
        {
            GameManager.Instance.randomQuestionPanel.ShowQuestion(question);
        }
    }

    void ShowInformation()
    {
        Question question = questionSet.GetRandomQuestion();
        if (question != null)
        {
            GameManager.Instance.randomInformationPanel.ShowInformation(question);
        }
    }

    void ShowPurchaseMenu()
    {
        GameManager.Instance.purchasePanel.Show(purchaseItemSet);
    }

    void OnPlayerEntered()
    {
        switch (_locationMode)
        {
            case LocationMode.AskQuestionOnEnter:
                AskQuestion();
                break;
            case LocationMode.RevealInformationOnEnter:
                ShowInformation();
                break;
            case LocationMode.Purchase:
                ShowPurchaseMenu();
                break;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            _playerInLocation = true;

            //TODO: Maybe change this so that the question doesn't pop up instantly
            OnPlayerEntered();
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            _playerInLocation = false;
        }
    }

    #region Editor Helper Functions

    #endregion
}