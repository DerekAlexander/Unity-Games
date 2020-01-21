using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTutorial : Tutorial
{
    public string objectTag = "Player";

    private void OnTriggerEnter ( Collider other )
    {
        if ( other.tag == objectTag)
        {
            DisplayUITutorial();
        }
    }

}
