using UnityEngine;
using UnityEngine.Audio;

public class AudioSpawner : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject _audioPrefab;

    [Header("Components")]
    [SerializeField] private Transform _target;

    [Header("Properties")]
    [SerializeField, Min(2)] private int _poolSize = 10;
    [SerializeField] private AudioMixerGroup _mixerGroup;

    private ObjectPool _pool;

    public void Spawn(AudioClip clip)
    {
        GameObject instance = _pool.Instantiate(_target.position, Quaternion.identity);
        
        if (!instance.TryGetComponent(out AudioObject audio))
            return;

        audio.Play(clip, _mixerGroup);
    }

    private void Awake()
    {
        _pool = new ObjectPool(_audioPrefab, _poolSize);
    }
}
