using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] public GameObject targetBoxPrefab;
    [SerializeField] private float maxBoundZ = 60;
    [SerializeField] private float minBoundZ = 15;
    [SerializeField] public float boundX = 20;
    [SerializeField] private float spawnPosY = 2;
    private float boxCount;
   
    void Start()
    {
        SpawnTargetBox();
    }


    void LateUpdate()
    {
       // if the box is destroyed with gun, spawn a new target box
        if(GameObject.FindWithTag("Box") == null)
        {
            SpawnTargetBox();
        }
    }

    // spawn target box in a random location

    void SpawnTargetBox()
    {
     
        float spawnPosX = Random.Range(-boundX, boundX);
        float spawnPosZ = Random.Range(minBoundZ, maxBoundZ);
        Vector3 spawnPos = new Vector3(spawnPosX, spawnPosY, spawnPosZ);
        Instantiate(targetBoxPrefab, spawnPos, targetBoxPrefab.transform.rotation);
    }
}
