using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI scoreUI;
    public Button newGameButton;
    public Button mainMenuButton;

    private void Start()
    {
        SoundManager.Instance.StopBGMusic();
        SoundManager.Instance.PlayGameOverSFX();

        newGameButton.onClick.AddListener(() => StartNewGame());
        mainMenuButton.onClick.AddListener(() => GoToMainMenu());

        int score = GameManager.Instance.currentScore;
        scoreUI.text = "Level: " + score.ToString();

        //Send data to the database

        string userName = UserManager.Instance.GetUserName();
        string userID = UserManager.Instance.GetUserID();


        CheckAndUpdateHighScore(userName, score, userID);
    }

    public async void CheckAndUpdateHighScore(string name, int newScore, string userID)
    {
        bool IsHighScoreBeaten = await FirebaseManager.Instance.IsHighScoreBeaten(userID, newScore);

        if(IsHighScoreBeaten)
        {
            Debug.Log("New high score !!!");

            FirebaseManager.Instance.SendScore(name, newScore, userID);
        } else {
            Debug.Log("Score is not a high score");
        }
    }

    private void StartNewGame()
    {
        SceneManager.LoadScene("Game");
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


}
