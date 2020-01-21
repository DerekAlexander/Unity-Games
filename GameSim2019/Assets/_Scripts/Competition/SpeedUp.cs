using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpeedUp : MonoBehaviour
{

    float savedSpeed = -1;

    // Start is called before the first frame update
    void Start ( )
    {

    }

    // Update is called once per frame
    void Update ( )
    {

    }


    private void OnTriggerEnter ( Collider other )
    {
        if ( other.tag == "Blobisaur" )
        {
            NavMeshAgent agent = other.GetComponent<NavMeshAgent>();

            Debug.Log("Entered into Jumping action");

            if ( agent.speed < 6 )
            {
                savedSpeed = agent.speed;
                agent.speed = 6;
            }
        }
    }


    private void OnTriggerExit ( Collider other )
    {
        if ( other.tag == "Blobisaur" )
        {
            Debug.Log("Exited out of  Jumping action");
            NavMeshAgent agent = other.GetComponent<NavMeshAgent>();
            if ( savedSpeed > 0)
                agent.speed = savedSpeed;
        }
    }
}
