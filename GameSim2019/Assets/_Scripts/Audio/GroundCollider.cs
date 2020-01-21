using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollider : MonoBehaviour
{

    public FootSteps steps;

    private void OnTriggerEnter ( Collider other )
    {
        if ( other.tag != "Player" && other.tag != "Blobisaur" && other.tag != "Audio" )
        {
            steps.TakeStep();
        }
            
    }

}
