using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyMaker : MonoBehaviour, DayNightEventInterface
{

    public AIStatSheet parentOne, parentTwo;

    public GameObject[] eggSpawnPoint;

    public GameObject eggReference;


    private GameObject[] blobies;


    private void Start ()
    {
        RefreshList();
        StartCoroutine(CheckForLove());

        FindObjectOfType<DayNightCycle>().RegisterForDayNightEvents(this);

    }


    public void RefreshList ( )
    {
        blobies = GameObject.FindGameObjectsWithTag("Blobisaur");
    }

    IEnumerator CheckForLove ( )
    {
        while ( true )
        {
            if ( FindSpawnPoint() != -1 )
            {
                for ( int i = 0; i < blobies.Length; i++ )
                {
                    if ( blobies[i] && blobies[i].GetComponent<AIFarmBehavior>().wantsBaby )
                    {

                        if ( !parentOne ) // parentOne is empty store into parentOne
                            parentOne = blobies[i].GetComponent<AIStatSheet>();
                        else if ( !parentTwo && parentOne != blobies[i].GetComponent<AIStatSheet>() ) // parentTwo is empty store into parentTwo
                        {
                            parentTwo = blobies[i].GetComponent<AIStatSheet>();
                            break;
                        }
                    }
                }
            }
            if ( parentOne && parentTwo )
                break;

            RefreshList();
            yield return new WaitForSeconds(10);
        }

        StartCoroutine(LoveAtFirstSight());
    }


    IEnumerator LoveAtFirstSight ( )
    {
        while ( true )
        {
            if ( Vector3.Distance(parentOne.transform.position, parentTwo.transform.position) < 5 &&
                parentOne.gameObject.GetComponent<AIFarmBehavior>().state != AIFarmBehavior.State.SLEEP &&
                parentTwo.gameObject.GetComponent<AIFarmBehavior>().state != AIFarmBehavior.State.SLEEP )
            {
                parentOne.gameObject.GetComponent<AIFarmBehavior>().FallingInLove();
                parentTwo.gameObject.GetComponent<AIFarmBehavior>().FallingInLove();
                break;
            }

            yield return null;
        }
        StartCoroutine(WaitingForTheCoupleToArive());
    }

    IEnumerator WaitingForTheCoupleToArive ()
    {
        while ( true )
        {

            if ( parentOne.GetComponent<AIFarmBehavior>().state == AIFarmBehavior.State.WAIT &&
               parentTwo.GetComponent<AIFarmBehavior>().state == AIFarmBehavior.State.WAIT )
                break;
            yield return null;
        }
        CreateBaby();
    }

    public int SpawnBaby(char speed, char glide, char power)
    {

        int spawnNumber = FindSpawnPoint();

        if ( spawnNumber == -1 )
            return -1;

        GameObject newBaby = Instantiate(eggReference,
                                         eggSpawnPoint[spawnNumber].transform.position,
                                         eggSpawnPoint[spawnNumber].transform.rotation) as GameObject;



        Egg newEgg = newBaby.GetComponent<Egg>();


        newEgg.InstatiateStats(speed, glide, power, 1, 30);

        return 0;
    }

    private void CreateBaby ( )
    {

        // get totals of parents stats
        float totalSpeed = parentOne.speed + parentTwo.speed;
        float totalGlide = parentOne.glide + parentTwo.glide;
        float totalPower = parentOne.power + parentTwo.power;

        // get random numbers between 1-175 inclusve that the totals need to meet to level up
        float speedNeeded = Random.Range(1, 175);
        float glideNeeded = Random.Range(1, 175);
        float powerNeeded = Random.Range(1, 175);


        int spawnNumber = FindSpawnPoint();

        if ( spawnNumber == -1 )
            return;

        GameObject newBaby = Instantiate(eggReference,
                                         eggSpawnPoint[spawnNumber].transform.position,
                                         eggSpawnPoint[spawnNumber].transform.rotation) as GameObject;



        Egg newEgg = newBaby.GetComponent<Egg>();


        newEgg.InstatiateStats(
            CalculateLetterGrade(speedNeeded, totalSpeed, parentOne.SpeedGrade(), parentTwo.SpeedGrade()),
            CalculateLetterGrade(glideNeeded, totalGlide, parentOne.GlideGrade(), parentTwo.GlideGrade()),
            CalculateLetterGrade(powerNeeded, totalPower, parentOne.PowerGrade(), parentTwo.PowerGrade()),
            1,
            30);



        blobies = GameObject.FindGameObjectsWithTag("Blobisaur");
        parentOne.GetComponent<AIFarmBehavior>().PickNewState();
        parentTwo.GetComponent<AIFarmBehavior>().PickNewState();
        parentOne.GetComponent<AIFarmBehavior>().wantsBaby = false;
        parentTwo.GetComponent<AIFarmBehavior>().wantsBaby = false;
        parentOne.GetComponent<AIFarmBehavior>().isInLove = false;
        parentTwo.GetComponent<AIFarmBehavior>().isInLove = false;

        // setting wait time till these blobies can have another baby
        parentOne.daysTillNextBaby = 3;
        parentTwo.daysTillNextBaby = 3;

        // after the baby is made remove reference to the parents
        parentOne = null;
        parentTwo = null;
        StartCoroutine(CheckForLove());

    }

    public int FindSpawnPoint ( )
    {
        bool didHitEgg = false;
        for ( int i = 0; i < eggSpawnPoint.Length; i++ )
        {
            didHitEgg = false;
            Collider[] hits = Physics.OverlapSphere(eggSpawnPoint[i].transform.position, .5f);
            for ( int j = 0; j < hits.Length; j++ )
            {
                if ( hits[j].tag == "Egg" )
                {
                    didHitEgg = true;
                    break;
                }
            }
            if ( !didHitEgg )
                return i;
        }
        return -1;
    }


    private char CalculateLetterGrade ( float statNeeded, float totalStat, char gradeOne, char gradeTwo )
    {

        int statOne = gradeOne, statTwo = gradeTwo; // get int values of chars

        if ( totalStat < statNeeded ) // if requirements were not meet for level up 
        {
            if ( gradeOne != 'S' && gradeTwo != 'S')
                return (char)Mathf.Max(statOne, statTwo); // return the minimum char ( Max ASCII value )
            else
            {
                if ( gradeOne == 'S' )
                    return gradeTwo;
                else
                    return gradeOne;
            }
        }

        char shouldBeS = (char)( Mathf.Min(statOne, statTwo) - 1 ); // Check one above lower stat

        if ( shouldBeS == 'A' - 1 || shouldBeS == 'S' - 1 ) // checks if the new char passed 'A' or if it is S
        {
            return 'S'; // if so, return S
        }
        return shouldBeS;

    }


    private int BlobiesThatCanHaveBabies ( )
    {
        int count = 0;


        for ( int i = 0; i < blobies.Length; i++ )
        {
            if ( blobies[i].GetComponent<AIStatSheet>().ReadyToBreed() )
                count++;
        }

        return count;

    }


    public void Morning ()
    {
        int rand = Random.Range(1, 10);
        

        if ( rand == 5 ) // if love is meant to be
        {
            if ( BlobiesThatCanHaveBabies() >= 2 ) // if there is enough for love
            {
                for ( int i = 0; i < blobies.Length; i++ ) // loop through all blobies
                {
                    if ( !blobies[i].GetComponent<AIStatSheet>().ReadyToBreed() ) // if not ready to breed skip them
                        continue;

                    if ( !parentOne ) // parentOne is empty store into parentOne
                        parentOne = blobies[i].GetComponent<AIStatSheet>();
                    else if ( !parentTwo && parentOne != blobies[i].GetComponent<AIStatSheet>() ) // parentTwo is empty store into parentTwo
                    {
                        parentTwo = blobies[i].GetComponent<AIStatSheet>();
                        break;
                    }
                }
            }
        }
    }

    public void Night ()
    {

    }
}
