using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

public class DayNightLighting : MonoBehaviour , DayNightEventInterface
{
    public Color nightColor;
    public Color dayColor;
    private Light myMoonLight;
    private Volume volume;
    private Light mySunLight;
    private float startingSunIntensity;
    public VolumeProfile day, night, currentProfile;
    public VolumeProfile[] dusk = new VolumeProfile[6];
    public Gradient dayNightColor = new Gradient();
    private GradientColorKey[] colorKey;
    private GradientAlphaKey[] alphaKey;


    // Start is called before the first frame update
    void Awake ()
    {
        volume = GameObject.Find("Scene Settings").GetComponent<Volume>();

        FindObjectOfType<DayNightCycle>().RegisterForDayNightEvents(this);

        mySunLight = GameObject.Find("Sun").GetComponent<Light>();
        myMoonLight = GetComponent<Light>();

        startingSunIntensity = mySunLight.intensity;

        nightColor = myMoonLight.color;
        dayColor = mySunLight.color;

        // when game starts assume we are in day so set the color of the moon to the suns color to have a nice rim lighting for day time. 
        myMoonLight.color = dayColor;


        colorKey = new GradientColorKey[2];
        alphaKey = new GradientAlphaKey[2];

        colorKey[0].color = dayColor;
        colorKey[0].time = 0f;
        colorKey[1].color = nightColor;
        colorKey[1].time = 1f;

        alphaKey[0].alpha = 1;
        alphaKey[1].alpha = 1;
        alphaKey[0].time = 0f;
        alphaKey[1].time = 1f;


        dayNightColor.SetKeys(colorKey, alphaKey);


        //DA: may cause issues when SAVING
        volume.profile = day;
    }

    private void Start ()
    {
        if ( DayNightCycle.isNight )
            StartCoroutine(SetToNight());
        else
            StartCoroutine(SetToDay());
    }


    IEnumerator SetToNight ( )
    {
        //yield return new WaitForEndOfFrame();
        while ( mySunLight.intensity != .5f )
        {
            SetToNightProfile();
            yield return null;
        }
    }


    IEnumerator SetToDay ( )
    {
        //yield return new WaitForEndOfFrame();
        while ( mySunLight.intensity != 3f )
        {
            SetToDayProfile();
            yield return null;
        }
    }

    public void Update ()
    {
        currentProfile = volume.profile;
        
    }

    public void Morning ()
    {
        StartCoroutine(SwitchToDayProfile());
    }

    public void Night ()
    {
        StartCoroutine(SwitchToNightProfile());

    }


    public void SetToDayProfile ( )
    {
        mySunLight.intensity = 3f;
        myMoonLight.intensity = 3f;
        volume.profile = day;
        mySunLight.color = dayColor;
        myMoonLight.color = dayColor;
    }

    public void SetToNightProfile ( )
    {
        mySunLight.intensity = .5f;
        myMoonLight.intensity = .5f;
        volume.profile = night;
        mySunLight.color = nightColor;
        myMoonLight.color = nightColor;
    }


    private IEnumerator SwitchToNightProfile ()
    {
        float colorLerps = 0;
        volume.profile = dusk[4];

        for (int i = 3 ; i >= 0; i-- )
        {

            while ( mySunLight.intensity >  i && mySunLight.intensity > .5f )
            {

                mySunLight.color = dayNightColor.Evaluate(colorLerps);
                myMoonLight.color = dayNightColor.Evaluate(colorLerps);

                mySunLight.intensity -= .05f;
                //Debug.Log("sun " + mySunLight.intensity);

                colorLerps += .0125f;

                if ( myMoonLight.intensity > .5f )
                    myMoonLight.intensity -= .05f;

                //Debug.Log("moon " + myMoonLight.intensity);

                yield return new WaitForSeconds(.25f);
                
  

            }

            volume.profile = dusk[i];
            //Debug.LogWarning("switch profile " + volume.profile);
            //Debug.Log(" moon " + myMoonLight.intensity);
           //Debug.Log(" sun " + mySunLight.intensity);

        }
        yield return null;
    }

    private IEnumerator SwitchToDayProfile ()
    {
        float colorLerps = 1;
        volume.profile = dusk[1];

        for ( int i = 0; i <= 3; i++ )
        {
            while ( mySunLight.intensity < i + 1 && mySunLight.intensity < 3f )
            {

                mySunLight.color = dayNightColor.Evaluate(colorLerps);
                myMoonLight.color = dayNightColor.Evaluate(colorLerps);

                mySunLight.intensity += .025f;
                //Debug.Log("sun " + mySunLight.intensity);

                colorLerps -= .0125f;

                if ( myMoonLight.intensity > 3f )
                    myMoonLight.intensity += .025f;

               //Debug.Log("moon " + myMoonLight.intensity);

                yield return new WaitForSeconds(.25f);



            }

            volume.profile = dusk[i + 1];
            //Debug.LogWarning("switch profile " + volume.profile);
            //Debug.Log(" moon " + myMoonLight.intensity);
            //Debug.Log(" sun " + mySunLight.intensity);

        }
        yield return null;
    }



}
