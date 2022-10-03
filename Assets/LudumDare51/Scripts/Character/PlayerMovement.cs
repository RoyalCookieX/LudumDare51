using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 MoveDirection { get; private set; }

    [Header("Components")]
    [SerializeField] private Rigidbody2D _rigidbody;

    [Header("Properties")]
    [SerializeField, Min(1.00f)] private float _maxSpeed = 10.0f;
    [SerializeField, Min(0.01f)] private float _maxVelocity = 10.0f;

    public void SetMoveDirection(Vector2 direction)
    {
        MoveDirection = direction;
    }

    private void FixedUpdate()
    {
        if (MoveDirection == Vector2.zero)
        {
            _rigidbody.velocity = Vector2.zero;
            return;
        }

        _rigidbody.AddForce(MoveDirection * _maxSpeed, ForceMode2D.Impulse);
        _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, _maxVelocity);
    }
}
