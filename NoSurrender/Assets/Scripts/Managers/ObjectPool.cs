using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private Queue<GameObject> pooledObjects;
    [SerializeField] private GameObject spawnObject;
    [SerializeField] private int poolSize;




    private void Awake()
    {
        pooledObjects = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(spawnObject);
            obj.SetActive(false);

            pooledObjects.Enqueue(obj);
        }
    }

    public GameObject GetPooledObjects()
    {
        GameObject obj = pooledObjects.Dequeue();

        obj.SetActive(true);

        pooledObjects.Enqueue(obj);

        return obj;
    }
}
