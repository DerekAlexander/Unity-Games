using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlies : MonoBehaviour, DayNightEventInterface
{

    public ParticleSystem fireFlies;

    private void Start ()
    {
        FindObjectOfType<DayNightCycle>().RegisterForDayNightEvents(this);
        if ( DayNightCycle.isNight)
        {
            Night();
        }
    }


    public void Morning ()
    {
        fireFlies.Stop();
    }

    public void Night ()
    {
        fireFlies.Play();
    }
}
