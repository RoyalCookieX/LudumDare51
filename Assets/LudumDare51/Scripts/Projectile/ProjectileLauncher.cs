using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileLauncher : MonoBehaviour
{
    public bool Active { get => _active; set => _active = value; }
    private float Percentage => _launcherAsset ? (_currentCooldown / _launcherAsset.Cooldown) : 0.0f;

    [Header("Events")]
    [SerializeField] private UnityEvent _onLaunched;
    [SerializeField] private UnityEvent<float> _onCooldownChanged;
    [SerializeField] private UnityEvent<LauncherAsset> _onAssetChanged;

    [Header("Components")]
    [SerializeField] private Transform _target;
    [SerializeField] private SpriteRenderer _renderer;

    [Header("Properties")]
    [SerializeField] private bool _active = true;
    [SerializeField] private TeamAsset _teamAsset;
    [SerializeField] private LauncherAsset _launcherAsset;

    private float _currentCooldown = 0.0f;
    private ObjectPool _pool;
    private LauncherAsset _defaultLauncher;
    private Coroutine _launchRoutine = null;
    private Coroutine _cooldownRoutine = null;

    public bool Launch()
    {
        if (!_active || _currentCooldown > 0.0f)
            return false;

        if (_cooldownRoutine != null)
            StopCoroutine(_cooldownRoutine);
        _cooldownRoutine = StartCoroutine(CooldownRoutine());

        if (_launchRoutine != null)
            StopCoroutine(_launchRoutine);
        _launchRoutine = StartCoroutine(LaunchRoutine());
        _onLaunched?.Invoke();
        return true;
    }

    public void SetAsset(LauncherAsset asset)
    {
        _launcherAsset = asset ? asset : _defaultLauncher;
        int poolSize = _launcherAsset.PoolSize * _launcherAsset.ShotMultiplier;
        _pool = new ObjectPool(_launcherAsset.ProjectilePrefab, poolSize);
        SetCooldown(0.0f);
        _renderer.sprite = _launcherAsset.HeldSprite;
        _onAssetChanged?.Invoke(_launcherAsset);
    }

    private void SetCooldown(float cooldown)
    {
        _currentCooldown = cooldown;
        _onCooldownChanged?.Invoke(Percentage);
    }

    private IEnumerator LaunchRoutine()
    {
        for (int i = 0; i < _launcherAsset.ShotMultiplier; i++)
        {
            float targetRotation = _target.rotation.eulerAngles.z;
            float variance = 90.0f * (1.0f - _launcherAsset.Accuracy);
            float angleMin = targetRotation - variance;
            float angleMax = targetRotation + variance;
            float targetAngle = Random.Range(angleMin, angleMax);
            Quaternion newRotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);
            GameObject instance = _pool.Instantiate(_target.position, newRotation);

            if (instance.TryGetComponent(out IHurtbox hurtbox))
                hurtbox.SetAsset(_teamAsset);

            yield return new WaitForSeconds(_launcherAsset.ShotDelay);
        }
    }

    private IEnumerator CooldownRoutine()
    {
        SetCooldown(_launcherAsset.Cooldown);
        while (_currentCooldown > 0.0f)
        {
            yield return null;
            SetCooldown(_currentCooldown - Time.deltaTime);
        }
    }

    private void Start()
    {
        _defaultLauncher = _launcherAsset;
        SetAsset(_defaultLauncher);
    }
}
