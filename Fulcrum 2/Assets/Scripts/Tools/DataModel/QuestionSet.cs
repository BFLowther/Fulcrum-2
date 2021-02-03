using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "New Question Set", menuName = "Question Set", order = 1)]
public class QuestionSet : ScriptableObject
{
    public List<Question> Questions = new List<Question>();

    private int _nextQuestionIndex;
    private bool _needsShuffling = true;

    public void Init()
    {
        Reset();
    }

    private void Reset()
    {
        _nextQuestionIndex = 0;
        _needsShuffling = true;
    }

    /// <summary>
    /// Performs Fisher-Yates Shuffle with Circular Check
    /// </summary>
    private void ShuffleQuestions()
    {
        if (Questions.Count <= 1)
        {
            return;
        }

        Question lastQuestion = Questions[Questions.Count - 1];

        System.Random rng = new System.Random();
        int n = Questions.Count;
        while (n > 1)
        {
            n--;

            int k = rng.Next(n + 1);
            Question value = Questions[k];
            Questions[k] = Questions[n];
            Questions[n] = value;
        }

        if (lastQuestion == Questions[0])
        {
            int k = rng.Next(1, Questions.Count);
            Question value = Questions[k];
            Questions[k] = Questions[0];
            Questions[0] = value;
        }

        _needsShuffling = false;
    }

    /// <summary>
    /// Returns a Random Question from the question set. Guarantees to return all Questions before repeating.
    /// </summary>
    /// <returns>A New Random Question</returns>
    public Question GetRandomQuestion()
    {
        if (Questions.Count == 0)
        {
            return null;
        }
        
        if (_needsShuffling)
        {
            ShuffleQuestions();
            _nextQuestionIndex = 0;
        }

        Question question = Questions[_nextQuestionIndex];
        _nextQuestionIndex++;

        if (_nextQuestionIndex >= Questions.Count)
        {
            _needsShuffling = true;
        }

        return question;
    }
}