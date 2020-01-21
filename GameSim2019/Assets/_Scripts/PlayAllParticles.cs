using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAllParticles : MonoBehaviour
{


    public ParticleSystem[] particles;



    // Start is called before the first frame update
    void Start ( )
    {

    }



    public void PlayAll ( )
    {
        for ( int i = 0; i < particles.Length; i++ )
        {
            particles[i].Play();
        }
    }


    // Update is called once per frame
    void Update ( )
    {

    }
}
