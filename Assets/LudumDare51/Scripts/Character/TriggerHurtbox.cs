using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class TriggerHurtbox : MonoBehaviour, ITeamReference
{
    public UnityEvent OnHealthDamaged { get => _onHealthDamaged; set => _onHealthDamaged = value; }
    public UnityEvent OnHealthKilled { get => _onHealthKilled; set => _onHealthKilled = value; }

    public TeamAsset Team => _team; 
    public int Damage => _damage;

    [Header("Events")]
    [SerializeField] private UnityEvent _onHealthDamaged;
    [SerializeField] private UnityEvent _onHealthKilled;
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
        if (other.TryGetComponent(out Health health) && health.Damage(_team.ID, _damage, out bool killed))
        {
            _onHealthDamaged?.Invoke();
            if (killed)
                _onHealthKilled?.Invoke();
        }
    }
}
