using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitDaysTutorial : Tutorial
{
    public int daysTillActivation = 3;

    public GameObject objectToBeActivated;


    private DayNightCycle cycle;

    // Start is called before the first frame update
    void Start ( )
    {
        cycle = FindObjectOfType<DayNightCycle>();
        StartCoroutine(ToDeactivateOrNotToDeactivate());
    }

    // sadly this is needed because of annoying timing issues and dependencies on many other scripts to run first
    IEnumerator ToDeactivateOrNotToDeactivate ( ) 
    {
        for ( int i = 0; i < 5; i++ )
        {
            if ( objectToBeActivated && !GetState() )
                objectToBeActivated.SetActive(false);
            else
                objectToBeActivated.SetActive(true);
            yield return null;
        }
    }


    // Update is called once per frame
    void Update ( )
    {
        if ( daysTillActivation <= cycle.GetDays() )
            readyToBeSeen = true;
        else
            readyToBeSeen = false;
    }


    private void OnTriggerEnter ( Collider other )
    {

        if ( other.tag == "Player" && daysTillActivation <= cycle.GetDays() )
        {
            DisplayUITutorial();
            if ( objectToBeActivated )
                objectToBeActivated.SetActive(true);
        }
    }


}
