using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Answer
{
    public string answerText;
    public bool isCorrect;

    public Answer(string text, bool correct)
    {
        answerText = text;
        isCorrect = correct;
    }
}
