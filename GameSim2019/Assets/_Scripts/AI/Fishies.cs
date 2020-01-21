using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishies : MonoBehaviour
{

    public GameObject[] waypoints;

    [Tooltip("Offset from the targeted waypoint. Potentially stop multiple fishes from ending in the exact same spot and clipping" +
        " recommended keeping the offset small")]
    public Vector3 offset = new Vector3(0, 0, 0);

    [Tooltip("The amount of metters the fish will move in a second")]
    public float speed = 1f;

    [Tooltip("True lets the fish move to a random waypoint, false makes it go from waypoint to waypoint sequentially.")]
    public bool moveAtRandom = true;

    [Tooltip("True and the fish will go from waypoint to waypoint at random times, false is the opposite")]
    public bool randomTime = true;

    [Tooltip("if randomTime is false, this is how often to move between waypoints")]
    public float timeBetweenMovements = 3f;


    int counter = 0;

    private void Awake ()
    {
        StartCoroutine(Movement());
    }

    IEnumerator Movement ( )
    {
        Vector3 target;
        Vector3 targetDir;
        while ( true )
        {
            if ( moveAtRandom )
                target = waypoints[Random.Range(0, waypoints.Length)].transform.position + offset;
            else
            {
                counter++;
                target = waypoints[counter % waypoints.Length].transform.position + offset;
            }

            targetDir = target - transform.position;
            while ( true )
            {
                if ( Vector3.Distance( transform.position, target) < 2 )
                {
                    break;
                }

                float step = speed * Time.deltaTime;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDir);
                transform.position = Vector3.MoveTowards(transform.position, target, step);
                yield return new WaitForFixedUpdate();
            }

            if ( randomTime )
                yield return new WaitForSeconds(Random.Range(3, 10));
            else
                yield return new WaitForSeconds(timeBetweenMovements);
        }

    }

}
