using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenUI : MonoBehaviour
{
    [SerializeField] private string UIName = "";
    void Start()
    {
        UIManager.Instance.OpenAndAddInHistoryByName(UIName);
    }
}
