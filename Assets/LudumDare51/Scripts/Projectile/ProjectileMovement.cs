using System.Collections;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D _rigidbody;

    [Header("Properties")]
    [SerializeField, Min(1.0f)] private float _maxSpeed = 1.0f;
    [SerializeField, Min(0.1f)] private float _duration = 1.0f;

    private Coroutine _current = null;

    private IEnumerator DurationRoutine()
    {
        yield return new WaitForSeconds(_duration);
        gameObject.SetActive(false);
        _current = null;
    }

    private void OnEnable()
    {
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddRelativeForce(Vector2.up * _maxSpeed, ForceMode2D.Impulse);
        
        if (_current != null)
            StopCoroutine(_current);
        _current = StartCoroutine(DurationRoutine());
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
