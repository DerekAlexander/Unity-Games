using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateItem : MonoBehaviour
{


    private void OnTriggerEnter ( Collider other )
    {
        //other.GetComponent<Collider>().enabled = false;
        if ( other.tag == "Food" )
        {
            other.GetComponent<Food>().inInventory = true;
        }
        other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    private void OnTriggerExit ( Collider other )
    {
        if ( other.tag == "Food" )
        {
            other.GetComponent<Food>().inInventory = false;
        }
        other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

    }

}
