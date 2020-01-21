using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreedingTutorialOne : Tutorial
{
    // Start is called before the first frame update
    void Start ( )
    {
        if ( FindObjectOfType<Santa>().hasGivenEasyFirstPlacePresent )
            readyToBeSeen = true;
    }

    // Update is called once per frame
    void Update ( )
    {

    }

    public void ActivateTut ( )
    {
        readyToBeSeen = true;
    }


    private void OnTriggerEnter ( Collider other )
    {

        if ( other.tag == "Player" && readyToBeSeen )
        {
            DisplayUITutorial();
        }
    }



}
