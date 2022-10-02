using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class TriggerHurtbox : MonoBehaviour, IHurtbox
{
    public int HealthID => _asset ? _asset.ID : 0;
    public int Damage => _damage;

    [Header("Events")]
    [SerializeField] private UnityEvent _onHit;

    [Header("Properties")]
    [SerializeField, Min(0)] private int _damage = 10;

    private HealthAsset _asset;

    public void SetAsset(HealthAsset asset)
    {
        _asset = asset;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out Health health) || !health.Damage(_asset.ID, _damage))
            return;
        _onHit?.Invoke();
    }
}
