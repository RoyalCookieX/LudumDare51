using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PanelController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PanelNavigator _navigator;

    [Header("Properties")]
    [SerializeField] private int _defaultPanelIndex = 0;
    [SerializeField] private int _defaultPanelIndexOnCancelled = 1;

    public void OnCancel(InputAction.CallbackContext context)
    {
        bool pressed = context.ReadValue<float>() > 0.0f;
        if (!pressed)
            return;

        if(_navigator.ActivePanelCount == 1)
        {
            _navigator.PushPanelIndex(_defaultPanelIndexOnCancelled);
        }
        else
        {
            _navigator.PopPanelIndex();
        }
    }

    private void Start()
    {
        _navigator.PushPanelIndex(_defaultPanelIndex);
    }
}
