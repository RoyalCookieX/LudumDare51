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
    }

    public void ResetLauncher()
    {
        _launcher.ResetLauncher();
    }

    private void OnMovement(InputValue value)
    {
        if (!_inputEnabled)
            return;

        Vector2 input = value.Get<Vector2>();
        _movement.SetMoveDirection(input);
    }

    private void OnAction(InputValue value)
    {
        if (!_inputEnabled || !_actionEnabled)
            return;

        _action = value.isPressed;
    }

    private void OnAim(InputValue value)
    {
        if (!_inputEnabled)
            return;

        Vector2 cursorPosition = value.Get<Vector2>();
        Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(cursorPosition);
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

        if(_action)
            _launcher.Launch();
    }
}
