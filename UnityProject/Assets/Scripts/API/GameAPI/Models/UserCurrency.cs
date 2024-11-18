[System.Serializable]
public class UserCurrency
{
    public string _id;
    public string userId;
    public Currency currencyId;
    public int quantity;
}

public class AddUserCurrency
{
    public UserCurrency userCurrency;
    public int remainingQuantity;
}


public class UserCurrencies
{
    public UserCurrency[] currencies;
}