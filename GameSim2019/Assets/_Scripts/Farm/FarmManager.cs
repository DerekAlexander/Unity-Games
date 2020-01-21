using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    public List<AIStatSheet> blobies = new List<AIStatSheet>();
    public List<Egg> eggs = new List<Egg>();

    public GameObject baseBlob;
    public GameObject speedBlob;
    public GameObject powerBlob;
    public GameObject glideBlob;

    public GameObject egg;

    public GameObject initSpawnPoint;

    public DayNightCycle cycle;
    public TutorialManager tutorialManager;
    public Santa santa;
    public HouseItems houseItems;
    public CleanUp clean;
    public TreasureManager treasure;
    private int blobieID = 1;


    private void Awake ()
    {
        cycle = FindObjectOfType<DayNightCycle>();
        tutorialManager = FindObjectOfType<TutorialManager>();
        santa = FindObjectOfType<Santa>();
        houseItems = FindObjectOfType<HouseItems>();
        clean = FindObjectOfType<CleanUp>();
        treasure = FindObjectOfType<TreasureManager>();

        if (! LoadTheBlobies() )
        {
            GameObject newBlob = Instantiate(baseBlob, initSpawnPoint.transform.position, baseBlob.transform.rotation) as GameObject;
            AIStatSheet stats = newBlob.GetComponent<AIStatSheet>();

            stats.speed = Random.Range(15, 60);
            stats.glide = Random.Range(15, 60);
            stats.power = Random.Range(15, 60);
            stats.ApplyNewStats();
        }
        else
        {
            FindObjectOfType<KeepAwake>().gameObject.SetActive(false); // if not a new game deactivate this
        }
        
    }


    public void RegisterBlobisaur ( AIStatSheet stats )
    {
        if ( stats.ID == 0 )
        {
            stats.ID = blobieID;
            blobieID++;
        }
        blobies.Add(stats);
    }


    public void RegisterEgg ( Egg egg )
    {
        eggs.Add(egg);
    }


    public void DeregisterBlobisaur ( AIStatSheet stats )
    {
        blobies.Remove(stats);
    }
    public void DeregisterEgg ( Egg egg )
    {
        eggs.Remove(egg);
    }

    public void SaveTheBlobies ()
    {
        FarmSaving.SaveFarm(this);
    }


    public bool LoadTheBlobies ()
    {
        FarmData data = FarmSaving.LoadFarm();

        if ( data == null )
            return false;

        blobieID = data.blobieID;   

        LoadDayNight(data.dayNight);

        tutorialManager.LoadTutorials(data.tutorialData);

        santa.LoadData(data.santaData);

        houseItems.LoadData(data.houseItems);

        clean.LoadData(data.clean); 

        LoadEggs(data.eggs);

        treasure.LoadTreasure(data.treasure);

        for ( int i = 0; i < data.pets.Length; i++ )
        {
            GameObject newPet;


            CreatureData creature = data.pets[i]; // pull the corresponding CreatureData to match the new pet



            switch ( creature.bodyType )
            {
                case "Base":
                newPet = Instantiate( baseBlob,
                                      new Vector3(data.pets[i].pos[0], data.pets[i].pos[1], data.pets[i].pos[2]),
                                      Quaternion.Euler(new Vector3(data.pets[i].rotation[0], data.pets[i].rotation[1], data.pets[i].rotation[2])))
                                      as GameObject; // create new pet
                break;

                case "Speed":
                newPet = Instantiate( speedBlob,
                                      new Vector3(data.pets[i].pos[0], data.pets[i].pos[1], data.pets[i].pos[2]),
                                      Quaternion.Euler(new Vector3(data.pets[i].rotation[0], data.pets[i].rotation[1], data.pets[i].rotation[2])))
                                      as GameObject; // create new pet
                break;

                case "Power":
                    newPet = Instantiate(powerBlob,
                                          new Vector3(data.pets[i].pos[0], data.pets[i].pos[1], data.pets[i].pos[2]),
                                          Quaternion.Euler(new Vector3(data.pets[i].rotation[0], data.pets[i].rotation[1], data.pets[i].rotation[2])))
                                          as GameObject; // create new pet
                    break;

                case "Glide":
                    newPet = Instantiate(glideBlob,
                                          new Vector3(data.pets[i].pos[0], data.pets[i].pos[1], data.pets[i].pos[2]),
                                          Quaternion.Euler(new Vector3(data.pets[i].rotation[0], data.pets[i].rotation[1], data.pets[i].rotation[2])))
                                          as GameObject; // create new pet
                    break;

                default: // By default assume it is a base model
                newPet = Instantiate( baseBlob,
                                      new Vector3(data.pets[i].pos[0], data.pets[i].pos[1], data.pets[i].pos[2]),
                                      Quaternion.Euler(new Vector3(data.pets[i].rotation[0], data.pets[i].rotation[1], data.pets[i].rotation[2])))
                                      as GameObject; // create new pet

                Debug.LogError("Body type missing in Blobie, instantiated base model by default");

                break;
            }


            AIStatSheet stats = newPet.GetComponent<AIStatSheet>(); // get reference to the AIStatSheet from each pet

            stats.transform.position = new Vector3(creature.pos[0], creature.pos[1], creature.pos[2]);
            stats.transform.rotation = Quaternion.Euler(creature.rotation[0], creature.rotation[1], creature.rotation[2]);

            stats.myName = creature.name;

            
            stats.SetMaxStamina(creature.maxStamina);
            stats.SetStaminaGrade(creature.staminaGrade);

            stats.power = creature.power;
            stats.SetMaxPower(creature.maxPower);
            stats.SetPowerGrade(creature.powerGrade);

            stats.speed = creature.speed;
            stats.SetMaxSpeed(creature.maxSpeed);
            stats.SetSpeedGrade(creature.speedGrade);

            stats.glide = creature.glide;
            stats.SetMaxGlide(creature.maxGlide);
            stats.SetGlideGrade(creature.glideGrade);

            stats.currentHunger = creature.currentHunger;
            stats.hungerLowerRate = creature.hungerLowerRate;
            stats.maxHunger = creature.maxHunger;

            stats.SetStarvingState(creature.isStarving);
            stats.happiness = creature.happiness;

            stats.age = creature.age;
            stats.ID = creature.ID;

            stats.daysTillNextBaby = creature.daysTillNextBaby;

            newPet.GetComponent<AIFarmBehavior>().wantsBaby = creature.wantsBaby;

            stats.ApplyNewStats();
            

        }

        return true;

    }


    private void LoadEggs ( EggData[] savedEggs )
    {
        for ( int i = 0; i < savedEggs.Length; i++ )
        {
            GameObject newEgg = Instantiate(egg, 
                                            new Vector3(savedEggs[i].pos[0], savedEggs[i].pos[1], savedEggs[i].pos[2]),
                                            Quaternion.Euler(new Vector3(savedEggs[i].rotation[0], savedEggs[i].rotation[1], savedEggs[i].rotation[2])))
                                            as GameObject;

            Egg eggInit = newEgg.GetComponent<Egg>();
            eggInit.InstatiateStats(savedEggs[i].speed, savedEggs[i].glide, savedEggs[i].power,
                                    savedEggs[i].days, savedEggs[i].seconds);
            

        }
    }


    private void LoadDayNight ( DayNightData savedCycle )
    {
        cycle.SetRotation(new Vector3(savedCycle.sunRotation[0], savedCycle.sunRotation[1], savedCycle.sunRotation[2]));

        cycle.SetDays(savedCycle.days);

        cycle.hasSentDayMessage = savedCycle.hasSentDayMessage;
        cycle.hasSEntNightMessage = savedCycle.hasSEntNightMessage;
        DayNightCycle.isNight = savedCycle.isNight;

    }


    public int getBlobieID ( )
    {
        return blobieID;
    }
    public void setBlobieID ( int ID )
    {
        blobieID = ID;
    }

}
