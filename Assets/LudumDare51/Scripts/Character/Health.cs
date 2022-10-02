using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int Current => _current;
    public int Max => _max;
    public float Percentage => _current / (float)_max;
    public bool Invincible { get => _invincible; set => _invincible = value; }

    [Header("Events")]
    [SerializeField] private UnityEvent<float> _onHealthChanged;
    [SerializeField] private UnityEvent _onRevived;
    [SerializeField] private UnityEvent _onKilled;

    [Header("Properties")]
    [SerializeField] private bool _invincible = false;
    [SerializeField, Min(0)] private int _current = 100;
    [SerializeField, Min(0)] private int _max = 100;
    [SerializeField] private TeamAsset _teamAsset;

    public void Heal(int heal)
    {
        if (_current == 0)
            _onRevived?.Invoke();
        _current = Mathf.Min(_current + heal, _max);
        _onHealthChanged?.Invoke(Percentage);
    }
    public void Kill()
    {
        _current = 0;
        _onKilled?.Invoke();
    }

    public bool Damage(int id, int damage)
    {
        if (id == _teamAsset.ID || damage <= 0 || _invincible)
            return false;

        _current -= damage;
        _onHealthChanged?.Invoke(Percentage);
        if (_current <= 0)
            Kill();
        return true;
    }

    private void OnEnable()
    {
        _onHealthChanged.Invoke(Percentage);
    }
}
