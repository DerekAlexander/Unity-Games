using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{

    public AudioClip grassStep;
    public AudioClip woodStep;
    public AudioClip waterStep;
    


    public enum State { GRASS, WOOD, WATER};
    public State stepState = State.GRASS;


    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start ( )
    {
        audioSource = GetComponent<AudioSource>();
    }





    public void TakeStep ( )
    {
        switch ( stepState )
        {
            case State.GRASS:
                audioSource.PlayOneShot(grassStep);
                break;
            case State.WOOD:
                audioSource.PlayOneShot(woodStep);
                break;
            case State.WATER:
                audioSource.PlayOneShot(waterStep);
                break;
        }


    }


}
