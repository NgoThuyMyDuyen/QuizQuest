using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Button  changeUserNameBTN;
    public Button submitNameBTN;

    public TextMeshProUGUI nameInputField;

    public GameObject changeNameScreen;

    bool isChangingName = false;

    public MasterVolumeController volumeController;

    private void Start()
    {
        changeUserNameBTN.onClick.AddListener(() => HandleNameChange());
        submitNameBTN.onClick.AddListener(() => HandleSubmit());
    }

    private void HandleNameChange()
    {
        isChangingName = true;
        changeNameScreen.SetActive(true);
    }

    private void HandleSubmit()
    {
        string newUser = nameInputField.text;
        if (!string.IsNullOrEmpty(newUser))
        {
            UserManager.Instance.SetUserName(newUser); // Update name inside playerpref (local)

            isChangingName = false;
            changeNameScreen.SetActive(false);

            FirebaseManager.Instance.UpdatePlayerName(newUser, UserManager.Instance.GetUserID());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isChangingName == false)
        {
            volumeController.SaveMasterVolume();

            SceneManager.LoadScene("MainMenu");
        }
    }
}
