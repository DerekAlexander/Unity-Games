using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [Tooltip("Use as a prefab reference here for which fruit you want to grow.")]
    [SerializeField] GameObject fruit;

    [Tooltip("Use as a prefab reference here for which fruit you want to grow. This is a rare fruit and has a 10% chance of " +
             "spawning. If you don't want a rare fruit leave this empty")]
    [SerializeField] GameObject rareFruit;

    [Range(.005f,.02f)]
    [Tooltip("This is how much the scale of the fruit will grow per second.")]
    [SerializeField] float growRatePerSecond = .01f;

    [Range(.005f, .5f)]
    [Tooltip("The starting scale of the fruit when it is spawned.")]
    [SerializeField] float fruitStartSize = .05f;

    [Range(.75f, 3f)]
    [Tooltip("The size the fruit will grow to before it falls")]
    [SerializeField] float fruitMaxSize = 1.5f;

    [Tooltip("A range in time in seconds for the next fruit to start growing. (minimum time = x, maximum time = y")]
    [SerializeField] Vector2 waitTime = new Vector2(20,100);



    private GameObject growingFruit;

    // Start is called before the first frame update
    void Start()
    {
        StartGrowing();
        //Invoke("StartGrowing", Random.Range(waitTime[0], waitTime[1]));
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void StartGrowing ( )
    {

        if ( rareFruit )
        {
            int rand = Random.Range(0, 10);
            if ( rand == 2 )
                growingFruit = Instantiate(rareFruit, transform) as GameObject;
            else
                growingFruit = Instantiate(fruit, transform) as GameObject;

        }
        else
            growingFruit = Instantiate(fruit, transform) as GameObject;

        growingFruit.transform.localScale = new Vector3(fruitStartSize, fruitStartSize, fruitStartSize);
        growingFruit.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        growingFruit.GetComponent<Food>().inInventory = true;

        StartCoroutine(GrowFoods());
    }


    IEnumerator GrowFoods ( )
    {
        while (growingFruit.transform.localScale.x < fruitMaxSize )
        {

            growingFruit.transform.localScale = new Vector3(growingFruit.transform.localScale.x + growRatePerSecond,
                                                            growingFruit.transform.localScale.y + growRatePerSecond,
                                                            growingFruit.transform.localScale.z + growRatePerSecond);



            yield return new WaitForSeconds(1);
        }

        growingFruit.transform.localScale = new Vector3(fruitMaxSize, fruitMaxSize, fruitMaxSize);

        growingFruit.transform.parent = null;
        growingFruit.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        growingFruit.GetComponent<Food>().inInventory = false;
        growingFruit = null;

        Invoke("StartGrowing", Random.Range(waitTime.x, waitTime.y));

    }


}
