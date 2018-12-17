using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour {

    public GameObject coin;
    public GameObject instatiateCoin;
    public float spawnTime = 1f;          
    public Transform[] spawnPoints;
    public void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Update()
    {

        Destroy(instatiateCoin, 4f);

    }

    public void Spawn()
    {

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        instatiateCoin = (GameObject)Instantiate(coin, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

    }
}

