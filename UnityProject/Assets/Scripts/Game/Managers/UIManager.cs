using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UILinker
{
    public string UIName;
    public BaseUI UIRef;
}

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<UILinker> listUILinker = new List<UILinker>();

    private Dictionary<string, UILinker> uiDictionary = new Dictionary<string, UILinker>();
    private Stack<UILinker> history = new Stack<UILinker>();

    // Singleton pattern to easily access GameDataManager
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("UIManager");
                    _instance = go.AddComponent<UIManager>();
                }
            }
            return _instance;
        }
    }

    // Initialize the dictionary from the list
    private void Awake()
    {
        foreach (var linker in listUILinker)
        {
            if (string.IsNullOrEmpty(linker.UIName))
            {
                Debug.LogError("Cant add to dicctionary because UIName is null or empty");
            }
            else if (linker.UIRef == null)
            {
                Debug.LogError("Cant add " + linker.UIName + " to dicctionary because UIRef is null");
            }
            else
            {
                linker.UIRef.SetCurrentUI(false);
                uiDictionary[linker.UIName] = linker;
            }
        }
    }

    // Go back to the previous UI in the history and return it
    public BaseUI GoBackInHistory()
    {
        if (history.Count > 0)
        {
            BaseUI lastUI = history.Pop().UIRef;
            lastUI.SetCurrentUI(false);

            if (history.Count > 0)
            {
                BaseUI newUI = history.Peek().UIRef;
                newUI.SetCurrentUI(true);
                return newUI;
            }
        }
        return null;
    }

    // Add a UI to history by name if it exists in the dictionary
    public void OpenAndAddInHistoryByName(string name)
    {
        if (uiDictionary.TryGetValue(name, out UILinker uiLinker))
        {
            if (history.Count > 0)
            {
                BaseUI lastUI = history.Peek().UIRef;
                lastUI.SetCurrentUI(false);
            }

            history.Push(uiLinker);

            BaseUI newUI = uiLinker.UIRef;
            newUI.SetCurrentUI(true);
        }
        else
        {
            Debug.LogError("You try to open " + name + " but it's not a key set in the dictionnary of UI (Check your UIManager setup).");
            if (uiDictionary.Count == 0)
            {
                Debug.LogError("The dictionary is empty");
            }
            else
            {
                Debug.LogError("Here is a list of all the available key : " + string.Join(", ", uiDictionary.Keys));
            }
        }
    }

    // Clear the UI history
    public void CleanHistory()
    {
        BaseUI currentUI = history.Peek().UIRef;
        currentUI.SetCurrentUI(false);

        history.Clear();
    }

    // Get the current UI in the history
    public UILinker GetCurrentUIInHistory()
    {
        return history.Count > 0 ? history.Peek() : null;
    }

    // Get a UI BaseUI by its name using the dictionary
    public BaseUI GetBaseUIByName(string name)
    {
        return uiDictionary.TryGetValue(name, out UILinker uiLinker) ? uiLinker.UIRef : null;
    }

    public BaseUI GoBack()
    {
        if (history.Count > 0)
        {
            BaseUI currentUI = GetCurrentUIInHistory().UIRef;
            if (currentUI.popupHistory.Count > 0)
            {
                currentUI.CloseTopPopup();
                return currentUI;
            }
            else
            {
                return GoBackInHistory();
            }
        }
        return null;
    }
}
