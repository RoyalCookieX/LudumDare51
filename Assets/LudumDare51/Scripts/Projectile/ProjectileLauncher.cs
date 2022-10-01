using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileLauncher : MonoBehaviour
{
    private float Percentage => _asset ? (_currentCooldown / _asset.Cooldown) : 0.0f;

    [Header("Events")]
    [SerializeField] private UnityEvent<int, int> _onLaunched;
    [SerializeField] private UnityEvent<int, int> _onReloaded;
    [SerializeField] private UnityEvent<float> _onCooldownChanged;

    [Header("Prefab")]
    [SerializeField] private GameObject _projectilePrefab;

    [Header("Components")]
    [SerializeField] private LauncherAsset _asset;
    [SerializeField] private Transform _target;
    [SerializeField] private ObjectPool _pool;

    [Header("Properties")]
    [SerializeField] private int _healthID = 0;

    private int _currentAmmo = 0;
    private float _currentCooldown = 0.0f;
    private Coroutine _launchRoutine = null;
    private Coroutine _cooldownRoutine = null;

    public void Reload()
    {
        _currentAmmo = _asset.Ammo;
        _onReloaded?.Invoke(_currentAmmo, _asset.Ammo);
    }

    public bool Launch()
    {
        if (_currentCooldown > 0.0f || _currentAmmo < 1)
            return false;

        if (_cooldownRoutine != null)
            StopCoroutine(_cooldownRoutine);
        _cooldownRoutine = StartCoroutine(CooldownRoutine());

        if (_launchRoutine != null)
            StopCoroutine(_launchRoutine);
        _launchRoutine = StartCoroutine(LaunchRoutine());
        _currentAmmo--;
        _onLaunched?.Invoke(_currentAmmo, _asset.Ammo);
        return true;
    }

    private void SetCooldown(float cooldown)
    {
        _currentCooldown = cooldown;
        _onCooldownChanged?.Invoke(Percentage);
    }

    private IEnumerator LaunchRoutine()
    {
        for (int i = 0; i < _asset.ShotMultiplier; i++)
        {
            float targetRotation = _target.rotation.eulerAngles.z;
            float variance = 90.0f * (1.0f - _asset.Accuracy);
            float angleMin = targetRotation - variance;
            float angleMax = targetRotation + variance;
            float targetAngle = Random.Range(angleMin, angleMax);
            Quaternion newRotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);
            GameObject instance = _pool.Instantiate(_target.position, newRotation);
            
            if (!instance.TryGetComponent(out IHurtbox hurtbox))
                hurtbox.HealthID = _healthID;

            yield return new WaitForSeconds(_asset.ShotDelay);
        }
    }

    private IEnumerator CooldownRoutine()
    {
        SetCooldown(_asset.Cooldown);
        while (_currentCooldown > 0.0f)
        {
            yield return null;
            SetCooldown(_currentCooldown - Time.deltaTime);
        }
    }

    private void Start()
    {
        int poolSize = _asset.Ammo * _asset.ShotMultiplier;
        _pool = new ObjectPool(_projectilePrefab, poolSize);
        Reload();
        SetCooldown(0.0f);
    }
}
