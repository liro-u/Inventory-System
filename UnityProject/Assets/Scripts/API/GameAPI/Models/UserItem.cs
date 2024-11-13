[System.Serializable]
public class UserItem
{
    public string _id;
    public string userId;
    public Item itemId;
    public int quantity;
}

public class AddUserItem
{
    public UserItem userItem;
    public int remainingQuantity;
}

public class UserItems
{
    public UserItem[] items;
}