using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Tooltip("The speed in degrees per second that the sun(main directional light) rotates")]
    [SerializeField] float dayNightCycleSpeed = 1f;


    private List<DayNightEventInterface> eventList = new List<DayNightEventInterface>();

    [SerializeField] int days = 0;
    [HideInInspector] public bool hasSentDayMessage = true;
    [HideInInspector] public bool hasSEntNightMessage = false;


    public float sunRotation;
    public bool isCountingDown = false;

    public static bool isNight = false;




    private void Start ()
    {
        if ( isNight )
        {
            dayNightCycleSpeed *= 2;
        }
    }

    // Update is called once per frame
    void Update ( )
    {
        transform.Rotate(new Vector3(dayNightCycleSpeed * Time.deltaTime, 0,0));


        //night is roughtly 180 degrees / 20:40
        // when this time happens call SendNightMessage()
        // but it can only call once per day

        //morning is roughly -5 degrees / 08:19
        // when this time happens call SendNewDayMessage()
        // but it can only call once per day

        float newSunRotation = transform.rotation.eulerAngles.x;

        if ( newSunRotation > sunRotation )
        {
            isCountingDown = false;
        }
        else
        {
            isCountingDown = true;
        }
        sunRotation = newSunRotation;

        //float rotation = UnityEditor.TransformUtils.GetInspectorRotation(transform).x;

        if ( !isCountingDown && !hasSentDayMessage && sunRotation > 355 && sunRotation < 357 )
            SendNewDayMessage();
        if ( isCountingDown && !hasSEntNightMessage && sunRotation > 355 && sunRotation < 357 )
            SendNightMessage();



    }




    public void RegisterForDayNightEvents ( DayNightEventInterface obj )
    {
        eventList.Add(obj);
    }


    public void SendNewDayMessage ( )
    {
        DayNightCycle.isNight = false;
        dayNightCycleSpeed /= 2;
        hasSEntNightMessage = false;
        hasSentDayMessage = true;
        days++;
        foreach ( DayNightEventInterface messaged in eventList )
        {
            messaged.Morning();
        }
    }

    public void SendNightMessage ( )
    {
        DayNightCycle.isNight = true;
        dayNightCycleSpeed *= 2;
        hasSEntNightMessage = true;
        hasSentDayMessage = false;
        foreach ( DayNightEventInterface messaged in eventList )
        {
            messaged.Night();
        }
    }

    public void SetRotation ( Vector3 rotation )
    {
        transform.rotation = Quaternion.Euler(rotation);
    }
    public Vector3 GetRotation ( )
    {
        return transform.rotation.eulerAngles;
    }

    public void SetDays ( int day)
    {
        days = day;
    }
    public int GetDays ( )
    {
        return days;
    }

}
