using System.Collections.Generic;
using UnityEngine;

public class ConnectionDataSO : ScriptableObject
{
    [SerializeField] private UserData currentUserData;

    // Public getter with private setter for connectionData
    public UserData CurrentUserData
    {
        get => currentUserData;
        set
        {
            currentUserData = value;
            OnUserConnection?.Invoke();
        }
    }

    // Delegate and event for user connection
    public delegate void UserConnectionHandler();
    public event UserConnectionHandler OnUserConnection = delegate { };
}