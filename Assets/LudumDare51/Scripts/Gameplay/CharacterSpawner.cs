using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CharacterSpawner : MonoBehaviour
{
    public int WaveSize { get; private set; } = 1;
    public int MaxWaveSize => _maxWaveSize;

    [Header("Events")]
    [SerializeField] private UnityEvent<int> _onWaveChanged;
    [SerializeField] private UnityEvent<int> _onWaveSizeChanged;
    [SerializeField] private UnityEvent<GameObject> _onCharacterSpawned;

    [Header("Prefab")]
    [SerializeField] private GameObject _characterPrefab;

    [Header("Properties")]
    [SerializeField, Min(1)] private int _maxWaveSize = 25;
    [SerializeField, Min(1.00f)] private Vector2 _spawnWaveRange = Vector2.one;
    [SerializeField, Min(1.01f)] private Vector2 _minSpawnWaveMultiplierRange = new Vector2(1.25f, 2.25f);
    [SerializeField, Min(1.01f)] private Vector2 _maxSpawnWaveMultiplierRange = new Vector2(2.00f, 2.50f);
    [SerializeField, Min(0.01f)] private Vector2 _spawnDelayRange = new Vector2(0.5f, 1.5f);
    [SerializeField] private SpawnBounds _spawnBounds;

    private int _currentWave = 0;
    private ObjectPool _pool;
    private Coroutine _current;

    [ContextMenu("Spawn Wave")]
    public void SpawnWave()
    {
        if (_current != null)
            return;
        _current = StartCoroutine(WaveRoutine());
        _currentWave++;
        _onWaveChanged?.Invoke(_currentWave);
        _onWaveSizeChanged?.Invoke(WaveSize);
    }

    [ContextMenu("Increment Wave")]
    public void IncrementWave()
    {
        _spawnWaveRange.x *= Random.Range(_minSpawnWaveMultiplierRange.x, _minSpawnWaveMultiplierRange.y);
        _spawnWaveRange.y *= Random.Range(_maxSpawnWaveMultiplierRange.x, _maxSpawnWaveMultiplierRange.y);
        
        _spawnWaveRange.x = Mathf.Min(_spawnWaveRange.x, _maxWaveSize);
        _spawnWaveRange.y = Mathf.Min(_spawnWaveRange.y, _maxWaveSize);

        WaveSize = Mathf.RoundToInt(Random.Range(_spawnWaveRange.x, _spawnWaveRange.y));
    }

    private IEnumerator WaveRoutine()
    {        
        for(int i = 0; i < WaveSize; i++)
        {
            Vector2 targetPosition = _spawnBounds.Evaluate();
            GameObject instance = _pool.Instantiate(targetPosition, Quaternion.identity);
            _onCharacterSpawned?.Invoke(instance);

            float spawnDelay = Random.Range(_spawnDelayRange.x, _spawnDelayRange.y);
            yield return new WaitForSeconds(spawnDelay);
        }
        _current = null;
    }

    private void Awake()
    {
        _pool = new ObjectPool(_characterPrefab, _maxWaveSize);
    }
    
    private void OnDrawGizmosSelected()
    {
        _spawnBounds.DrawGizmos(Color.red);
    }
}
