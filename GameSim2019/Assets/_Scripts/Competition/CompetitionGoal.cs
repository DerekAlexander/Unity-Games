using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompetitionGoal : MonoBehaviour
{
    public GameObject ExitButton;

    public ParticleSystem[] FireWorks;

    public GameObject[] standings;


    public GameObject firstPlace, secondPlace, thirdPlace;


    SelectionManager selectionManager;
    public int numOfFinished = 1;
    private int placed = 0;

    private AudioSource source;
    public AudioClip winClip, loseClip;

    MusicController musicController;

    // Start is called before the first frame update
    void Start()
    {
        selectionManager = FindObjectOfType<SelectionManager>();
        musicController = FindObjectOfType<MusicController>();
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter ( Collider other )
    {

        AIStatSheet temp = other.GetComponent<AIStatSheet>();
        if ( !temp )
            return;
        if ( temp.ID == -1)
        {
            temp.GetComponent<AICompetitionState>().SetDestination(standings[numOfFinished - 1].transform.position);
            numOfFinished++;
        }
        else
        {
            for ( int i = 0; i < FireWorks.Length; i++ )
            {
                FireWorks[i].Play();
            }
            temp.GetComponent<AICompetitionState>().SetDestination(standings[numOfFinished - 1].transform.position);
            placed = numOfFinished;
            DisplayFinishPlace();
            numOfFinished++;
            ExitButton.SetActive(true);
            Utils.CursorState(false);

            if ( placed < 4 )
            {
                source.PlayOneShot(winClip);
            }
            else
            {
                source.PlayOneShot(loseClip);
            }

        }


        if ( numOfFinished == 5 && placed != 0)
        {
            placed = 6;
            ExitButton.SetActive(true);
            DisplayFinishPlace();
            Utils.CursorState(false);
        }

    }


    private void DisplayFinishPlace ( )
    {
        switch ( placed )
        {
            case 1:
                firstPlace.SetActive(true);
                break;
            case 2:
                secondPlace.SetActive(true);
                break;
            case 3:
                thirdPlace.SetActive(true);
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
        }
    }


    public void LeaveCompetition ( )
    {
        selectionManager.RaceOver(placed);
        musicController.ChangeToIslandMusic();
        //call function to pass place of choosen one. 
    }

}
