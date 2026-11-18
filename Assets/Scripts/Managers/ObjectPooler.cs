using System.Collections.Generic;
using UnityEngine;

public static class ObjectPooler
{
    public static Dictionary<string, Component> poolLockup = new Dictionary<string, Component>();
    public static Dictionary<string, Queue<Component>> poolDictionary = new Dictionary<string, Queue<Component>>();

    public static void EnqueueObject<T>(T item, string name) where T : Component
    {
        if (item == null || !item.gameObject.activeSelf) { return; }

   
        if (!poolDictionary.ContainsKey(name))
        {
            item.gameObject.SetActive(false);
            return;
        }

        item.transform.position = Vector3.zero;
        poolDictionary[name].Enqueue(item);
        item.gameObject.SetActive(false);
    }

    public static T DequeueObject<T>(string key) where T : Component
    {
        if (!poolDictionary.ContainsKey(key))
        {

            Debug.LogError($"ObjectPooler: No existe el pool con la llave '{key}'");
            return null;

        }

        while (poolDictionary[key].Count > 0)
        {

            var item = poolDictionary[key].Dequeue();
            if (item != null)
            {

                return item as T;

            }

        }

        T prefab = poolLockup[key] as T;
        T newInstance = Object.Instantiate(prefab);
        newInstance.gameObject.SetActive(false);

        return newInstance;
    }

    public static void SetupPool<T>(T pooledItemPrefab, int poolSize, string dictionaryEntry) where T : Component
    {
        if (pooledItemPrefab == null)
        {

            Debug.LogError("ObjectPooler: El prefab asignado es NULL.");
            return;

        }

        poolDictionary[dictionaryEntry] = new Queue<Component>();
        poolLockup[dictionaryEntry] = pooledItemPrefab;

        for (int i = 0; i < poolSize; i++)
        {

            T pooledInstance = Object.Instantiate(pooledItemPrefab);
            pooledInstance.gameObject.SetActive(false);
            poolDictionary[dictionaryEntry].Enqueue(pooledInstance);

        }
    }
}
