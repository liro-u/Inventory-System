using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIType
{
    Fullscreen,
    Popup
}

[System.Serializable]
public class UILinker
{
    public string UIName;
    public GameObject UIRef;
}

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<UILinker> listUILinker = new List<UILinker>();

    // Runtime dictionary for faster lookups
    private Dictionary<string, UILinker> uiDictionary = new Dictionary<string, UILinker>();

    private Stack<UILinker> history = new Stack<UILinker>();

    // Initialize the dictionary from the list
    private void Awake()
    {
        foreach (var linker in listUILinker)
        {
            if (!string.IsNullOrEmpty(linker.UIName) && linker.UIRef != null)
            {
                uiDictionary[linker.UIName] = linker;
            }
        }
    }

    // Go back to the previous UI in the history and return it
    public GameObject GoBackInHistory()
    {
        if (history.Count > 0)
        {
            history.Pop();

            if (history.Count > 0)
            {
                return history.Peek().UIRef; // Return the new top (previous UI)
            }
        }
        return null;
    }

    // Add a UI to history by name if it exists in the dictionary
    public void AddInHistoryByName(string name)
    {
        if (uiDictionary.TryGetValue(name, out UILinker uiLinker))
        {
            history.Push(uiLinker);
        }
    }

    // Clear the UI history
    public void CleanHistory()
    {
        history.Clear();
    }

    // Get the current UI in the history
    public UILinker GetCurrentUIInHistory()
    {
        return history.Count > 0 ? history.Peek() : null;
    }

    // Get a UI GameObject by its name using the dictionary
    public GameObject GetGameObjectByName(string name)
    {
        return uiDictionary.TryGetValue(name, out UILinker uiLinker) ? uiLinker.UIRef : null;
    }
}
