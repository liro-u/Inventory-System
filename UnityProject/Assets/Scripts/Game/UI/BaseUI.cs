using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UIPopupLinker
{
    public string UIPopupName;
    public GameObject UIPopupRef;
}

public class BaseUI : MonoBehaviour
{
    [SerializeField] private GameObject fullscreenUI;
    [SerializeField] private List<UIPopupLinker> listUIPopupLinker = new List<UIPopupLinker>();

    private Dictionary<string, UIPopupLinker> uiPopupDictionary = new Dictionary<string, UIPopupLinker>();
    public Stack<UIPopupLinker> popupHistory
    {
        get; 
        private set;
    } = new Stack<UIPopupLinker>();

    // Initialize the dictionary from the list
    private void Awake()
    {
        foreach (var popupLinker in listUIPopupLinker)
        {
            if (!string.IsNullOrEmpty(popupLinker.UIPopupName) && popupLinker.UIPopupRef != null)
            {
                uiPopupDictionary[popupLinker.UIPopupName] = popupLinker;
            }
        }
    }

    public void OpenPopupByName(string name)
    {
        if (uiPopupDictionary.TryGetValue(name, out UIPopupLinker uiPopupLinker))
        {
            popupHistory.Push(uiPopupLinker);

            GameObject newPopup = uiPopupLinker.UIPopupRef;
            newPopup.gameObject.SetActive(true);
        }
    }

    public UIPopupLinker CloseTopPopup()
    {
        if (popupHistory.Count > 0)
        {
            GameObject lastUI = popupHistory.Pop().UIPopupRef;
            lastUI.gameObject.SetActive(false);

            if (popupHistory.Count > 0)
            {
                UIPopupLinker newUIPopupLinker = popupHistory.Peek();
                return newUIPopupLinker;
            }
        }
        return null;
    }

    public void CloseAllPopup()
    {
        while (popupHistory.Count > 0)
        {
            GameObject currentPopup = popupHistory.Pop().UIPopupRef;
            currentPopup.gameObject.SetActive(false);
        }
    }

    public UIPopupLinker GetTopPopup()
    {
        return popupHistory.Count > 0 ? popupHistory.Peek() : null;
    }

    public GameObject GetPopupGameObjectByName(string name)
    {
        return uiPopupDictionary.TryGetValue(name, out UIPopupLinker uiPopupLinker) ? uiPopupLinker.UIPopupRef : null;
    }

    public void SetCurrentUI(bool setCurrent)
    {
        CloseAllPopup();
        fullscreenUI.SetActive(setCurrent);
    }
}
