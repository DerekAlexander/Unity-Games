using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryTutorials : Tutorial
{
    public GameObject initialTutorial;

    public bool activateOnActivation = false;


    private bool hasDisplayed = false;

    // Update is called once per frame
    void Update ( )
    {
        if ( activateOnActivation ) 
            OnActivation();
        else
            OnDeactivation();
    }



    private void OnDeactivation ( )
    {
        if ( !initialTutorial.activeSelf && !hasDisplayed )
        {
            DisplayUITutorial();
            hasDisplayed = true;
        }
    }


    private void OnActivation ( )
    {
        if ( initialTutorial.activeSelf && !hasDisplayed )
        {
            DisplayUITutorial();
            hasDisplayed = true;
        }
    }
    public bool HasDisplayed()
    {
        return hasDisplayed;
    }


}
