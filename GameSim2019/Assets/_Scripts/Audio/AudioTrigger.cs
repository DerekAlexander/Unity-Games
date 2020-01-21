using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{

    enum Music { RANCH, ISLAND, COMPETITION};

    [Tooltip("The type of music to switch to when the player enters this objects TriggerColider")]
    [SerializeField] Music music = Music.RANCH;

    private void OnTriggerEnter ( Collider other )
    {
        if (other.tag == "Player")
        {
            if ( music == Music.RANCH )
                FindObjectOfType<MusicController>().ChangeToRanchMusic();
            else if ( music == Music.ISLAND )
                FindObjectOfType<MusicController>().ChangeToIslandMusic();
            else if ( music == Music.COMPETITION )
                FindObjectOfType<MusicController>().ChangeToCompetitionMusic();
        }
    }

}
