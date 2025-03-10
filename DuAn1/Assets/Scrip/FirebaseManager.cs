using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

public class FirebaseManager : MonoBehaviour
{

    public static FirebaseManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private FirebaseFirestore firestore;


    private void Start()
    {
        // Check and fix Firebase dependencies
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            FirebaseApp app = FirebaseApp.DefaultInstance;

            // Initialize Firestore
            firestore = FirebaseFirestore.DefaultInstance;
            Debug.Log("Firestore initialized successfully.");
        });
    }

    public void SendScore(string playerName, int score, string userId)
    {
        // Reference to the "scores" collection
        CollectionReference scoresRef = firestore.Collection("quizquestUser");

        // Query for documents where the playerID matches
        scoresRef.WhereEqualTo("PlayerID", userId).GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Failed to query data: " + task.Exception);
                return;
            }

            QuerySnapshot snapshot = task.Result;

            if (snapshot != null && snapshot.Count > 0)
            {
                // Player exists, iterate over the snapshot and update the first matching document
                foreach (var document in snapshot.Documents)
                {
                    DocumentReference existingDocRef = document.Reference;
                    existingDocRef.UpdateAsync("PlayerScore", score).ContinueWithOnMainThread(updateTask =>
                    {
                        if (updateTask.IsFaulted)
                        {
                            Debug.LogError("Failed to update score: " + updateTask.Exception);
                        }
                        else
                        {
                            Debug.Log("Score updated successfully.");
                        }
                    });
                    break; // We update only the first matching document and exit the loop
                }
            }
            else
            {
                // Player doesn't exist, create a new document
                DocumentReference newDocRef = scoresRef.Document();
                QuizQuestUser newQuizQuestUser = new QuizQuestUser(playerName, score, userId);
                newDocRef.SetAsync(newQuizQuestUser).ContinueWithOnMainThread(createTask =>
                {
                    if (createTask.IsFaulted)
                    {
                        Debug.LogError("Failed to send data: " + createTask.Exception);
                    }
                    else
                    {
                        Debug.Log("New score data sent successfully.");
                    }
                });
            }
        });
    }

    public void UpdatePlayerName(string playerName, string userId)
    {
        // Reference to the "scores" collection
        CollectionReference scoresRef = firestore.Collection("quizquestUser");

        // Query for documents where the playerID matches
        scoresRef.WhereEqualTo("PlayerID", userId).GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Failed to query data: " + task.Exception);
                return;
            }

            QuerySnapshot snapshot = task.Result;
            if (snapshot != null && snapshot.Count > 0)
            {
                // Player exists, iterate over the snapshot and update the first matching document
                foreach (var document in snapshot.Documents)
                {
                    DocumentReference existingDocRef = document.Reference;
                    existingDocRef.UpdateAsync("PlayerName", playerName).ContinueWithOnMainThread(updateTask =>
                    {
                        if (updateTask.IsFaulted)
                        {
                            Debug.LogError("Failed to update score: " + updateTask.Exception);
                        }
                        else
                        {
                            Debug.Log("Score updated successfully.");
                        }
                    });
                    break; // We update only the first matching document and exit the loop
                }
            }
        });
    }

    public async Task<List<QuizQuestUser>> ReadScores()
    {
        List<QuizQuestUser> usersList = new List<QuizQuestUser>();

        try {
            QuerySnapshot snapshot = await firestore.Collection("quizquestUser").GetSnapshotAsync();

            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                if (document.Exists)
                {
                    QuizQuestUser quizquestUser = document.ConvertTo<QuizQuestUser>();
                    //Add each user we find in the database
                    usersList.Add(quizquestUser);
                }
            }
        } catch (System.Exception e) {
            Debug.LogError("Failed to fetch data: " + e);
        }

        return usersList;
    }

    public async Task<bool> IsHighScoreBeaten(string userID, int newScore)
    {
        // Get the player's current score
        int currentScore = await GetScoreByPlayerID(userID);

        // Check if the new score is higher than the current score
        return newScore > currentScore;
    }

    public async Task<int> GetScoreByPlayerID(string playerId)
    {
        // Reference to the "scores" collection
        CollectionReference scoresRef = firestore.Collection("quizquestUser");

        // Query the collection for documents where the PlayerName matches
        QuerySnapshot snapshot = await scoresRef.WhereEqualTo("PlayerID", playerId).GetSnapshotAsync();

        if (snapshot != null && snapshot.Count > 0)
        {
        
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                if (document.Exists)
                {
                    QuizQuestUser quizquestUser = document.ConvertTo<QuizQuestUser>();
                    return quizquestUser.PlayerScore;
                }
            }
        }

        // If no score was found or player doesn't exist, return 0 or another default value
        Debug.LogWarning($"No score found for player: {playerId}");
        return 0;
    }


  
}