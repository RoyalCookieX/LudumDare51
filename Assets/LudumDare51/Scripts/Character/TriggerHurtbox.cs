using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class TriggerHurtbox : MonoBehaviour, IHurtbox
{
    public int TeamID => _asset ? _asset.ID : 0;
    public int Damage => _damage;

    [Header("Events")]
    [SerializeField] private UnityEvent _onHit;
    [SerializeField] private UnityEvent<TeamAsset> _onAssetChanged;

    [Header("Properties")]
    [SerializeField, Min(0)] private int _damage = 10;

    private TeamAsset _asset;

    public void SetAsset(TeamAsset asset)
    {
        _asset = asset;
        _onAssetChanged?.Invoke(_asset);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out Health health) || !health.Damage(_asset.ID, _damage))
            return;
        _onHit?.Invoke();
    }
}
