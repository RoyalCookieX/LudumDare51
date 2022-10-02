using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class TriggerHurtbox : MonoBehaviour, ITeamReference
{
    public TeamAsset Team => _team; 
    public int Damage => _damage;

    [Header("Events")]
    [SerializeField] private UnityEvent _onHit;
    [SerializeField] private UnityEvent<TeamAsset> _onAssetChanged;

    [Header("Properties")]
    [SerializeField, Min(0)] private int _damage = 10;

    private TeamAsset _team;

    public void SetTeam(TeamAsset team)
    {
        _team = team;
        _onAssetChanged?.Invoke(_team);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out Health health) || !health.Damage(_team.ID, _damage))
            return;
        _onHit?.Invoke();
    }
}
