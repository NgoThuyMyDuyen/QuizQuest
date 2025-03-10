using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject welcomeScreen;
    public TextMeshProUGUI nameInputField;
    public Button submitButton;

    private void Start()
    {
        SoundManager.Instance.PlayBGMusic(SoundManager.Instance.menuMusic);
        submitButton.onClick.AddListener(OnSubmitButtonClick);

        if (UserManager.Instance.DoesUserExits())
        {
            welcomeScreen.SetActive(false);
        } else { //there is not user saved
            welcomeScreen.SetActive(true);
        }
    }

    private void OnSubmitButtonClick()
    {
        string userName = nameInputField.text;

        //check for valid user name
        //require specific lenght or special characters

        if (!string.IsNullOrEmpty(userName))
        {
            UserManager.Instance.SetUserName(userName);

            // Generate Unique ID
            string uid = UserManager.Instance.GenerateUniqueId();
            UserManager.Instance.SetUserID(uid);

            welcomeScreen.SetActive(false);
        }
    }
    public void OnPlayClicked()
    {
        SceneManager.LoadScene("Game");
    }
    public void OnLeaderboardClicked()
    {
        SceneManager.LoadScene("Leaderboards");
    }
    public void OnOptionClicked()
    {
        SceneManager.LoadScene("Options");
    }
}
