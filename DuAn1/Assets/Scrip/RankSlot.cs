using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankSlot : MonoBehaviour
{
    private TextMeshProUGUI userNameTXT;
    private TextMeshProUGUI scoreTXT;

    private void Start()
    {
        userNameTXT = transform.Find("Name").GetComponent<TextMeshProUGUI>();
        scoreTXT = transform.Find("Score").GetComponent<TextMeshProUGUI>();
    }

    public void SetUserData(QuizQuestUser userData)
    {
        userNameTXT.text = userData.PlayerName;
        scoreTXT.text = $"Lv. {userData.PlayerScore}";
    }

}
