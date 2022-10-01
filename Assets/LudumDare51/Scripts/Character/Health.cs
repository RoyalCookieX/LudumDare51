using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int Current => _current;
    public int Max => _max;
    public float Percentage => _current / (float)_max;
    public int ID => _id;

    [Header("Events")]
    [SerializeField] private UnityEvent<float> _onHealthChanged;
    [SerializeField] private UnityEvent _onKilled;

    [Header("Properties")]
    [SerializeField, Min(0)] private int _current = 100;
    [SerializeField, Min(0)] private int _max = 100;
    [SerializeField] private int _id = 0;

    public void Kill()
    {
        _current = 0;
        _onKilled?.Invoke();
    }

    public bool Damage(int id, int damage)
    {
        if (id == _id || damage <= 0)
            return false;

        _current -= damage;
        _onHealthChanged?.Invoke(Percentage);
        if (_current <= 0)
            Kill();
        return true;
    }

    private void Start()
    {
        _onHealthChanged.Invoke(Percentage);
    }
}
