using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ChestTutorial : Tutorial
{
    public Chest chest;

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
        if ( other.tag == "Player" && !chest.hasBeenOpened )
        {
            DisplayUITutorial();
        }
    }




}
