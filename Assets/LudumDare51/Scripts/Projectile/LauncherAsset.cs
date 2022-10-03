using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LudumDare51/Assets/Launcher")]
public class LauncherAsset : ScriptableObject
{
    public GameObject ProjectilePrefab => _projectilePrefab;
    public int PoolSize => _poolSize;
    public int ShotMultiplier => _shotMultiplier;
    public float ShotDelay => _shotDelay;
    public float Cooldown => _cooldown;
    public float Accuracy => _accuracy;
    public Sprite HeldSprite => _heldSprite;
    public Sprite ItemSprite => _itemSprite;
    public Sprite IconSprite => _iconSprite;
    public AudioClip EquipClip => _equipClip;

    [Header("Prefab")]
    [SerializeField] private GameObject _projectilePrefab;

    [Header("Properties")]
    [SerializeField, Min(2)] private int _poolSize = 10;
    [SerializeField, Min(1)] private int _shotMultiplier = 1;
    [SerializeField, Min(0.0f)] private float _shotDelay = 0.1f;
    [SerializeField, Range(0.0f, 1.0f)] private float _accuracy = 1.0f;
    [SerializeField, Min(0.001f)] private float _cooldown = 0.1f;

    [Header("Display")]
    [SerializeField] private Sprite _heldSprite;
    [SerializeField] private Sprite _itemSprite;
    [SerializeField] private Sprite _iconSprite;

    [Header("Audio")]
    [SerializeField] private List<AudioClip> _launchClips;
    [SerializeField] private AudioClip _equipClip;

    public AudioClip GetRandomLaunchClip() => _launchClips[Random.Range(0, _launchClips.Count)];

    private void OnValidate()
    {
        _cooldown = Mathf.Max(_cooldown, _shotMultiplier * _shotDelay);
    }
}
