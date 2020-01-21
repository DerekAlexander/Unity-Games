using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour, DayNightEventInterface
{
    bool isNight = false;
    Light myLight;
    public ParticleSystem ps;
    
    private AudioSource audioSource;
    private CapsuleCollider capsule;



    public float minIntensity = 0.5f;
    public float maxIntensity = 5f;


    float randieBoo;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<DayNightCycle>().RegisterForDayNightEvents(this);
        audioSource = GetComponent<AudioSource>();
        myLight = GetComponent<Light>();
        randieBoo = Random.Range(0.0f, 65535.0f);
        
        capsule = GetComponent<CapsuleCollider>();
        if ( capsule )
            capsule.enabled = false;

        if ( DayNightCycle.isNight)
        {
            DelayedStart();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    IEnumerator Flicker ( )
    {
        while (isNight)
        {
            float noise = Mathf.PerlinNoise(randieBoo, Time.time);
            myLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
            yield return null;
        }
    }

    public void Morning ( )
    {
        isNight = false;
        myLight.enabled = false;
        if ( audioSource )
            audioSource.Stop();
        ps.Stop();
        if ( capsule )
            capsule.enabled = false;
    }
    public void Night ( )
    {
        Invoke("DelayedStart", Random.Range(0, 4));
    }


    private void DelayedStart ( )
    {
        isNight = true;
        myLight.enabled = true;
        if ( audioSource )
            audioSource.Play();
        ps.Play();

        if ( capsule )
            capsule.enabled = true;

        StartCoroutine(Flicker());
    }

}
