using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    #region || -- Singelton -- ||
    public static QuestionManager Instance {get; set;}
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else {
            Destroy(Instance);
        }
    }
    #endregion
    
    public QuestionArchive questionArchive;
    private Question currenQuestion;

    public float questionTimeLimit = 15f;
    private float timeRemaining;
    private bool isTimerunning = false;

    bool isPlayingTimerSound;
    private void Update()
    {
        if (isTimerunning)
        {
            timeRemaining -= Time.deltaTime;
            UIManager.Instance.UpdateTimerUI(timeRemaining);

            if (timeRemaining <= 3 && isPlayingTimerSound == false)
            {
                SoundManager.Instance.PlayTimerSFX();
                isPlayingTimerSound = true;
            }
            

            if(timeRemaining <= 0)
            {
                TimerExpired();
            }
        }
    }

    private void TimerExpired()
    {
        isTimerunning = false;

        currenQuestion.isAnswered = true;

        GameManager.Instance.LoseLife();
        LoadNextQuestion();
    }

    private void StartQuestionTimer()
    {
        timeRemaining = questionTimeLimit;
        isTimerunning = true;
        isPlayingTimerSound = false;
    }

    public void LoadNextQuestion()
    {
        if (AreMoreQuestionAvailable() == false)
        {
            GameManager.Instance.EndGame();
            return;
        }

        var previousQuestion = currenQuestion;

        currenQuestion = questionArchive.GetRandomQuestion();

        if (currenQuestion.isAnswered || previousQuestion == currenQuestion)
        {
            LoadNextQuestion();
            return;
        }

        if (currenQuestion != null)
        {
            UIManager.Instance.DisplayQuestion(currenQuestion);
        } else {
            Debug.LogError("No question loaded! The question archive might be empty");
        }

        StartQuestionTimer();
    }

    private bool AreMoreQuestionAvailable()
    {
        foreach (Question question in questionArchive.questions)
        {
            if (question.isAnswered == false)
            {
                return true;
            }
        }
        return false;
    }

    public void SubmitAnswer(Answer answer)
    {
        if (currenQuestion == null)
        {
            Debug.LogError("No current question to submit");
            return;
        }

        if (currenQuestion.IsCorrect(answer))
        {
            Debug.Log("Correct Answer!");
            SoundManager.Instance.PlayCorrectAnswerSFX();
            GameManager.Instance.IncreaseScore();
        } else {
            Debug.Log("Wrong answer!");
            SoundManager.Instance.PlayWrongAnswerSFX();
            GameManager.Instance.LoseLife();
        }

        currenQuestion.isAnswered = true;

        LoadNextQuestion();
    }

    internal void ResetQuestionsData()
    {
        foreach (Question q in questionArchive.questions)
        {
            q.isAnswered = false;
        }
    }
}
