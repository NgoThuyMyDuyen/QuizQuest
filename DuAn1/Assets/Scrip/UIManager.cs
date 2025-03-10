using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using  Random = UnityEngine.Random;

public class UIManager : MonoBehaviour
{
    #region || -- Singelton -- ||
    public static UIManager Instance {get; set;}

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
    public TextMeshProUGUI timerText;
    
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;

    public Image[] heartImages;

    public TextMeshProUGUI scoreUI;

    private void Start()
    {
        UpdateScoreUI(0);
    }

    public void UpdateTimerUI(float timeLeft)
    {
        timerText.text = $"{Mathf.Ceil(timeLeft).ToString()}s";
    }

    public void UpdateScoreUI(int score)
    {
        scoreUI.text = $"Level {score}";
    }

    public void UpdateHeartDisplay(int lives)
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < lives)
            {
                heartImages[i].gameObject.SetActive(true);
            } else {
                heartImages[i].gameObject.SetActive(false);
            }
        }
    }

    // private void InitializeButtons()
    // {
    //     for (int i = 0; i < answerButtons.Length; i++)
    //     {
    //         int index = i;
    //         answerButtons[i].onClick.AddListener(() => OnAnswerSelected(index));
            
    //     }
    // }

    private void OnAnswerSelected(Answer answer)
    {
        QuestionManager.Instance.SubmitAnswer(answer);
    }
    public void DisplayQuestion(Question question)
    {
        UnselectAllButtons();
        questionText.text = question.questionText;

        Answer[] shuffledAnswers = question.answers.OrderBy(answer => Random.value).ToArray();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < shuffledAnswers.Length)
            {
                answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = shuffledAnswers[i].answerText;

                answerButtons[i].onClick.RemoveAllListeners();

                int capturedIndex = i;
                answerButtons[capturedIndex].onClick.AddListener(() => OnAnswerSelected(shuffledAnswers[capturedIndex]));

                answerButtons[i].gameObject.SetActive(true);
            } else {
                answerButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void UnselectAllButtons()
    {
        EventSystem eventSystem = EventSystem.current;
        if(eventSystem != null)
        {
            eventSystem.SetSelectedGameObject(null);
        }
    }
}
