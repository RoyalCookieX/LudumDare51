using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool InputEnabled => _inputEnabled;
    public bool ActionEnabled => _actionEnabled;

    [Header("Components")]
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private ProjectileLauncher _launcher;
    [SerializeField] private Rotator _rotator;

    [Header("Properties")]
    [SerializeField] private bool _inputEnabled = true;
    [SerializeField] private bool _actionEnabled = true;

    private bool _action = false;
    private Camera _mainCamera;

    public void EnableInput(bool enable)
    {
        _inputEnabled = enable;
    }

    public void EnableAction(bool enable)
    {
        _actionEnabled = enable;
        _launcher.SetActive(_actionEnabled);
        if (!_actionEnabled)
            _action = false;
    }

    public void ResetLauncher()
    {
        _launcher.ResetLauncher();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (!_inputEnabled)
            return;

        Vector2 input = context.ReadValue<Vector2>();
        _movement.SetMoveDirection(input);
    }

    public void OnAction(InputAction.CallbackContext context)
    {
        if (!_inputEnabled)
            return;

        _action = _actionEnabled ? context.performed : false;
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        if (!_inputEnabled || !_mainCamera)
            return;

        Vector2 cursorPosition = context.ReadValue<Vector2>();
        Vector2 worldPosition = _mainCamera.ScreenToWorldPoint(cursorPosition);
        _rotator.SetFollowPosition(worldPosition);
    }

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!_inputEnabled)
        {
            _movement.SetMoveDirection(Vector2.zero);
            return;
        }

        if (_action)
            _launcher.Launch();
    }
}
