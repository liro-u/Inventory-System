using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

/// <summary>
/// Handles navigation between input fields using the Tab and Shift + Tab keys.
/// </summary>
public class NavigateWithTab : MonoBehaviour
{
    EventSystem system;

    /// <summary>The initial input field / button / Selectable to select when the script starts.</summary>
    public Selectable firstInput;

    /// <summary>
    /// Initializes the EventSystem and sets the initial input selection.
    /// </summary>
    void Start()
    {
        system = EventSystem.current;
        firstInput.Select();
    }

    /// <summary>
    /// Monitors keyboard input to navigate between selectable UI elements.
    /// - Shift + Tab selects the previous input field.
    /// - Tab selects the next input field.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            Selectable previous = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if (previous != null)
            {
                previous.Select();
            }
        }

        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null)
            {
                next.Select();
            }
        }
    }
}
