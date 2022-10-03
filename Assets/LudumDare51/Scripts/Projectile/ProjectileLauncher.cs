using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileLauncher : MonoBehaviour, ITeamReference
{
    public UnityEvent OnHealthDamaged { get => _onHealthDamaged; set => _onHealthDamaged = value; }
    public UnityEvent OnHealthKilled { get => _onHealthKilled; set => _onHealthKilled = value; }
    public TeamAsset Team => _team;
    public bool Active => _active;
    private float Percentage => _launcher ? (_currentCooldown / _launcher.Cooldown) : 0.0f;

    [Header("Events")]
    [SerializeField] private UnityEvent _onLaunched;
    [SerializeField] private UnityEvent _onHealthDamaged;
    [SerializeField] private UnityEvent _onHealthKilled;
    [SerializeField] private UnityEvent<bool> _onActiveChanged;
    [SerializeField] private UnityEvent<float> _onCooldownChanged;
    [SerializeField] private UnityEvent _onAssetReset;
    [SerializeField] private UnityEvent<LauncherAsset> _onAssetChanged;
    [SerializeField] private UnityEvent<AudioClip> _onAudioPlayed;


    [Header("Components")]
    [SerializeField] private Transform _target;
    [SerializeField] private SpriteRenderer _renderer;

    [Header("Properties")]
    [SerializeField] private bool _active = true;
    [SerializeField] private LauncherAsset _launcher;

    private bool _playEquip = false;
    private float _currentCooldown = 0.0f;
    private ObjectPool _pool;
    private TeamAsset _team;
    private LauncherAsset _defaultLauncher;
    private Coroutine _launchRoutine = null;
    private Coroutine _cooldownRoutine = null;

    public bool Launch()
    {
        if (!_active || !_launcher || _currentCooldown > 0.0f)
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

    public void SetActive(bool active)
    {
        _active = active;
        _onActiveChanged?.Invoke(_active);
    }

    public void SetTeam(TeamAsset team)
    {
        _team = team;
    }

    public void SetLauncher(LauncherAsset launcher)
    {
        _launcher = launcher ? launcher : _defaultLauncher;
        int poolSize = _launcher.PoolSize * _launcher.ShotMultiplier;
        _pool = new ObjectPool(_launcher.ProjectilePrefab, poolSize);
        SetCooldown(0.0f);
        _renderer.sprite = _launcher.HeldSprite;
        _onAssetChanged?.Invoke(_launcher);
        if(_playEquip)
            _onAudioPlayed?.Invoke(_launcher.EquipClip);
    }

    public void ResetLauncher()
    {
        if (_launchRoutine != null)
            StopCoroutine(_launchRoutine);

        _launcher = null;
        _pool = null;
        SetCooldown(0.0f);
        _renderer.sprite = null;
        _onActiveChanged?.Invoke(_launcher);
        _onAssetReset?.Invoke();
    }

    private void SetCooldown(float cooldown)
    {
        _currentCooldown = cooldown;
        _onCooldownChanged?.Invoke(Percentage);
    }

    private IEnumerator LaunchRoutine()
    {
        for (int i = 0; i < _launcher.ShotMultiplier; i++)
        {
            float targetRotation = _target.rotation.eulerAngles.z;
            float variance = 90.0f * (1.0f - _launcher.Accuracy);
            float angleMin = targetRotation - variance;
            float angleMax = targetRotation + variance;
            float targetAngle = Random.Range(angleMin, angleMax);
            Quaternion newRotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);
            GameObject instance = _pool.Instantiate(_target.position, newRotation);

            if (instance.TryGetComponent(out ITeamReference teamRef))
            {
                teamRef.SetTeam(_team);
                teamRef.OnHealthDamaged.AddListener(_onHealthDamaged.Invoke);
                teamRef.OnHealthKilled.AddListener(_onHealthKilled.Invoke);
            }

            _onAudioPlayed?.Invoke(_launcher.GetRandomLaunchClip());
            yield return new WaitForSeconds(_launcher.ShotDelay);
        }
    }

    private IEnumerator CooldownRoutine()
    {
        SetCooldown(_launcher.Cooldown);
        while (_currentCooldown > 0.0f)
        {
            yield return null;
            SetCooldown(_currentCooldown - Time.deltaTime);
        }
    }

    private void Awake()
    {
        _defaultLauncher = _launcher;
        SetLauncher(_defaultLauncher);
        _playEquip = true;
    }
}
