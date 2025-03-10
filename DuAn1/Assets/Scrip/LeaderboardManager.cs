using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderboardManager : MonoBehaviour
{
    public List<RankSlot> rankSlots;

    private void Start()
    {
        StartCoroutine(FetchUsers());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Back button pressed on mobile
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    IEnumerator FetchUsers()
    {
        Task<List<QuizQuestUser>> userData = FirebaseManager.Instance.ReadScores();

        //Make sure that userData was fetched
        yield return new WaitUntil(() => userData.IsCompleted);

        PopulateLeaderboards(userData.Result);
    }

    private void PopulateLeaderboards(List<QuizQuestUser> result)
    {
        //Get Top 10 Users
        List<QuizQuestUser> top10Users = result.OrderByDescending(user => user.PlayerScore).Take(10).ToList();

        //Populate UI with user data
        int userIndex = 0;

        foreach (RankSlot slot in rankSlots)
        {
            if (userIndex < top10Users.Count)
            {
                slot.SetUserData(top10Users[userIndex]);
                userIndex++;
            } else {
                break;
            }
        }
    }
}
