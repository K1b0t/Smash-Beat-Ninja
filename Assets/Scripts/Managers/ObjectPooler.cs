using System.Collections.Generic;
using UnityEngine;

public static class ObjectPooler
{
    public static Dictionary<string, Component> poolLockup = new Dictionary<string, Component>();
    public static Dictionary<string, Queue<Component>> poolDictionary = new Dictionary<string, Queue<Component>>();

    public static void EnqueueObject <T> (T item, string name) where T : Component
    {
        if (!item.gameObject.activeSelf) { return; }

        item.transform.position = Vector3.zero;
        poolDictionary[name].Enqueue(item);
        item.gameObject.SetActive(false);

    }

    public static T DequeueObject <T> (string key) where T : Component
    {
        if (!poolDictionary.ContainsKey(key))
        {
            return null;
        }

        if (poolDictionary[key].TryDequeue(out var item)) 
        {
            return item as T;
        }

        T prefab = poolLockup[key] as T;
        T newInstance = Object.Instantiate(prefab);
        newInstance.gameObject.SetActive(false);

        return newInstance;
    } 

    public static T EnqueueNewInstance<T>(T item, string key) where T : Component
    {
        T newInstance = Object.Instantiate(item);
        newInstance.gameObject.SetActive(false);
        poolDictionary[key].Enqueue(newInstance);
        return newInstance;
    }

    public static void SetupPool <T>(T pooledItemPrefab, int poolSize, string dictionaryEntry) where T : Component
    {
        poolDictionary.Add(dictionaryEntry, new Queue<Component>());
        poolLockup.Add(dictionaryEntry, pooledItemPrefab);

        for (int i = 0; i < poolSize; i++)
        {
            T pooledInstance = Object.Instantiate(pooledItemPrefab);
            pooledInstance.gameObject.SetActive(false);
            poolDictionary[dictionaryEntry].Enqueue((T)pooledInstance);
        }

    }
}
