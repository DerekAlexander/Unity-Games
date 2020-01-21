using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialFruitSpawner : MonoBehaviour
{
    [Tooltip("Referance object to the food you want to spawn. Make sure it is the same type of fruit that " +
             "grows on the tree you are placing it by")]
    public GameObject food;
    
    [Tooltip("Empty GameObjects around the tree where fruit can spawn")]
    public List<GameObject> spawnPoints;
    
    void Start ( )
    {
        int numToSpawn = Random.Range(0, spawnPoints.Count); // random number of foods to spawn

        for ( int i = 0; i < numToSpawn; i++ )
        {
            int rand = Random.Range(0, spawnPoints.Count); // random spawn point picked
            Instantiate(food, spawnPoints[rand].transform.position, spawnPoints[rand].transform.rotation); // spawn food
            spawnPoints.RemoveAt(rand); // remove spawn point from list so it can't be called again
        }
    }


}
