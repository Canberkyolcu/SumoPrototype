using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpawnMaganer : MonoBehaviour
{
    [SerializeField] private ObjectPool objectPool;

    [SerializeField] private float spawnTime = 1f;

    //[SerializeField] private Transform spawnPositonY;


    

    void Start()
    {
        StartCoroutine(nameof(SpawnRoutine));
    }

    
   private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            var obj = objectPool.GetPooledObjects();

            float randomPointX = Random.Range(-4.50f, 4.50f);
            float randomPointZ = Random.Range(-4.50f, 4.50f);


            obj.transform.position = new Vector3(randomPointX, (float)2, randomPointZ);

            obj.transform.DOMoveY(transform.position.y, 2);


            
            yield return  new WaitForSeconds(spawnTime);
        }
    }
}
