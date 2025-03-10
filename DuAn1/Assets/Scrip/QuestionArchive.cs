using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestionDB", menuName = "MyDuyen/Question Arcive")]
public class QuestionArchive : ScriptableObject
{
    public Question[] questions;

    public Question GetRandomQuestion()
    {
        if (questions == null || questions.Length == 0)
        {
            Debug.LogError("Question Archive is empty!");
            return null;
        }

        int randomIndex = Random.Range(0, questions.Length);
        return questions[randomIndex];
    }
}
