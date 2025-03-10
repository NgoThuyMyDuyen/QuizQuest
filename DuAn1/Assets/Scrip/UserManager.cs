using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    #region || -- Singelton -- ||
    public static UserManager Instance {get; set;}

    private const string DEVICE_USER_NAME = "DeviceUserName";
    private const string DEVICE_USER_ID = "DeviceUserID";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else {
            Destroy(Instance);
        }
        
        DontDestroyOnLoad(gameObject);
    }
    #endregion
    
    public bool DoesUserExits()
    {
        return PlayerPrefs.HasKey(DEVICE_USER_ID) && PlayerPrefs.HasKey(DEVICE_USER_NAME);
    }

    public void SetUserName(string userName)
    {
        PlayerPrefs.SetString(DEVICE_USER_NAME, userName);
    }

    public void SetUserID(string id)
    {
        PlayerPrefs.SetString(DEVICE_USER_ID, id);
    }

    public string GetUserName()
    {
        return PlayerPrefs.GetString(DEVICE_USER_NAME);
    }

    public string GetUserID()
    {
        return PlayerPrefs.GetString(DEVICE_USER_ID);
    }

    public string GenerateUniqueId()
    {
        return System.Guid.NewGuid().ToString();
    }
}
