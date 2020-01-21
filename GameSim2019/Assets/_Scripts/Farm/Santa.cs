using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Santa : MonoBehaviour
{

    public GameObject baseBlob;

    public GameObject initSpawnPoint;

    public GameObject fruitSpawnPoint;
    public GameObject rareFruitOne, rareFruitTwo, rareFruitThree, rareFruitFour;


    private AudioSource audioSource;
    public AudioClip moneySound;


    public bool hasGivenEasyFirstPlacePresent = false;
    public bool hasGivenMediumFirstPlacePresent = false;
    public bool hasGivenHardFirstPlacePresent = false;
    public bool hasGivenMasterFirstPlacePresent = false;

    public bool hasCompletedCompTut = false;

    private void Awake ()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void GivePresent ( int place, int difficulty )
    {
        hasCompletedCompTut = true;
        
        switch ( place )
        {
            case 1:
                GiveFirstPlacePresent(difficulty);
                break;
            case 2:
                GiveSecondPlacePresent(difficulty);
                break;
            case 3:
                GiveThirdPlacePresent(difficulty);
                break;
        }
    }



    private void GiveThirdPlacePresent ( int dif )
    {

        switch ( dif )
        {
            case 0:
                GivePlayerMoney(50);
                break;
            case 1:
                GivePlayerMoney(100);
                break;
            case 2:
                GivePlayerMoney(175);
                break;
            case 3:
                GivePlayerMoney(300);
                break;
        }

    }
    private void GiveSecondPlacePresent ( int dif )
    {

        switch ( dif )
        {
            case 0:
                SpawnRareFruit(0);
                break;
            case 1:
                SpawnRareFruit(1);
                break;
            case 2:
                SpawnRareFruit(2);
                break;
            case 3:
                SpawnRareFruit(3);
                break;
        }

    }
    private void GiveFirstPlacePresent ( int dif )
    {

        switch ( dif )
        {
            case 0:
                if ( !hasGivenEasyFirstPlacePresent )
                {
                    GiveBaby('C');
                    hasGivenEasyFirstPlacePresent = true;
                    FindObjectOfType<BreedingTutorialOne>().ActivateTut();
                }
                else
                {
                    GiveThirdPlacePresent(dif);
                    GiveSecondPlacePresent(dif);
                }
                break;
            case 1:
                if ( !hasGivenMediumFirstPlacePresent )
                {
                    GiveBaby('B');
                    hasGivenMediumFirstPlacePresent = true;
                }
                else
                {
                    GiveThirdPlacePresent(dif);
                    GiveSecondPlacePresent(dif);
                }
                break;
            case 2:
                if ( !hasGivenHardFirstPlacePresent )
                {
                    GiveBaby('A');
                    hasGivenHardFirstPlacePresent = true;
                }
                else
                {
                    GiveThirdPlacePresent(dif);
                    GiveSecondPlacePresent(dif);
                }
                break;
            case 3:
                if ( !hasGivenMasterFirstPlacePresent )
                {
                    GiveBaby('S');
                    hasGivenMasterFirstPlacePresent = true;
                }
                else
                {
                    GiveThirdPlacePresent(dif);
                    GiveSecondPlacePresent(dif);
                }
                break;
        }

    }



    private void GivePlayerMoney ( int amount )
    {
        audioSource.PlayOneShot(moneySound);
        FindObjectOfType<ItemInventory>().AddCurrency(amount);
    }

    private void SpawnRareFruit (int gradeFruit )
    {
        switch ( gradeFruit )
        {
            case 0:
                Instantiate(rareFruitOne, fruitSpawnPoint.transform.position, fruitSpawnPoint.transform.rotation);
                break;
            case 1:
                Instantiate(rareFruitTwo, fruitSpawnPoint.transform.position, fruitSpawnPoint.transform.rotation);
                break;
            case 2:
                Instantiate(rareFruitThree, fruitSpawnPoint.transform.position, fruitSpawnPoint.transform.rotation);
                break;
            case 3:
                Instantiate(rareFruitFour, fruitSpawnPoint.transform.position, fruitSpawnPoint.transform.rotation);
                break;
        }
    }

    private void GiveBaby (char grade)
    {
        GameObject newEgg = Instantiate(baseBlob, initSpawnPoint.transform.position, baseBlob.transform.rotation) as GameObject;
        newEgg.GetComponent<Egg>().InstantiateletterGrades(grade, grade, grade);
    }





    public void LoadData ( SantaData data )
    {

        hasGivenEasyFirstPlacePresent = data.easy;
        hasGivenMediumFirstPlacePresent = data.medium;
        hasGivenHardFirstPlacePresent = data.hard;
        hasGivenMasterFirstPlacePresent = data.master;
        hasCompletedCompTut = data.hasCompletedCompTut;

    }

}
