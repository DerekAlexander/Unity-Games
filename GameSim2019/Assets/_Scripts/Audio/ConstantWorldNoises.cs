using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantWorldNoises : MonoBehaviour
{

    [Tooltip("At what distance from the player should this start to play sounds")]
    public float activateDistance = 30;


    [Tooltip("Sounds to be played during the day")]
    public AudioClip sound;
    private AudioSource audioSource;


    private GameObject player;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");


        audioSource.clip = sound;
        audioSource.loop = true;
        StartCoroutine(PlaySounds());

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    IEnumerator PlaySounds ()
    {
        while ( true )
        {

            if ( Vector3.Distance(transform.position, player.transform.position) > activateDistance )
            {
                audioSource.Stop();
                yield return new WaitForSeconds(1);
                continue;
            }

            if ( !audioSource.isPlaying )
            {
                
                audioSource.Play();
            }

            yield return null;
            
        }
    }

}
