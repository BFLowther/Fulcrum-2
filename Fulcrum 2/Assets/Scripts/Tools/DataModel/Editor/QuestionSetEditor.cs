using System.Collections;
using System.Collections.Generic;
using BasicTools.ButtonInspector;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

[CustomEditor(typeof(QuestionSet))]
public class QuestionSetEditor : Editor
{
    private QuestionSet _questionSet;

    public override void OnInspectorGUI()
    {
//        DrawDefaultInspector();

        _questionSet = target as QuestionSet;

        DrawQuestions();

        EditorUtility.SetDirty(_questionSet);
    }
    
    void DrawQuestions()
    {
        List<int> questionsToRemove = new List<int>();
        for (int i = 0; i < _questionSet.Questions.Count; i++)
        {
            Question locationQuestion = _questionSet.Questions[i];

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Question " + (i + 1), EditorStyles.whiteLargeLabel);
            if (GUILayout.Button("Remove", EditorStyles.toolbarButton))
            {
                questionsToRemove.Add(i);
            }

            EditorGUILayout.EndHorizontal();

            locationQuestion.question = EditorGUILayout.TextArea(locationQuestion.question);


            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Options", EditorStyles.boldLabel);
            if (GUILayout.Button("Add", EditorStyles.miniButtonRight))
            {
                locationQuestion.options.Add(new Question.Option());
            }

            EditorGUILayout.EndHorizontal();

            List<int> optionsToRemove = new List<int>();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical();
            for (int j = 0; j < locationQuestion.options.Count; j++)
            {
                Question.Option questionOption = locationQuestion.options[j];

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Option: ", EditorStyles.boldLabel);
                questionOption.optionName = EditorGUILayout.TextField(questionOption.optionName);
                if (GUILayout.Button("X", EditorStyles.miniButtonRight))
                {
                    optionsToRemove.Add(j);
                }

                EditorGUILayout.EndHorizontal();

                //                    EditorGUILayout.LabelField("Effects", EditorStyles.boldLabel);

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.Space();
                EditorGUILayout.BeginVertical();

                EditorGUILayout.BeginHorizontal();
                GUI.enabled = false;
                EditorGUILayout.IntField("Index", j);
                GUI.enabled = true;
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                questionOption.effects.esteem = EditorGUILayout.IntField("Esteem", questionOption.effects.esteem);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                questionOption.effects.satisfaction = EditorGUILayout.IntField("Satisfaction", questionOption.effects.satisfaction);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                questionOption.effects.risk = EditorGUILayout.IntField("Risk", questionOption.effects.risk);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space();
                EditorGUILayout.Space();
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            foreach (int op in optionsToRemove)
            {
                locationQuestion.options.RemoveAt(op);
            }

            optionsToRemove.Clear();

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUILayout.Separator();
        }

        foreach (int q in questionsToRemove)
        {
            _questionSet.Questions.RemoveAt(q);
        }

        questionsToRemove.Clear();

        if (GUILayout.Button("Add New Question"))
        {
            _questionSet.Questions.Add(new Question());
        }
    }
}