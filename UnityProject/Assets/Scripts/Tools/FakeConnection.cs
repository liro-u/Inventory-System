using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeConnection : MonoBehaviour
{
    [SerializeField] private UserData userData = new UserData();
    [SerializeField] private bool doFakeConnection = false;

    private void OnValidate()
    {
        if (doFakeConnection)
        {
            if (Application.isPlaying)
            {
                DoFakeConnection();
            }
            doFakeConnection = false;
        }
    }

    public void DoFakeConnection()
    {
        Debug.Log("Connection faked");
        GameDataManager.Instance.ConnectionData.CurrentUserData = userData;
    }
}
