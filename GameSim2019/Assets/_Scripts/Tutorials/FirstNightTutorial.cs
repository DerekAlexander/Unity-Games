using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstNightTutorial : Tutorial, DayNightEventInterface
{

    private bool timeToDisplay = false;
    // Start is called before the first frame update
    void Start ( )
    {
        FindObjectOfType<DayNightCycle>().RegisterForDayNightEvents(this);
    }

    // Update is called once per frame
    void Update ( )
    {
        if ( timeToDisplay )
        {
            timeToDisplay = false;
            DisplayUITutorial();
        }
    }
    

    public void Morning ()
    {
        
    }

    public void Night ()
    {
        timeToDisplay = true;
    }

}
