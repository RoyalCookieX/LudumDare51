using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private GameObject _prefab;
    private int _poolSize;
    private Queue<GameObject> _pool;

    const int MIN_POOL_SIZE = 2;

    public ObjectPool(GameObject prefab, int poolSize)
    {
        if(!prefab)
        {
            Debug.LogError("Invalid Prefab!");
            return;
        }
        if (poolSize < MIN_POOL_SIZE)
        {
            Debug.LogError("Invalid Prefab!");
            return;
        }
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

    public void Reset(GameObject newPrefab = null, int newPoolSize = 0)
    {
        _pool?.Clear();
        GameObject prefab = newPrefab ? newPrefab : _prefab;
        int poolSize = newPoolSize >= MIN_POOL_SIZE ? newPoolSize : _poolSize;
        _pool = new Queue<GameObject>(poolSize);
        for (int i = 0; i < poolSize; i++)
        {
            GameObject instance = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity);
            instance.SetActive(false);
            _pool.Enqueue(instance);
        }
    }
}
