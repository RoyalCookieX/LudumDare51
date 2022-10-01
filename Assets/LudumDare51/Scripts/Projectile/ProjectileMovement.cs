using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D _rigidbody;

    [Header("Properties")]
    [SerializeField, Min(1.0f)] private float _maxSpeed = 1.0f;

    private void OnEnable()
    {
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddRelativeForce(Vector2.up * _maxSpeed, ForceMode2D.Impulse);
    }

    private void OnDisable()
    {
        _rigidbody.velocity = Vector2.zero;
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, _maxSpeed);
    }
}
