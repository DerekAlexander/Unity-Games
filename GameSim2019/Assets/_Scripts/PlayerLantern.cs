using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLantern : MonoBehaviour, DayNightEventInterface
{
    public GameObject lantern;
    public ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<DayNightCycle>().RegisterForDayNightEvents(this);


        if ( DayNightCycle.isNight )
            Night();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Morning ()
    {
        lantern.SetActive(false);
        ps.Stop();
    }

    public void Night ()
    {
        lantern.SetActive(true);
        ps.Play();
    }

}
