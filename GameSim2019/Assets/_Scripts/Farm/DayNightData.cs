using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DayNightData
{

    public float[] sunRotation = new float[3];

    public int days;

    public bool hasSentDayMessage;
    public bool hasSEntNightMessage;
    public bool isNight;



    public DayNightData ( DayNightCycle cycle )
    {
        sunRotation[0] = cycle.GetRotation().x;
        sunRotation[1] = cycle.GetRotation().y;
        sunRotation[2] = cycle.GetRotation().z;


        this.days = cycle.GetDays();

        this.hasSentDayMessage = cycle.hasSentDayMessage;
        this.hasSEntNightMessage = cycle.hasSEntNightMessage;
        this.isNight = DayNightCycle.isNight;

    }


}
