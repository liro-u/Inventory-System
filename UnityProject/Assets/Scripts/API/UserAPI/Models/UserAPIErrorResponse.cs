using System.Collections.Generic;

[System.Serializable]
public class UserAPIErrors
{
    public string global;
}

[System.Serializable]
public class UserAPIErrorResponse
{
    public UserAPIErrors errors;
}
