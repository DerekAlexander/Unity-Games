using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterActionTutorial : Tutorial
{

    private void OnTriggerEnter ( Collider other )
    {
        if ( other.tag == "Player" || other.name == "Chest" )
        {
            DisplayUITutorial();
        }
    }

}
