using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengerCreator : MonoBehaviour
{

    public GameObject baseBlob;
    public GameObject speedBlob;
    public GameObject powerBlob;
    public GameObject glideBlob;

    private string[] names = { "Nugub", "Glunk", "Bahman", "Ravi", "Skadi",
                                "Keven", "Sad b.", "Raziel", "Oinone", "Gertrud",
                                "ButtFace", "Leda", "Nanabozho", "Mentor", "Luia",
                                "Alexandros", "Indira", "Barbara", "Lykos", "Prosperus" };

    private char speedGrade, powerGrade, glideGrade, staminaGrade;

    private int difficulty;

    //private AIStatSheet tempStats;// = new AIStatSheet();
    private GameObject newPet;
    public CompeitionCameras compCams;

    public GameObject[] startingPoint;

    // Start is called before the first frame update
    void Awake()
    {
        compCams = FindObjectOfType<CompeitionCameras>();


        for (int i = 0; i < startingPoint.Length; i++ )
        {
            SpawnNewBlobie(i);
            compCams.blobies[i + 1] 
                = newPet;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomizeAllStats (AIStatSheet stats)
    {

        int speedRand, glideRand, powerRand, staminaRand, bodyRand;

        speedRand = Random.Range(0, 100);
        powerRand = Random.Range(0, 100);
        glideRand = Random.Range(0, 100);
        staminaRand = Random.Range(0, 100);
        bodyRand = Random.Range(0, 100);

        switch ( difficulty )
        {
            case 0://easy

                //stats.SetBodyType("Base");

                speedGrade = 'C';
                powerGrade = 'C';
                glideGrade = 'C';
                staminaGrade = 'C';

                break;

            case 1://medium


                if ( speedRand > 60 )
                    speedGrade = 'B';
                
                else
                    speedGrade = 'C';

                if ( powerRand > 60 )
                    powerGrade = 'B';

                else
                    powerGrade = 'C';

                if ( glideRand > 60 )
                    glideGrade = 'B';

                else
                    glideGrade = 'C';

                if ( staminaRand > 60 )
                    staminaGrade = 'B';

                else
                    staminaGrade = 'C';

                break;

            case 2://hard

                if ( speedRand > 40 )
                    speedGrade = 'A';

                else
                    speedGrade = 'B';

                if ( powerRand > 40 )
                    powerGrade = 'A';

                else
                    powerGrade = 'B';

                if ( glideRand > 40 )
                    glideGrade = 'A';

                else
                    glideGrade = 'B';

                if ( staminaRand > 40 )
                    staminaGrade = 'A';

                else
                    staminaGrade = 'B';

                break;

            case 3://master
                if ( speedRand > 20 )
                    speedGrade = 'S';

                else
                    speedGrade = 'A';

                if ( powerRand > 20 )
                    powerGrade = 'S';

                else
                    powerGrade = 'A';

                if ( glideRand > 20 )
                    glideGrade = 'S';

                else
                    glideGrade = 'A';

                if ( staminaRand > 20 )
                    staminaGrade = 'S';

                else
                    staminaGrade = 'A';
                break;

        }

        int i = 100;

        if ( difficulty == 0 )
            i = 60;

        if ( difficulty == 1 )
            i = 70;

        if ( difficulty == 2 )
            i = 80;

        stats.power = Random.Range(0, i);
        stats.speed = Random.Range(0, i);
        stats.glide = Random.Range(0, i);

        stats.SetSpeedGrade(speedGrade);
        stats.SetStaminaGrade(staminaGrade);
        stats.SetPowerGrade(powerGrade);
        stats.SetGlideGrade(glideGrade);

        int nameSpot = Random.Range(0, names.Length - 1);
        stats.myName = names[nameSpot];

        stats.ID = -1;
        switch ( stats.GetBodyName() )
        {
            case "Base":
                stats.GetComponent<SphereCollider>().radius = .5f;
                break;
            case "Speed":
                stats.GetComponent<SphereCollider>().radius = .75f;
                break;
            case "Glide":
                stats.GetComponent<SphereCollider>().radius = .5f;
                break;
            case "Power":
                stats.GetComponent<SphereCollider>().radius = 1f;
                break;

        }
        

        stats.ApplyNewStats();


    }

    private void SpawnNewBlobie ( int pos )
    {
        difficulty = FindObjectOfType<SelectionManager>().difficulty;

        int i = Random.Range(0, 4);
        if ( difficulty == 0 )
            i = 0;
        if ( difficulty == 3 )
            i = Random.Range(1, 4);

        switch ( i )
        {
            case 0:
                newPet = Instantiate(baseBlob,
                                      new Vector3(startingPoint[pos].transform.position.x,
                                                  startingPoint[pos].transform.position.y,
                                                  startingPoint[pos].transform.position.z),
                                      startingPoint[pos].transform.rotation)
                                      as GameObject; // create new pet
                break;

            case 1:
                newPet = Instantiate(speedBlob,
                                      new Vector3(startingPoint[pos].transform.position.x,
                                                  startingPoint[pos].transform.position.y,
                                                  startingPoint[pos].transform.position.z),
                                      startingPoint[pos].transform.rotation)
                                      as GameObject; // create new pet
                break;

            case 2:
                newPet = Instantiate(powerBlob,
                                      new Vector3(startingPoint[pos].transform.position.x,
                                                  startingPoint[pos].transform.position.y,
                                                  startingPoint[pos].transform.position.z),
                                      startingPoint[pos].transform.rotation)
                                      as GameObject; // create new pet
                break;

            case 3:
                newPet = Instantiate(glideBlob,
                                      new Vector3(startingPoint[pos].transform.position.x,
                                                  startingPoint[pos].transform.position.y,
                                                  startingPoint[pos].transform.position.z),
                                      startingPoint[pos].transform.rotation)
                                      as GameObject; // create new pet
                break;

            default: // By default assume it is a base model
                newPet = Instantiate(baseBlob,
                                      new Vector3(startingPoint[pos].transform.position.x,
                                                  startingPoint[pos].transform.position.y,
                                                  startingPoint[pos].transform.position.z),
                                      startingPoint[pos].transform.rotation)
                                      as GameObject; // create new pet

                Debug.LogError("Body type missing in Blobie, instantiated base model by default");

                break;
        }


        AIStatSheet stats = newPet.GetComponent<AIStatSheet>(); // get reference to the AIStatSheet from each pet

        RandomizeAllStats(stats);
    }

}
