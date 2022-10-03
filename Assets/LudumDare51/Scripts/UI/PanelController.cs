using UnityEngine;
using UnityEngine.InputSystem;

public class PanelController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PanelNavigator _navigator;

    [Header("Properties")]
    [SerializeField] private int _defaultPanelIndex = 0;
    [SerializeField] private int _defaultPanelIndexOnCanceled = 1;

    public void OnCancel(InputAction.CallbackContext context)
    {
        bool pressed = context.ReadValue<float>() > 0.0f;
        if (!pressed)
            return;

        if(_navigator.ActivePanelCount == 1)
        {
            _navigator.PushPanelIndex(_defaultPanelIndexOnCanceled);
        }
        else
        {
            _navigator.PopPanelIndex();
        }
    }

    private void Start()
    {
        _navigator.StartAtPanelIndex(_defaultPanelIndex);
    }
}
