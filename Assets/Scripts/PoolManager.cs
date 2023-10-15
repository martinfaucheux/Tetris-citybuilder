using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public string poolCode;
    public GameObject prefab;
    public int prewarmCount = 10;
    private List<GameObject> _pooledObjects = new List<GameObject>();
    private static List<PoolManager> instances;

    private void Awake()
    {
        if (instances == null)
            instances = new List<PoolManager>();
        instances.Add(this);
    }

    private void Start()
    {
        PrewarmPool();
    }

    public void OnDestroy()
    {
        instances.Remove(this);
    }

    public static PoolManager GetPool(string poolCode)
    {
        if (instances == null)
            return null;

        foreach (PoolManager pool in instances)
        {
            if (pool.poolCode == poolCode)
                return pool;
        }
        Debug.LogWarning("Trying to get a pool that doesn't exist");
        return null;
    }

    private void PrewarmPool()
    {
        for (int i = 0; i < prewarmCount; i++)
        {
            GameObject newObj = CreateNewObject();
            newObj.SetActive(false);
        }
    }

    public GameObject GetPooledObject()
    {
        CleanPool();
        // Search for an inactive object in the pool
        foreach (GameObject obj in _pooledObjects)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        return CreateNewObject();
    }

    private GameObject CreateNewObject()
    {
        GameObject newObj = Instantiate(prefab);
        _pooledObjects.Add(newObj);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public void ReturnToPool(GameObject obj)
    {
        if (_pooledObjects.Contains(obj))
            obj.SetActive(false);
        else
            Debug.LogWarning("Trying to return an object to the pool that doesn't belong to it");
    }

    private void CleanPool()
    {
        // remove object that have been destroyed
        int destroyedCount = _pooledObjects.Where(obj => obj == null).Count();
        if (destroyedCount > 0)
            Debug.LogWarning($"{destroyedCount} destroyed objects from pool {poolCode}");

        _pooledObjects = _pooledObjects.Where(obj => obj != null).ToList();
    }
}