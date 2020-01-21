using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolutionManager : MonoBehaviour
{


    public GameObject speedyBlobie;
    public GameObject strongBlobie;
    public GameObject jumpyBlobie;

    public AudioClip evolveSFX;

    private FarmManager farm;
    private AudioSource audioSource;

    public GameObject particle;
    public PlayAllParticles playAll;



    private void Start ()
    {
        farm = FindObjectOfType<FarmManager>();
        audioSource = GetComponent<AudioSource>();
    }


    public bool Evolve ( GameObject oldBlobie )
    {

        AIStatSheet oldStats = oldBlobie.GetComponent<AIStatSheet>();

        // check stats of the blobie
        // depending on stats instantiate a new body of the right type


        float maxStat = Mathf.Max(oldStats.speed, Mathf.Max(oldStats.glide, oldStats.power));

        if ( maxStat < 50 )
            return false;

        StartCoroutine(EvolveOverTime(oldBlobie));
        return true;
    }



    IEnumerator EvolveOverTime ( GameObject oldBlobie )
    {
        particle.transform.position = oldBlobie.transform.position;

        AIStatSheet oldStats = oldBlobie.GetComponent<AIStatSheet>();

        // check stats of the blobie
        // depending on stats instantiate a new body of the right type


        //TODO: add checks to see which body to spawn
        GameObject newBlobie = null;

        oldBlobie.GetComponent<AIFarmBehavior>().state = AIFarmBehavior.State.WAIT;

        playAll.PlayAll();
        audioSource.PlayOneShot(evolveSFX);

        yield return new WaitForSeconds(2);


        if ( oldStats.speed > oldStats.glide && oldStats.speed > oldStats.power )
        {
            newBlobie = Instantiate(speedyBlobie, oldBlobie.transform.position, oldBlobie.transform.rotation) as GameObject;

        }
        else if ( oldStats.glide > oldStats.speed && oldStats.glide > oldStats.power )
        {
            newBlobie = Instantiate(jumpyBlobie, oldBlobie.transform.position, oldBlobie.transform.rotation) as GameObject;

        }
        else if ( oldStats.power > oldStats.speed && oldStats.power > oldStats.glide )
        {
            newBlobie = Instantiate(strongBlobie, oldBlobie.transform.position, oldBlobie.transform.rotation) as GameObject;

        }
        else
            newBlobie = PickBetweenStats(oldStats);



        TransferData(newBlobie.GetComponent<AIStatSheet>(), oldStats);


        farm.DeregisterBlobisaur(oldBlobie.GetComponent<AIStatSheet>());
        Destroy(oldBlobie);

        FindObjectOfType<BabyMaker>().RefreshList();


        yield return null;
    }



    // TODO: make this more efficient
    private GameObject PickBetweenStats ( AIStatSheet stats )
    {
        GameObject newBlobie;
        float sp = stats.speed;
        float gl = stats.glide;
        float pw = stats.power;

        if ( sp == gl && gl == pw )
        {

            int rand = Random.Range(1, 4);
            if ( rand == 1)
                newBlobie = Instantiate(speedyBlobie, stats.transform.position, stats.transform.rotation) as GameObject;
            else if ( rand == 2 )
                newBlobie = Instantiate(jumpyBlobie, stats.transform.position, stats.transform.rotation) as GameObject;
            else
                newBlobie = Instantiate(strongBlobie, stats.transform.position, stats.transform.rotation) as GameObject;
        }
        else if ( sp == gl )
        {
            int rand = Random.Range(1, 3);
            if ( rand == 1 )
                newBlobie = Instantiate(speedyBlobie, stats.transform.position, stats.transform.rotation) as GameObject;
            else
                newBlobie = Instantiate(jumpyBlobie, stats.transform.position, stats.transform.rotation) as GameObject;
        }
        else if ( sp == pw )
        {
            int rand = Random.Range(1, 3);
            if ( rand == 1 )
                newBlobie = Instantiate(speedyBlobie, stats.transform.position, stats.transform.rotation) as GameObject;
            else
                newBlobie = Instantiate(strongBlobie, stats.transform.position, stats.transform.rotation) as GameObject;
        }
        else
        {
            int rand = Random.Range(1, 3);
            if ( rand == 1 )
                newBlobie = Instantiate(jumpyBlobie, stats.transform.position, stats.transform.rotation) as GameObject;
            else
                newBlobie = Instantiate(strongBlobie, stats.transform.position, stats.transform.rotation) as GameObject;
        }


        return newBlobie;
    }



    private void TransferData ( AIStatSheet newBody, AIStatSheet oldBody )
    {
        // boring stupid tedious stuff

        newBody.myName = oldBody.myName;

        newBody.age = oldBody.age;

        newBody.SetMaxStamina(oldBody.MaxStamina());
        newBody.SetStaminaGrade(oldBody.StaminaGrade());

        newBody.glide = oldBody.glide;
        newBody.SetMaxGlide(oldBody.MaxGlide());
        newBody.SetGlideGrade(oldBody.GlideGrade());

        newBody.power = oldBody.power;
        newBody.SetMaxPower(oldBody.MaxPower());
        newBody.SetPowerGrade(oldBody.PowerGrade());

        newBody.speed = oldBody.speed;
        newBody.SetMaxSpeed(oldBody.MaxSpeed());
        newBody.SetSpeedGrade(oldBody.SpeedGrade());

        newBody.maxHunger = oldBody.maxHunger;
        newBody.currentHunger = oldBody.maxHunger; // starts out with hunger meter full
        newBody.hungerLowerRate = oldBody.hungerLowerRate;

        newBody.SetStarvingState(oldBody.IsStarving());
        newBody.happiness = oldBody.happiness;

        newBody.ApplyNewStats();

    }
    
}
