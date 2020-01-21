using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldNoises : MonoBehaviour, DayNightEventInterface
{
    [Tooltip("At what distance from the player should this start to play sounds")]
    public float activateDistance = 30;


    [Tooltip("Sounds to be played during the day")]
    public AudioClip[] daySounds;
    [Tooltip("Sounds to be played during the night")]
    public AudioClip[] nightSounds;
    private AudioClip[] activeSounds;
    private AudioSource audioSource;

    private GameObject player;

    // Start is called before the first frame update
    void Start ( )
    {
        FindObjectOfType<DayNightCycle>().RegisterForDayNightEvents(this);
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        if ( DayNightCycle.isNight )
            Night();

        StartCoroutine(PlaySounds());
    }

    // Update is called once per frame
    void Update ( )
    {

    }

    IEnumerator PlaySounds ( )
    {
        while ( true )
        {

            if ( Vector3.Distance(transform.position, player.transform.position) > activateDistance )
            {
                yield return new WaitForSeconds(1);
                continue;
            }

            if ( !audioSource.isPlaying )
            {
                audioSource.PlayOneShot(activeSounds[Random.Range(0, activeSounds.Length)]);
            }



            yield return new WaitForSeconds(Random.Range(audioSource.timeSamples, audioSource.timeSamples * Random.Range(2,5)));
        }
    }



    public void Morning ()
    {
        activeSounds = daySounds;
    }

    public void Night ()
    {
        activeSounds = nightSounds;
    }
}
