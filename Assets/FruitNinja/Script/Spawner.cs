using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] fruitsPrefabs;
    public Transform[] spawnPoints;
    public float spawnTime;
    private float timer;
    public float velocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > spawnTime)
        {
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject randomPrefab = fruitsPrefabs[Random.Range(0, fruitsPrefabs.Length)];

            GameObject spawnedPrefab = Instantiate(randomPrefab, randomPoint.position, randomPoint.rotation);
            timer -= spawnTime;

            Rigidbody rb = spawnedPrefab.GetComponent<Rigidbody>();
            rb.velocity = randomPoint.forward * velocity;
        }
    }
}
