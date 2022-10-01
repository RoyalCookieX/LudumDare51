using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private GameObject _prefab;
    private int _poolSize;
    private Queue<GameObject> _pool;

    public ObjectPool(GameObject prefab, int poolSize)
    {
        _prefab = prefab;
        _poolSize = poolSize;
        Reset();
    }

    public GameObject Instantiate(Vector3 position, Quaternion rotation)
    {
        GameObject instance = _pool.Dequeue();
        instance.SetActive(false);
        _pool.Enqueue(instance);
        instance.transform.SetPositionAndRotation(position, rotation);
        instance.SetActive(true);
        return instance;
    }

    public void SetPrefab(GameObject prefab)
    {
        _prefab = prefab;
    }

    public void Reset()
    {
        _pool?.Clear();
        _pool = new Queue<GameObject>(_poolSize);
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject instance = GameObject.Instantiate(_prefab, Vector3.zero, Quaternion.identity);
            instance.SetActive(false);
            _pool.Enqueue(instance);
        }
    }
}
