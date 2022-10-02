using UnityEngine;
using UnityEngine.Events;

public class ProjectileImpact : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent _onEnabled;
    [SerializeField] private UnityEvent _onDisabled;
    [SerializeField] private UnityEvent<GameObject> _onImpact;

    private void OnEnable()
    {
        _onEnabled?.Invoke();
    }

    private void OnDisable()
    {
        _onDisabled?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _onImpact?.Invoke(other.gameObject);
    }
}
