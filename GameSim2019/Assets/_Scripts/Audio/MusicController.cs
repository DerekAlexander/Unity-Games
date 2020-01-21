using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    private AudioClip[] currentPlayList;


    [Tooltip("Music to be played while in the main menu")]
    [SerializeField] AudioClip[] menuMusic;
    [Tooltip("Music to be played while in the ranch")]
    [SerializeField] AudioClip[] ranchMusic;
    [Tooltip("Music to be played while out exploring the island")]
    [SerializeField] AudioClip[] islandMusic;
    [Tooltip("Music to be played while in the competition scene")]
    [SerializeField] AudioClip[] competitionMusic;

    private AudioSource audioSource;


    [SerializeField] float transitionTime = .5f;


    private int lastSong = -1;

    private bool isFadingOut = false;

    // Use this for initialization
    void Awake ()
    {

        int numOfMusicControllers = FindObjectsOfType<MusicController>().Length;

        if ( numOfMusicControllers > 1 )
            Destroy(gameObject);
        else
            DontDestroyOnLoad(this.gameObject);


        audioSource = GetComponent<AudioSource>();

    }

    private void Start ()
    {
        currentPlayList = menuMusic; // activates on start of this objects life ( in the MainMenu )
    }

    private void PickSong ()
    {
        if ( !audioSource.isPlaying )
        {
            if ( currentPlayList.Length > 1 )
            {
                int rand = lastSong;
                while ( rand == lastSong )
                    rand = Random.Range(0, currentPlayList.Length);
                lastSong = rand;
                audioSource.clip = currentPlayList[lastSong];
                audioSource.Play();
            }
            else if ( currentPlayList.Length == 0 )
            {
                // Do nothing
            }
            else
            {
                audioSource.clip = currentPlayList[0]; // if there is only one song loop that one song
                audioSource.Play();
            }
        }
    }



    // Update is called once per frame
    void Update ()
    {
        PickSong();
    }


    public void ChangeToMusic ( string newState )
    {
        switch ( newState )
        {
            case "Ranch":
                ChangeToRanchMusic();
                break;
            case "Island":
                ChangeToIslandMusic();
                break;
            case "Competition":
                ChangeToCompetitionMusic();
                break;
            case "Menu":
                ChangeToMenuMusic();
                break;
            default:
                //Do nothing
                break;
        }
    }

    public void ChangeToMenuMusic ( )
    {
        if ( currentPlayList == menuMusic && !isFadingOut)
        {
            return;
        }
        StopAllCoroutines();
        StartCoroutine(TransitionMusic(menuMusic));
    }

    public void ChangeToIslandMusic ( )
    {
        if ( currentPlayList == islandMusic && !isFadingOut )
        {
            return;
        }
        StopAllCoroutines();
        StartCoroutine(TransitionMusic(islandMusic));
    }

    public void ChangeToRanchMusic ()
    {
        if ( currentPlayList == ranchMusic && !isFadingOut )
        {
            return;
        }

        StopAllCoroutines();
        StartCoroutine(TransitionMusic(ranchMusic));
    }


    public void ChangeToCompetitionMusic ()
    {
        if ( currentPlayList == competitionMusic && !isFadingOut )
        {
            return;
        }
        StopAllCoroutines();
        StartCoroutine(TransitionMusic(competitionMusic));
    }

    private IEnumerator TransitionMusic ( AudioClip[] newMusic )
    {
        
        StartCoroutine(MusicFadeOut());
        while ( isFadingOut )
            yield return null;

        audioSource.Stop();
        currentPlayList = newMusic;
        PickSong();
        StartCoroutine(MusicFadeIn());

    }


    // for use when going into a new area of the map and you need to fade music out mid song
    IEnumerator MusicFadeOut ( )
    {
        isFadingOut = true;
        while ( audioSource.volume > 0 )
        {
            audioSource.volume -= transitionTime * Time.deltaTime;
            yield return null;
        }
        isFadingOut = false;
        //yield return null;
    }

    IEnumerator MusicFadeIn ()
    {
        while ( audioSource.volume < 1 )
        {
            audioSource.volume += transitionTime * Time.deltaTime;
            yield return null;
        }
    }


    public string MusicState ( )
    {
        if ( currentPlayList == ranchMusic )
            return "Ranch";
        else
            return "Island";
    }

}
