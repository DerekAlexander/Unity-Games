using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public GameObject baseBlob;
    public GameObject speedBlob;
    public GameObject powerBlob;
    public GameObject glideBlob;

    public float pedestalOffset = 10f;

    public int theChosenOnesID;
    public int difficulty;

    private GameObject startingPedestol;

    private FarmData data;

    private GameObject newPet;

    private PreCompeitionCamera precompCam;

    private SceneController sceneController;

    private int place;

    // Start is called before the first frame update
    void Awake ( )
    {
        startingPedestol = GameObject.Find("StartingPedestal");

        DontDestroyOnLoad(this);
        LoadTheBlobies();

        precompCam = GameObject.Find("CM vcam1").GetComponent<PreCompeitionCamera>();
        sceneController = FindObjectOfType<SceneController>();
    }

    // Update is called once per frame
    void Update ( )
    {

    }



    public void LoadTheBlobies ( )
    {
        data = FarmSaving.LoadFarm();
        
        for ( int i = 0; i < data.pets.Length; i++ )
        {
            LoadABlobie(data.pets[i], i); // pull the corisponding CreatureData to match the new pet
        }


    }


    private void LoadABlobie ( CreatureData creature, int pos )
    {
        //GameObject newPet;

        switch ( creature.bodyType )
        {
            case "Base":
                newPet = Instantiate(baseBlob,
                                      new Vector3(startingPedestol.transform.position.x + ( pos * pedestalOffset ),
                                                  startingPedestol.transform.position.y,
                                                  startingPedestol.transform.position.z),
                                      startingPedestol.transform.rotation)
                                      as GameObject; // create new pet
                break;

            case "Speed":
                newPet = Instantiate(speedBlob,
                                      new Vector3(startingPedestol.transform.position.x + ( pos * pedestalOffset ),
                                                  startingPedestol.transform.position.y,
                                                  startingPedestol.transform.position.z),
                                      startingPedestol.transform.rotation)
                                      as GameObject; // create new pet
                break;

            case "Power":
                newPet = Instantiate(powerBlob,
                                      new Vector3(startingPedestol.transform.position.x + ( pos * pedestalOffset ),
                                                  startingPedestol.transform.position.y,
                                                  startingPedestol.transform.position.z),
                                      startingPedestol.transform.rotation)
                                      as GameObject; // create new pet
                break;

            case "Glide":
                newPet = Instantiate(glideBlob,
                                      new Vector3(startingPedestol.transform.position.x + ( pos * pedestalOffset ),
                                                  startingPedestol.transform.position.y,
                                                  startingPedestol.transform.position.z),
                                      startingPedestol.transform.rotation)
                                      as GameObject; // create new pet
                break;

            default: // By default assume it is a base model
                newPet = Instantiate(baseBlob,
                                      new Vector3(startingPedestol.transform.position.x + ( pos * pedestalOffset ),
                                                  startingPedestol.transform.position.y,
                                                  startingPedestol.transform.position.z),
                                      startingPedestol.transform.rotation)
                                      as GameObject; // create new pet

                Debug.LogError("Body type missing in Blobie, instantiated base model by default");

                break;
        }


        AIStatSheet stats = newPet.GetComponent<AIStatSheet>(); // get reference to the AIStatSheet from each pet

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
        stats.ApplyNewStats();

    }


    public void InitCompetition ( GameObject newStartingPoint)
    {
        startingPedestol = newStartingPoint;

        CompetitionChallenges[] obst = FindObjectsOfType<CompetitionChallenges>();
        for ( int i = 0; i < obst.Length; i++ )
        {
            obst[i].InitializeChallengeDifficulty(difficulty);
        }
        FindTheChosenOne();
    }

    public GameObject ChosenOne()
    {
        StartComp startComp = FindObjectOfType<StartComp>();
        //TODO: update to non-depreciated
        newPet.transform.rotation.SetEulerAngles(startComp.playerBlobieStartPoint.transform.rotation.eulerAngles);
        newPet.GetComponent<SphereCollider>().radius = .5f;
        return newPet;
    }

    private void FindTheChosenOne ( )
    {
        for ( int i = 0; i < data.pets.Length; i++ )
        {
            if ( data.pets[i].ID == theChosenOnesID ) // finds the blobie that the player wanted to compete
            {
                // reset the GameObject startingPedestal to the starting point in the competition
                LoadABlobie(data.pets[i], 0); // load the chosen blobie
                break; // end loop
            }
        }
    }


    public void SelectBlobie ( )
    {
        theChosenOnesID = precompCam.SelectedBlobieId();
    }

    public void SelectDifficulty ( int dif )
    {
        difficulty = dif;
        sceneController.LoadScene("Competition_Test");
    }

    public void LoadToFarmAndDestroy ( )
    {
        sceneController.LoadScene("TestLayout");
        FindObjectOfType<MusicController>().ChangeToIslandMusic();
        Destroy(gameObject);
    }



    public void RaceOver ( int placeFinished )
    {
        place = placeFinished;
        sceneController.LoadScene("TestLayout");
        StartCoroutine(WaitingForIslandToLoad());
    }


    IEnumerator WaitingForIslandToLoad ( )
    {
        while ( SceneController.ActiveSceneName() != "TestLayout" )
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        FindObjectOfType<Santa>().GivePresent(place, difficulty);
        DispenceHappiness();
        Destroy(gameObject);
    }



    private void DispenceHappiness ( )
    {
        AIStatSheet[] blobies = FindObjectsOfType<AIStatSheet>();

        for ( int i = 0; i < blobies.Length; i++ )
        {
            if ( blobies[i].ID == theChosenOnesID)
            {
                blobies[i].happiness += HappinessFactor();
                Debug.Log("applying the happiness");
            }
        }
    }


    private int HappinessFactor ( )
    {
        int hap = 0;
        switch ( place )
        {
            case 1:
                hap = 25;
                break;
            case 2:
                hap = 15;
                break;
            case 3:
                hap = 10;
                break;
            case 4:
                hap = 0;
                break;
            case 5:
                hap = -10;
                break;
            case 6:
                hap = -15;
                break;

        }

        return hap;
    }

}
