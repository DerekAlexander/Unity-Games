using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIStatSheet : MonoBehaviour, DayNightEventInterface
{
    private NavMeshAgent navMeshAgent;

    public string myName;
    [SerializeField] string bodyType;
    private int adultAge = 3; 
    private int retiredAge = 7;

    [SerializeField] float maxStamina = 100;
    private char staminaGrade;

    public float power;
    public float adjustedPower;
    private float maxPower = 100.0f;
    public char powerGrade = 'C';

    public float speed;
    public float adjustedSpeed;
    private float baseSpeed = .5f;
    private float maxSpeed = 100.0f;
    public char speedGrade = 'C';

    public float glide;
    public float adjustedGlide;
    private float maxGlide = 100.0f;
    public char glideGrade = 'C';

    public float speedMaxAnimSpeed = 1;
    public float glideMaxAnimSpeed = 1;
    public float powerMaxAnimSpeed = 1;
    public float baseMaxAnimSpeed = 1;

    public float maxHunger;
    public float currentHunger;
    public float hungerLowerRate = .1f;
    private bool isStarving = false;

    public float health; //TODO: stuff
    public float happiness = 50;
    public float happinessLowerRate = .5f;


    public int age = 0;
    public int ageToEvolve = 1;
    public int ID = 0;

    public int daysTillNextBaby = 0;


    private bool isTryingToEvolve = false;

    public float walkAnimSpeed;
    public Animator animator;

    private void Awake ()
    {
        currentHunger = maxHunger;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start ()
    {
        //if in farm do this...
        if ( SceneController.ActiveSceneName() == "TestLayout" )
        {
            FindObjectOfType<FarmManager>().RegisterBlobisaur(this);

            FindObjectOfType<DayNightCycle>().RegisterForDayNightEvents(this);
        }

        ApplyNewStats();


        if ( age == ageToEvolve && bodyType == "Base" )
            StartCoroutine(TryToEvolve(Random.Range(10, 30)));
    }


    private void Update ()
    {
        CheckHunger();

#if UNITY_EDITOR
        if ( Input.GetKeyDown(KeyCode.G) )
        {
            FindObjectOfType<EvolutionManager>().Evolve(gameObject);
        }
        if ( Input.GetKeyDown(KeyCode.Alpha1) )
        {
            speed = 100;
        }
        if ( Input.GetKeyDown(KeyCode.Alpha2) )
        {
            glide = 100;
        }
        if ( Input.GetKeyDown(KeyCode.Alpha3) )
        {
            power = 100;
        }

#endif

    }

    // checks the current hunger level of the Blobisaur
    // if low lower the Blobisaurs speed
    private void CheckHunger ()
    {

        if ( currentHunger <= maxHunger * .2 && !isStarving  && !IsRetired() ) // if retired effects of low stamina don't matter    
        {
            navMeshAgent.speed = navMeshAgent.speed * .2f;
            isStarving = true;
        }

    }

    public void ApplyNewStats ( )
    {
        float p = 1, s = 1, g = 1;

        if ( bodyType == "Speed" )
            s = 1.1f;
        else if ( bodyType == "Glide" )
            g = 1.1f;
        else if ( bodyType == "Power" )
            p = 1.1f;

        navMeshAgent.speed = baseSpeed + (( speed * .015f ) * ( SpeedGradeMultiplier(speedGrade))) * s;
        adjustedPower = (power * OtherGradeMultiplier(powerGrade)) * p;
        adjustedGlide = (glide * OtherGradeMultiplier(glideGrade)) * g;
        adjustedSpeed = (speed * OtherGradeMultiplier(speedGrade)) * s;

    }


    public bool ReadyToBreed ( )
    {
        return ( bodyType != "Base" && daysTillNextBaby <= 0 );
    }


    public string GetBodyName ( )
    {
        return bodyType;
    }

    public void SetBodyType(string body)
    {
        bodyType = body;
    }


    // returns the maximum stamina this Blobisaur can have
    public float MaxStamina ( )
    {
        return maxStamina; //TODO: vary depending on staminaGrade later
    }
    public void SetMaxStamina ( float max )
    {
        maxStamina = max;
    }
    public char StaminaGrade ( )
    {
        return staminaGrade;
    }
    public void SetStaminaGrade ( char grade )
    {
        staminaGrade = grade;
    }


    public float MaxSpeed ( )
    {
        return maxSpeed;
    }
    public void SetMaxSpeed ( float max )
    {
        maxSpeed = max;
        
    }
    public char SpeedGrade ( )
    {
        return speedGrade;
    }
    public void SetSpeedGrade ( char grade )
    {
        speedGrade = grade;
    }


    public float MaxGlide ( )
    {
        return maxGlide;
    }
    public void SetMaxGlide ( float max )
    {
        maxGlide = max;
    }
    public char GlideGrade ( )
    {
        return glideGrade;
    }
    public void SetGlideGrade ( char grade )
    {
        glideGrade = grade;
    }


    public float MaxPower ( )
    {
        return maxPower;
    }
    public void SetMaxPower ( float max )
    {
        maxPower = max;
    }
    public char PowerGrade ()
    {
        return powerGrade;
    }
    public void SetPowerGrade ( char grade )
    {
        powerGrade = grade;
    }

    public float NavSpeed()
    {
        return navMeshAgent.speed;
    }

    public float NavVelocity()
    {
        return navMeshAgent.velocity.magnitude;
    }

    public bool IsStarving ( )
    {
        return isStarving;
    }
    public void SetStarvingState ( bool starving )
    {
        isStarving = starving;
    }

    public void SetAnimWalkSpeed()
    {
        switch ( bodyType )
        {
            case "Speed":
                walkAnimSpeed = Mathf.Lerp(.5f, speedMaxAnimSpeed, adjustedSpeed / 440);
                animator.speed = walkAnimSpeed;
                Debug.Log("setting speed monster speed");
                break;

            case "Glide":
                walkAnimSpeed = Mathf.Lerp(.5f, glideMaxAnimSpeed, adjustedSpeed / 400);
                animator.speed = walkAnimSpeed;
                Debug.Log("setting glide monster speed");
                break;

            case "Power":
                walkAnimSpeed = Mathf.Lerp(.5f, powerMaxAnimSpeed, adjustedSpeed / 400);
                animator.speed = walkAnimSpeed;
                Debug.Log("setting power monster speed");
                break;

            case "Base":
                walkAnimSpeed = Mathf.Lerp(.5f, baseMaxAnimSpeed, adjustedSpeed / 400);
                animator.speed = walkAnimSpeed;
                Debug.Log("setting base monster speed");
                break;

        }

    }

    private float SpeedGradeMultiplier ( char letter )
    {

        switch ( letter )
        {
            case 'C':
                return 1f;

            case 'B':
                return 2.34f;

            case 'A':
                return 3.67f;

            case 'S':
                return 5f;

        }
        return 0;
    }
    private float OtherGradeMultiplier ( char letter )
    {

        switch ( letter )
        {
            case 'C':
                return 1f;

            case 'B':
                return 2f;

            case 'A':
                return 3f;

            case 'S':
                return 4f;

        }
        return 0;
    }


    private void TryToEvolve ( )
    {
        if ( SceneController.ActiveSceneName() != "TestLayout" )
            return;

        isTryingToEvolve = false;
        if ( !FindObjectOfType<EvolutionManager>().Evolve(gameObject) )
        {
            Debug.Log("I are a failure!!!");
            ageToEvolve += 1;
        }
    }



    public void Morning ()
    {
        age++;
        if ( daysTillNextBaby > 0 )
            daysTillNextBaby--;

        if ( age == ageToEvolve && bodyType == "Base" && isTryingToEvolve )
            StartCoroutine(TryToEvolve(Random.Range(15, 100)));


        if ( age == retiredAge ) // if time to retire
        {
            GameObject[] blobies = GameObject.FindGameObjectsWithTag("Blobisaur"); 

            int numOfBlobies = 0;

            for ( int i = 0; i < blobies.Length; i++ )
            {
                if ( !blobies[i].GetComponent<AIStatSheet>().IsRetired() ) // count all blobies not retired
                    numOfBlobies++;
            }


            if ( numOfBlobies < 2 ) // if there aren't enough blobies don't retire yet.
                retiredAge += 2;
        }
        

    }


    IEnumerator TryToEvolve ( int seconds )
    {
        isTryingToEvolve = true;
        while ( seconds > 0)
        {
            seconds--;
            yield return new WaitForSeconds(1);
        }

        while ( !GetComponent<AIFarmBehavior>().CanEvolve() )
        {
            yield return null;
        }
        TryToEvolve();
    }

    public void Night ()
    {

    }

    public int AdultAge()
    {

        if ( bodyType == "Base" )
        {
            adultAge = age + 1;
        }
        else
            adultAge = age;

        return adultAge;
    }

    public int RetiredAge()
    {
        return retiredAge;
    }

    public bool IsRetired ( )
    {
        return ( age >= retiredAge );
    }
}
