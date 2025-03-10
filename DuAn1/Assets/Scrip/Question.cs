using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
    public string questionText;
    public Answer[] answers;
    public bool isAnswered;

    public bool IsCorrect(Answer answer)
    {
        return answer.isCorrect;
    }
}
