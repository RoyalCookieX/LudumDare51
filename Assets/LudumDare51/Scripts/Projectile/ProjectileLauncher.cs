using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileLauncher : MonoBehaviour
{
    private float Percentage => _asset ? (_currentCooldown / _asset.Cooldown) : 0.0f;

    [Header("Events")]
    [SerializeField] private UnityEvent<float> _onCooldownChanged;

    [Header("Prefab")]
    [SerializeField] private GameObject _projectilePrefab;

    [Header("Components")]
    [SerializeField] private LauncherAsset _asset;
    [SerializeField] private Transform _target;
    [SerializeField] private ObjectPool _pool;

    [Header("Properties")]
    [SerializeField, Min(5)] private int _poolSize = 10;
    [SerializeField] private int _healthID = 0;

    private float _currentCooldown = 0.0f;
    private Coroutine _cooldownRoutine = null;

    private void Start()
    {
        _pool = new ObjectPool(_projectilePrefab, _poolSize);
        _currentCooldown = 0.0f;
        _onCooldownChanged?.Invoke(Percentage);
    }

    public bool Launch()
    {
        if (_currentCooldown > 0.0f)
            return false;

        GameObject instance = _pool.Instantiate(_target.position, _target.rotation);
        if (_cooldownRoutine != null)
            StopCoroutine(_cooldownRoutine);
        _cooldownRoutine = StartCoroutine(CooldownRoutine());
        if (!instance.TryGetComponent(out IHurtbox hurtbox))
            return true;
        hurtbox.HealthID = _healthID;
        return true;
    }

    private IEnumerator CooldownRoutine()
    {
        _currentCooldown = _asset.Cooldown;
        _onCooldownChanged?.Invoke(Percentage);
        while (_currentCooldown > 0.0f)
        {
            yield return null;
            _currentCooldown -= Time.deltaTime;
            _onCooldownChanged?.Invoke(Percentage);
        }
    }
}
