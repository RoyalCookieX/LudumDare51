using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject _projectilePrefab;

    [Header("Components")]
    [SerializeField] private Transform _target;
    [SerializeField] private ObjectPool _pool;

    [Header("Properties")]
    [SerializeField, Min(5)] private int _poolSize = 10;

    private void Start()
    {
        _pool = new ObjectPool(_projectilePrefab, _poolSize);
    }

    public void Launch()
    {
        _pool.Instantiate(_target.position, _target.rotation);
    }
}
