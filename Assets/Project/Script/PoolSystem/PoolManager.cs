using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private Dictionary<string, ObjectPool> pools = new Dictionary<string, ObjectPool>();

    private static PoolManager _instance;
    public static PoolManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void CreatePool(string key, GameObject prefab, int initialSize)
    {
        if (prefab == null)
        {
            Debug.LogWarning("Given prefab for pool cant be null!");
            return;
        }

        if (!pools.ContainsKey(key))
        {
            GameObject poolObj = new GameObject(key + "Pool");
            poolObj.transform.parent = transform;

            ObjectPool pool = poolObj.AddComponent<ObjectPool>();
            pool.prefab = prefab;
            pool.initialSize = initialSize;
            pools[key] = pool;
        }
    }

    public GameObject GetObject(string key)
    {
        if (pools.ContainsKey(key))
        {
            return pools[key].GetPooledObject();
        }

        Debug.LogWarning("Pool with key " + key + " does not exist.");
        return null;
    }

    public void ReturnObject(string key, GameObject obj)
    {
        if (pools.ContainsKey(key))
        {
            pools[key].ReturnObjectToPool(obj);
        }
        else
        {
            Debug.LogWarning("Pool with key " + key + " does not exist.");
        }
    }
}
