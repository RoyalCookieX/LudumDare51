using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class TriggerHurtbox : MonoBehaviour, IHurtbox
{
    public int HealthID { get => _healthID; set => _healthID = value; }
    public int Damage => _damage;

    [Header("Events")]
    [SerializeField] private UnityEvent _onHit;

    [Header("Properties")]
    [SerializeField] private int _healthID = 0;
    [SerializeField, Min(0)] private int _damage = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out Health health) || !health.Damage(_healthID, _damage))
            return;
        _onHit?.Invoke();
    }
}
