using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PlayerMovement _movement;

    private void OnMovement(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        _movement.SetMoveDirection(input);
    }

    private void OnAction(InputValue value)
    {
        print($"action!");
    }

    private void OnAim(InputValue value)
    {
        Vector2 aim = value.Get<Vector2>();
        print($"aim: {aim}");
    }
}
