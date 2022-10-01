using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D _rigidbody;

    [Header("Properties")]
    [SerializeField, Min(1.0f)] private float _speed = 1.0f;
    [SerializeField, Min(1.0f)] private float _maxSpeed = 1.0f;

    private void Start()
    {
        _rigidbody.AddRelativeForce(transform.up * _maxSpeed, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        _rigidbody.AddRelativeForce(transform.up * _speed, ForceMode2D.Force);
        _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, _maxSpeed);
    }
}
