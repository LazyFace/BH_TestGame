using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    #region Singleton

    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    [System.Serializable]
    public class Pool
    {
        public ObjectsToSpawn tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;

    public Dictionary<ObjectsToSpawn, Queue<GameObject>> poolDictionary;

    public enum ObjectsToSpawn
    {
        BULLET,
        ZOMBIE,
        SKELETON,
        GHOST
    }

    private void Start()
    {
        poolDictionary = new Dictionary<ObjectsToSpawn, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(ObjectsToSpawn objectToSpawnName, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(objectToSpawnName)) { Debug.LogWarning("Pool with tag " + objectToSpawnName + " not exist"); return null; }

        GameObject objectToSpawn = poolDictionary[objectToSpawnName].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[objectToSpawnName].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
