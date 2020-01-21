using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CompetitionChallenges : MonoBehaviour
{
    [Tooltip("this is to balance speed with other stats. this value controls how many times a animation will loop if they FAIL the challenge.")]
    public int timesToLoopAnimFail;

    [Tooltip("this is to balance speed with other stats. this value controls how many times a animation will loop if they are CLOSE to passing the challenge.")]
    public int timesToLoopAnimClose;

    [SerializeField] private int StatValue;
    [SerializeField] private enum Type { GLIDE, POWER };
    [SerializeField] private Type type;

    [SerializeField] private enum Difficulty { EASY, MEDIUM, HARD, MASTER };
    [SerializeField] private Difficulty difficulty;

    private AIStatSheet stats;
    private Animator anim;
    private NavMeshAgent agent;

    [SerializeField] private float passState;
    private float baseSpeed;

    // Start is called before the first frame update
    void Awake ()
    {

    }


    public void InitializeChallengeDifficulty ( int dif )
    {
        timesToLoopAnimFail = 3;
        timesToLoopAnimClose = 2;

        switch ( dif )
        {
            case 0:
                difficulty = Difficulty.EASY;
                StatValue = Random.Range(20, 100);
                break;

            case 1:
                difficulty = Difficulty.MEDIUM;
                StatValue = Random.Range(80, 200);
                break;

            case 2:
                difficulty = Difficulty.HARD;
                StatValue = Random.Range(190, 300);
                break;

            case 3:
                difficulty = Difficulty.MASTER;
                StatValue = Random.Range(350, 400);
                break;
        }
    }


    // Update is called once per frame
    void Update ()
    {

    }

    public void DoChallenge(GameObject other)
    {
        stats = other.GetComponent<AIStatSheet>();
        agent = other.GetComponent<NavMeshAgent>();
        anim = other.GetComponentInChildren<Animator>();

        switch ( type )
        {

            case Type.GLIDE:

                passState = Glide(difficulty);
                //baseSpeed = agent.speed;
                //Debug.Log("PASS STATE" +passState);
                //agent.speed /= passState;

                //anim.Play("Glide");
                //Debug.Log("trying to play glide anim! " + other.name + " " + passState);
                other.GetComponent<AICompetitionState>().JumpChallenge(passState);

                break;

            case Type.POWER:

                passState = Power(difficulty);
                ////agent.isStopped = true;

                //anim = other.GetComponentInChildren<Animator>();

                //for ( int i = 0; i < passState; i++ )
                //{
                //    Debug.Log("trying to play power anim! " + other.name);
                //    anim.Play("Power");
                //}

                //agent.isStopped = false;


                other.GetComponent<AICompetitionState>().PowerChallenge(passState * 2); // there is only one power challenge
                // so multiplying it to make it longer

                break;
        }

    }

    //DA: did it this way with switches so we can do different animations based on difficulty. 
    private float Glide ( Difficulty difficulty )
    {
        switch ( difficulty )
        {
            case Difficulty.EASY:
                return CompareStats(stats.adjustedGlide);
                //stop nav
                //depending how they do play glide/jump anim. or if fail slow down movement speed and play no anim another option slow down anim.

            case Difficulty.MEDIUM:
                return CompareStats(stats.adjustedGlide);

            case Difficulty.HARD:
                return CompareStats(stats.adjustedGlide);
            case Difficulty.MASTER:
                return CompareStats(stats.adjustedGlide);

        }
        return 0;
    }

    private float Power ( Difficulty difficulty )
    {
        switch ( difficulty )
        {
            case Difficulty.EASY:
                return CompareStats(stats.adjustedPower);

            case Difficulty.MEDIUM:
                return CompareStats(stats.adjustedPower);

            case Difficulty.HARD:
                return CompareStats(stats.adjustedPower);
            case Difficulty.MASTER:
                return CompareStats(stats.adjustedGlide);
        }
        return 0;
    }

    private float CompareStats ( float creatureStat )
    {
        if ( creatureStat <= StatValue )
        {
            return timesToLoopAnimFail;
        }
        //if >= statvalue - 10% give close pass
        else if ( creatureStat <= ( StatValue - ( StatValue * .1 ) ) )
        {
            return timesToLoopAnimClose;
        }
        else
            return 1;
    }

}
