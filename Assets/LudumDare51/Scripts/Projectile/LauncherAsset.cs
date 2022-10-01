using UnityEngine;

[CreateAssetMenu(menuName = "LudumDare51/Assets/Launcher")]
public class LauncherAsset : ScriptableObject
{
    public GameObject ProjectilePrefab => _projectilePrefab;
    public int Ammo => _ammo;
    public int ShotMultiplier => _shotMultiplier;
    public float ShotDelay => _shotDelay;
    public float Cooldown => _cooldown;
    public float Accuracy => _accuracy;

    [Header("Prefab")]
    [SerializeField] private GameObject _projectilePrefab;

    [Header("Properties")]
    [SerializeField, Min(1)] private int _ammo = 7;
    [SerializeField, Min(1)] private int _shotMultiplier = 1;
    [SerializeField, Min(0.0f)] private float _shotDelay = 0.1f;
    [SerializeField, Range(0.0f, 1.0f)] private float _accuracy = 1.0f;
    [SerializeField, Min(0.001f)] private float _cooldown = 0.1f;

    private void OnValidate()
    {
        _cooldown = Mathf.Max(_cooldown, _shotMultiplier * _shotDelay);
    }
}
