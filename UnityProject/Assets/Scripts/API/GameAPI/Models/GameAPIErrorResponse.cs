using System.Collections.Generic;

[System.Serializable]
public class GameAPIErrors
{
    public string global;
}

[System.Serializable]
public class GameAPIErrorResponse
{
    public GameAPIErrors errors;
}
