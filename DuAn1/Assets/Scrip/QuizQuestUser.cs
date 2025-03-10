using System.Collections;
using System.Collections.Generic;
using Firebase.Firestore;
using UnityEngine;

[FirestoreData]
public class QuizQuestUser
{
    [FirestoreProperty]
    public string PlayerName {get; set;}
    [FirestoreProperty]
    public int PlayerScore {get; set;}
    [FirestoreProperty]
    public string PlayerID {get; set;}

    public QuizQuestUser() {}

    public QuizQuestUser ( string playerName, int playerScore, string playerID)
    {
        PlayerName = playerName;
        PlayerScore = playerScore;
        PlayerID = playerID;
    }

    
}
