using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region || -- Singelton -- ||
    public static GameManager Instance {get; set;}
    
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
    
    public int lives = 5;
    public int currentScore = 0;

    private int scorePerQuestion = 1;
    private void Start()
    {
        StartNewGame();
    }

    public void StartNewGame()
    {
        SoundManager.Instance.StopBGMusic();
        SoundManager.Instance.PlayBGMusic(SoundManager.Instance.inGameMusic);
        lives = 5;
        currentScore = 0;

        UIManager.Instance.UpdateHeartDisplay(lives);
        UIManager.Instance.UpdateScoreUI(currentScore);


        QuestionManager.Instance.ResetQuestionsData();

        QuestionManager.Instance.LoadNextQuestion();
        
    }

    public void IncreaseScore()
    {
        currentScore += scorePerQuestion;
        UIManager.Instance.UpdateScoreUI(currentScore);
    }

    public void LoseLife()
    {
        lives--;

        UIManager.Instance.UpdateHeartDisplay(lives);

        if (lives <= 0)
        {
            EndGame();
        }
    }
    public void EndGame()
    {
        SceneManager.LoadScene("GameOver");
    }
}
