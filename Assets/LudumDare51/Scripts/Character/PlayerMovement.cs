using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 MoveDirection { get; private set; }

    [Header("Components")]
    [SerializeField] private Rigidbody2D _rigidbody;

    [Header("Properties")]
    [SerializeField, Min(1.0f)] private float _maxSpeed = 10.0f;

    public void SetMoveDirection(Vector2 direction)
    {
        MoveDirection = direction;
    }

    private void FixedUpdate()
    {
        if (MoveDirection == Vector2.zero)
            return;
        _rigidbody.AddForce(MoveDirection, ForceMode2D.Force);
        _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, _maxSpeed);
    }
}
