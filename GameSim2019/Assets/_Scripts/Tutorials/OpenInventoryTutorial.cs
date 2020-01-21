using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventoryTutorial : Tutorial
{

    public GameObject otherTutorial;

    bool hasDeactivated = false;

    void Update ( )
    {

        if ( !otherTutorial.activeSelf )
            hasDeactivated = true;
    }



    private void OnTriggerEnter ( Collider other )
    {
        if ( other.tag == "Food" && hasDeactivated )
        {
            DisplayUITutorial();
        }
    }
}
