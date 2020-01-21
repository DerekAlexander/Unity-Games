using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Rigidbody[] rb;
    private AudioSource audiosource;

    // Use this for initialization
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    public void ExplodeRock ( )
    {
        // audiosource.Play();

        for ( int i = 0; i < rb.Length; i++ )
        {
            rb[i].isKinematic = false;

        }


        //Destroy(gameObject, 4f);

    }

}
