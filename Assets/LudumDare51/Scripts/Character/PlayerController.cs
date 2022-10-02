using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private ProjectileLauncher _launcher;
    [SerializeField] private Rotator _rotator;

    private bool _action = false;
    private Camera _mainCamera;

    private void OnEnable()
    {
        _mainCamera = Camera.main;
    }

    private void OnMovement(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        _movement.SetMoveDirection(input);
    }

    private void OnAction(InputValue value)
    {
        _action = value.isPressed;
    }

    private void OnAim(InputValue value)
    {
        Vector2 cursorPosition = value.Get<Vector2>();
        Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(cursorPosition);
        _rotator.SetFollowPosition(worldPosition);
    }

    private void Update()
    {
        if(_action)
            _launcher.Launch();
    }
}
