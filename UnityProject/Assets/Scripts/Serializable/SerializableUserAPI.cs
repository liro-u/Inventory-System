using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Role
{
    public string tag;
    public string name;
    public string description;
    public bool defaultForNewAccount;
    public bool isOwner;
    public bool isAdmin;
    public string _id;
}

[System.Serializable]
public class Preference
{
    public string language;
    public string _id;
}

[System.Serializable]
public class ErrorDetails
{
    public string global;
}

#region Login
[System.Serializable]
public class LoginRequest
{
    public string identifier;
    public string password;
}

[System.Serializable]
public class LoginResponse
{
    public string pseudo;
    public Role[] roles;
    public Preference preference;
    public string _id;
    public bool isAdmin;
    public bool isOwner;
    public string token;
}

[System.Serializable]
public class LoginError
{
    public ErrorDetails errors;
}
#endregion

#region Signup
[System.Serializable]
public class SignupRequest
{
    public string email;
    public string password;
}

[System.Serializable]
public class SignupResponse
{
    public string pseudo;
    public Role[] roles;
    public Preference preference;
    public string _id;
    public bool isAdmin;
    public bool isOwner;
    public string token;
}

[System.Serializable]
public class SignupError
{
    public ErrorDetails errors;
}
#endregion
