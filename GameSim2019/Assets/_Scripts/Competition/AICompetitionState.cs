using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICompetitionState : MonoBehaviour
{

    public enum State { Idle, Race, TugOfWar };
    public State state;

    //DA: came from aifarmbehavior. should it be one in the same?
    private float stamina;
    private float baseSpeed;

    private AIStatSheet stats;
    private NavMeshAgent agent;
    private GameObject[] waypoints;
    private Vector3 target;
    private Animator anim;
    private AudioSource audioSource;
    public AudioClip rockPunch;

    public int animationRuns = 0;


    public bool isAtEnd = false;


    private void Awake ()
    {
        stats = GetComponent<AIStatSheet>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();

        if ( SceneController.ActiveSceneName() == "Competition_Test" )
        {
            target = GameObject.FindGameObjectWithTag("Waypoint").transform.position;
        }

        stamina = stats.MaxStamina();
    }

    public void ApplyInitTarget ( )
    {
        agent.SetDestination(target);
    }


    public void Start ()
    {
        //DEBUG: this is here to update in editor stat changes. usually the game would have done this before entering the scene.
        //-------------
        stats.ApplyNewStats();
        //--------------
        baseSpeed = agent.speed;

        state = State.Idle;

    }

    public void DoBehavior ()
    {
        //something like this
        //if(SceneController.currentScene == "Competition_Test" )
        

        //DA: shouldnt this switch on currentScene name???? 
        //EG: No.
        switch ( state )
        {
            case State.Idle:
                //Do nothing
            break;

            case State.Race:
            Race();
            break;

            case State.TugOfWar:
            TugOfWar();
            break;
        }
    }


    public void JumpChallenge ( float result )
    {
        agent.speed = 0;
        agent.velocity = Vector3.zero;
        animationRuns = (int)result;
        anim.Play("JumpWarmUp");
    }

    public void PowerChallenge ( float result )
    {
        agent.speed = 0;
        agent.velocity = Vector3.zero;
        animationRuns = (int)result;
        anim.Play("Bash");
    }


    public void JumpEvent ( )
    {
        animationRuns--;
        if ( animationRuns > 0 )
        {
            anim.Play("JumpWarmUp");
        }
        else
        {
            agent.speed = baseSpeed;
            anim.Play("Jump");
        }
    }


    public void PowerEvent ()
    {
        animationRuns--;
        if ( animationRuns > 0 )
        {
            anim.Play("Bash");
            Debug.Log("Bashing");
        }
        else
        {
            Debug.Log("Done Bashing");
            anim.Play("Blank"); // for some reason I have to call an animation that can't loop here so it will go back to idle or it wont work
            agent.speed = baseSpeed;
        }
    }
    

    // call this at the last "puch" during the punch animations right before PowerEvent() is called
    // if this is the last animation to be run, destrow the rock
    public void PunchEven ( )
    {

        audioSource.PlayOneShot(rockPunch);

        if ( animationRuns == 1 )
        {
            //make rock explode
            Debug.Log("BOOM!!!!!");


            Collider[] hitColliders = Physics.OverlapSphere(transform.position + Vector3.forward, 2f);


            for ( int i = 0; i < hitColliders.Length; i++ )
            {
                if ( hitColliders[i].tag == "Rock" )
                {
                    hitColliders[i].GetComponent<Explosion>().ExplodeRock();
                    break;
                }
            }
        }
    }

    public void SetDestination ( Vector3 dest )
    {
        agent.SetDestination(dest);
    }

    private void Race ( )
    {

        stamina -= 1 * Time.deltaTime;
        if ( stamina <= 0 )
            Debug.Log("out of stamina " + this.name);
        //DA: maybe reduce speed?
        //state = State.SLEEP;

        if ( agent.remainingDistance <= .25f && isAtEnd )
        {
            state = State.Idle;
            agent.SetDestination(transform.position);
            agent.speed = 0;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 16, transform.eulerAngles.z);
        }

    }

    private void OnTriggerEnter ( Collider other )
    {

        if ( other.tag == "Challenge" )
        {
            other.GetComponent<CompetitionChallenges>().DoChallenge(gameObject);
        }
    }

    private void OnTriggerExit ( Collider other )
    {
        //agent.speed = baseSpeed;
    }

    private void TugOfWar ( )
    {

    }



}
