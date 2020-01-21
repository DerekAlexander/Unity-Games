using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIFarmBehavior : MonoBehaviour
{

    public float maxIdleTime = 10f;


    private float IdleTime;
    private float sleepTime;
    public float stamina;
    private bool isAsleep = false;
    public float stickHeight; 

    public float stoppingDistance;
    private string[] playAnimList = { "StuffedToy", "Whistle", "Crayon", "Dance" };

    private bool playerEntered = false;
    private bool isPlaying = false;
    private bool haveStick = false;

    private Vector3 target;
    private GameObject targetItem;

    private InteractionMenu interactionMenu;
    public GameObject TargetItem () { return targetItem; }

    private Transform toy;
    private GameObject player;

    private NavMeshAgent agent;

    public enum State { WAIT, WANDER, INVESTIGATE, SLEEP, IDLE, PLAY, STARVED, INTERACTION, FETCH, BABYMAKING };
    public State state;

    private AIStatSheet stats;

    private GameObject[] waypoints;
    private GameObject[] adultWaypoints;
    private GameObject[] retiredWaypoints;

    Animator animator;


    public ParticleSystem zzz, confused, sad, mad, hearts;

    public bool wantsBaby = false;
    public bool isInLove = false;



    AudioSource audioSource;
    public AudioClip sleepingSounds;
    public AudioClip idleSounds;
    public AudioClip happySounds;
    public AudioClip sadSounds;
    public AudioClip madSounds;
    public AudioClip eatSounds;


    private float defaultNavSpeed;
    void Awake ()
    {
        stats = GetComponent<AIStatSheet>();

        interactionMenu = GameObject.Find("Canvas").GetComponent<InteractionMenu>();
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        adultWaypoints = GameObject.FindGameObjectsWithTag("AdultWaypoint");
        retiredWaypoints = GameObject.FindGameObjectsWithTag("RetiredWaypoint");
        audioSource = GetComponent<AudioSource>();

        agent = GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player");

        animator = GetComponentInChildren<Animator>();

        IdleTime = Random.Range(1f, maxIdleTime);
        stamina = stats.MaxStamina();

        if ( SceneController.ActiveSceneName() == "TestLayout" )
            PickNewState();
        else
            state = State.WAIT;

        StartCoroutine(CheckUps());

    }
    private void Update ()
    {
        //this is checking for player wanting to view specific blobies stats
        if ( Input.GetKeyDown(KeyCode.F) && CanInteract() )
        {
            //updates the inputFields name with the current blobie name.
            GameObject.Find("Canvas").GetComponent<InteractionMenu>().UpdateNameOnOpen(stats.myName);
            state = State.INTERACTION;
            
        }


        animator.SetFloat("MoveSpeed", agent.velocity.magnitude);

    }

    private bool CanInteract ( )
    {
        if ( stats.IsRetired() )
            return false;
        bool canAct = ( playerEntered &&  state != State.INTERACTION && state != State.PLAY && state != State.SLEEP &&
                        state != State.FETCH && state != State.BABYMAKING && !player.GetComponent<Player>().IsPetting() && !TutorialManager.isDisplaying );


        GameObject interactMenu = GameObject.FindGameObjectWithTag("CreatureStatSheet");

        bool isBusy = !GameObject.Find("Canvas").GetComponent<InteractionMenu>().isInteracting;

        if ( !interactMenu && canAct && isBusy)
            return true;
        return false;
    }


    public bool CanEvolve ( )
    {
        return ( state != State.INTERACTION && state != State.PLAY && 
                 state != State.SLEEP && state != State.FETCH && state != State.BABYMAKING && 
                 !player.GetComponent<Player>().IsPetting() && !TutorialManager.isDisplaying );

    }

    private void FixedUpdate ()
    {
        
    }



    // coroutine that runs every 10 seconds meant to check the blobies stats and try to recover if anything
    // doesn't seem right
    IEnumerator CheckUps ( )
    {
        yield return new WaitForSeconds(3); // wait 3 seconds after load to give the blobies time to start up
        while ( true )
        {
            if ( (state == State.WANDER || state == State.INVESTIGATE ) && agent.velocity == Vector3.zero ) // if blobie is stopped when it shouldn't be
            {
                Debug.Log("Something might be up");
                yield return new WaitForSeconds(2); // wait two more seconds
                if ( ( state == State.WANDER || state == State.INVESTIGATE ) && agent.velocity == Vector3.zero ) // if blobie is still stopped
                {
                    state = State.IDLE; // pick new state to try to recover
                    Debug.Log("Fixing stuff up");
                }
            }
            if ( state == State.PLAY )
            {
                yield return new WaitForSeconds(5);
                Debug.Log("Something might be up");
                if ( state == State.PLAY )
                {
                    state = State.IDLE;
                    isPlaying = false;
                }
            }

            yield return new WaitForSeconds(10);
        }
    }


    // Finite state machine for this state(AIFarmBehavior)
    // checks the current state and calls the appropriate method to exicute that states behaviors
    public void DoBehavior ()
    {
        Depression();

        if ( agent.velocity.magnitude > 0 )
        {
            stats.SetAnimWalkSpeed();
        }
        else
        {
            animator.speed = 1.0f;
        }


        switch ( state )
        {
            case State.WANDER:
                Wander();
                break;
            case State.INVESTIGATE:
                Investigate();
                break;
            case State.SLEEP:
                Sleep();
                break;
            case State.IDLE:
                Idle();
                break;
            case State.PLAY:
                Play();
                break;
            case State.INTERACTION:
                Interact();
                break;
            case State.FETCH:
                Fetch();
                break;
            case State.STARVED:
                // He done dead son
                break;
            case State.WAIT:
                if ( SceneController.ActiveSceneName() != "PreCompetition" )
                    agent.SetDestination(transform.position);
                //meant to do nothing
                break;
            case State.BABYMAKING:
                MakeTheBaby();
                break;
        }
    }


    // when wandering, this picks a random point in the farm to set as its destination
    private void SetDestination ()
    {
        if ( stats.IsRetired() )
        {
            target = RandomNavSphere(retiredWaypoints[Random.Range(0, retiredWaypoints.Length)].transform.position, 5f, -1);
        }
        else if ( stats.age >= stats.AdultAge() )
        {
            int rand = Random.Range(0, 1);
            if ( rand == 0)
                target = RandomNavSphere(adultWaypoints[Random.Range(0, adultWaypoints.Length)].transform.position, 5f, -1);
            else
                target = RandomNavSphere(waypoints[Random.Range(0, waypoints.Length)].transform.position, 5f, -1);

        }
        else
        {
            target = RandomNavSphere(waypoints[Random.Range(0, waypoints.Length)].transform.position, 5f, -1);
        }

        agent.SetDestination(target);
    }

    // when idling, remain still while recovers stamina for a random time
    // when done idling, pick a new state
    void Idle ()
    {
        if ( stamina < stats.MaxStamina() )
            stamina += .5f * Time.deltaTime;

        IdleTime -= Time.deltaTime;
        if ( IdleTime <= 0 )
        {
            PickNewState();

            IdleTime = Random.Range(1f, maxIdleTime);
        }


    }


    public void PlayIdleSounds ( )
    {
        int num = Random.Range(1, 6);
        if ( num == 3 && !audioSource.isPlaying )
        {
            if ( stats.happiness <= 20 )
            {
                audioSource.PlayOneShot(madSounds);
                mad.Play();

            }
            else if ( stats.happiness <= 40 )
            {
                audioSource.PlayOneShot(sadSounds);
                sad.Play();
            }
            else if ( stats.happiness <= 80 )
                audioSource.PlayOneShot(idleSounds);

            else// he happy boi
            {
                audioSource.PlayOneShot(happySounds);
                hearts.Play();
            }



        }

    }

    public void Play()
    {
        if ( !isPlaying )
        {
            agent.isStopped = true;
            int i = Random.Range(0, playAnimList.Length);
            animator.Play(playAnimList[i]);
            PlayIdleSounds();
            isPlaying = true;
        }


    }


    // when sleeping, remain still and revocoer stamina until stamina is full
    // when done sleeping, pick a new state
    void Sleep ()
    {
        if(!isAsleep)
        {
            audioSource.PlayOneShot(sleepingSounds);
            animator.Play("FallAsleep");
            isAsleep = true;
            agent.destination = transform.position;
            zzz.Play();
            defaultNavSpeed = agent.speed;
            agent.speed = 0;
        }

        if ( !audioSource.isPlaying )
            audioSource.PlayOneShot(sleepingSounds);


        stamina += 1f * Time.deltaTime;
        if ( stamina >= stats.MaxStamina() )
        {
            animator.Play("WakeUp");
            agent.speed = defaultNavSpeed;
            stamina = stats.MaxStamina();
            isAsleep = false;
            zzz.Stop();
            audioSource.Stop();
            PickNewState();
        }
    }

    // while wandering, lower stamina while heading to the current target
    // if stamina falls to zero, sleep
    // when destination is reached, pick new state
    void Wander ()
    {
        stamina -= 1 * Time.deltaTime;
        if ( stamina <= 0 )
            state = State.SLEEP;

        if ( Vector3.Distance(agent.transform.position, target) <= 2f )
        {
            PickNewState();
        }
    }

    // NOT FULLY IMPLEMENTED
    // while invesigating, move towards point of interest
    // lower stamina while moving
    // TODO: when point of interest is reach, do action accordingly
    void Investigate ()
    {
        if ( targetItem )
            agent.SetDestination(targetItem.transform.position);

        stamina -= 1 * Time.deltaTime;
        if ( stamina <= 0 )
            state = State.SLEEP;

        if ( (target == null || targetItem == null ) || (target != null && Vector3.Distance(agent.transform.position, targetItem.transform.position) > 15 ) )
            PickNewState();

            
        if ( targetItem != null && Vector3.Distance(agent.transform.position, targetItem.transform.position) <= stoppingDistance )
        {
            //DA: this is odd but we dont know what type of item is on the ground. but it must always be an item.
            //so call the base classes item. which digs into the items inheriting class and then uses that items value
            //to add to, for now hunger, but should work to increase stat exp or however we do it. 
            if ( targetItem.GetComponent<Item>() != null )
            {
                if ( stats.currentHunger > 90 && targetItem.tag == "Food" )
                {
                    confused.Play();
                    PickNewState();
                    return;
                }
                targetItem.GetComponent<Item>().UseItem(this.gameObject);
                agent.isStopped = true;

                if ( targetItem.tag == "Food" )
                {

                    targetItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                    agent.velocity = Vector3.zero;
                    animator.Play("Eat");
                    state = State.WAIT;
                    Debug.LogWarning("waiting to eat");
                }

                if(targetItem.tag == "Toy")
                {
                    agent.isStopped = true;
                    Invoke("PickNewState", 1f);
                    agent.isStopped = false;
                }

            }
            //EX:
            //if(targetItem.GetComponent<Toy>() != null)
            //play that toys play anim

            //state = State.IDLE;
            Debug.Log("Changing to Idle");
        }

    }

    private void Depression()
    {
        if ( stats.currentHunger >= 0 )
            stats.currentHunger -= Time.deltaTime * stats.hungerLowerRate;

        if ( stats.currentHunger < 30 && stats.happiness > 0 )
        {
            stats.happiness -= Time.deltaTime * stats.happinessLowerRate;
            sad.Play();
        }

        if ( stats.happiness > 50 )
        {
            stats.happiness -= Time.deltaTime * stats.happinessLowerRate;
        }
    }


    //eat animation event
     public void DestroyFood()
    {
        audioSource.PlayOneShot(eatSounds);
        Destroy(targetItem);
        
        agent.isStopped = false;
        StartCoroutine(SecondFoodCheck());
    }


    IEnumerator SecondFoodCheck ( )
    {
        yield return new WaitForSeconds(3.5f);
        SphereCollider sphere = GetComponent<SphereCollider>();
        float radius = sphere.radius;

        for ( int i = 0; i < 10; i++ )
        {
            sphere.radius -= radius / 10;
            yield return null;
        }
        sphere.enabled = false;
        yield return null;
        sphere.enabled = true;
        for ( int i = 0; i < 10; i++ )
        {
            sphere.radius += radius / 10;
            yield return null;
        }
        sphere.radius = radius;
        
        yield return null;
        if ( ( state != State.INVESTIGATE && state != State.WAIT ) || targetItem == null )
        {
            Invoke("PickNewState", 1f);
            Debug.LogWarning("Nothing to eat");
        }
        else
            Debug.LogWarning("FOOOODDDDSSSSS");
    }


    void Interact ()
    {
        //stop keybinding from blocking player typing name.
        player.GetComponent<KeyBindings>().InputState(false);
        //this stupid bool stops the player movement.
        player.GetComponent<KeyBindings>().isStopped = true;

        PlayCurrentEmotion();

        //stop the blobie from moving
        agent.SetDestination(transform.position);

        //camera focus blobie
        Camera.main.GetComponent<CMModifer>().BlobCameraFocus(this.transform, 20);
        //blobie look at player modified so it doesnt look upwards... 
        Vector3 playerPos = new Vector3(player.transform.position.x, 0f, player.transform.position.z);

        //transform.LookAt(playerPos);

        //show menu
        GameObject.Find("Canvas").GetComponent<InteractionMenu>().ShowBlobiesStats(this.GetComponent<AIStatSheet>());


    }

    public void PlayCurrentEmotion ()
    {
        if ( !audioSource.isPlaying )
        {
            if ( stats.happiness <= 20 )
            {
                animator.Play("Angry");
                audioSource.PlayOneShot(madSounds);
                mad.Play();

            }
            else if ( stats.happiness <= 40 )
            {
                animator.Play("Sad");
                audioSource.PlayOneShot(sadSounds);
                sad.Play();
            }
            else if ( stats.happiness <= 80 )
                audioSource.PlayOneShot(idleSounds);

            else// he happy boi
            {
                animator.Play("Happy");
                audioSource.PlayOneShot(happySounds);
                hearts.Play();
            }
        }
    }

    public void SetFetchState ( Transform toy )
    {
        state = State.FETCH;
        this.toy = toy;
    }


    //to fetch it starts in InteractionMenu/FetchButton()
    //then goes to toy Use() interactionToy side. 
    //after player throws stick comes here and does fetch then returns it to player. 
    void Fetch ()
    {
        //if we havent reached the toy go to it.
        if ( FetchToy() == false && !haveStick)
        {
            Debug.Log("in fetch toy");
            FetchToy();
        }
        if(haveStick & transform.GetChild(1).childCount != 0 )// if we have stick and there is an object in the toy ph
        {
            Debug.Log("gotoplayer");
            GoToPlayer();
        }
    }

    private void MakeTheBaby ( )
    {
        target = GameObject.FindObjectOfType<BabyMaker>().transform.position;
        agent.SetDestination(target);

        if ( Vector3.Distance(target, agent.transform.position) <= 1f)
        {
            state = State.WAIT;
        }
    }


    //if we are at player unparent toy, turn on toy physics, and reset state to idle.
    private void GoToPlayer ()
    {
        if ( Vector3.Distance(agent.transform.position, player.transform.position) <= .5f )
        {

            Debug.Log("in go to player");
            toy.parent = null;
            toy.GetComponent<Rigidbody>().isKinematic = false;
            toy.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            toy.GetComponent<BoxCollider>().isTrigger = false;
            state = State.IDLE;
            stats.happiness += toy.GetComponent<Toy>().happinessValue;
            toy.GetComponent<Toy>().ReturnToPool();
            haveStick = false;
            hearts.Play();
        }
        else
        {
            agent.isStopped = false;
            Debug.Log("setting dest to player");
            agent.SetDestination(player.transform.position);
        }
    }

    //if we are at toy parent and turn off physics interactions. else, go to toy more
    private bool FetchToy ()
    {




        if ( Vector3.Distance(agent.transform.position, toy.position) <= stickHeight && !haveStick )
        {
            agent.isStopped = true;
            animator.Play("PlayStick"); // anim event calls attachstick at right timeing
            haveStick = true;
            return true;
        }
        else
        {
            agent.SetDestination(toy.position);
            CheckIfStickCanBeGotten();

            return false;
        }

    }


    private void CheckIfStickCanBeGotten ()
    {
        if ( agent.remainingDistance > 20 )
        {
            Debug.Log("Too far away :'( ");
            toy.parent = null;
            toy.GetComponent<Rigidbody>().isKinematic = false;
            toy.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            toy.GetComponent<BoxCollider>().isTrigger = false;
            state = State.IDLE;
            toy.GetComponent<Toy>().ReturnToPool();
            haveStick = false;
            return;
        }

        //NavMeshPath path = agent.path;

        //if ( !haveStick && path.s)

        NavMeshHit navHit;

        if ( !haveStick && !NavMesh.SamplePosition(toy.transform.position, out navHit, .3f, -1) && toy.GetComponent<Rigidbody>().velocity == Vector3.zero )
        {
            Debug.Log("Cannot get the stick");
            toy.parent = null;
            toy.GetComponent<Rigidbody>().isKinematic = false;
            toy.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            toy.GetComponent<BoxCollider>().isTrigger = false;
            state = State.IDLE;
            toy.GetComponent<Toy>().ReturnToPool();
            haveStick = false;
            return;
        }


    }


    public void AttachStick()
    {

        toy.SetParent(transform.GetChild(1));
        toy.transform.position = transform.GetChild(1).transform.position;
        Debug.Log("got toy");
        toy.GetComponent<Rigidbody>().isKinematic = true;
        toy.transform.rotation = transform.root.rotation;
        toy.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        toy.GetComponent<BoxCollider>().isTrigger = true;

    }

    // picks new state depending on circumstances
    public void PickNewState ()
    {
        agent.isStopped = false;
        isPlaying = false;
        int weight = (int)( ( stamina / stats.MaxStamina() ) * 100 );
        int randomNum = Random.Range(0, 51);
        if ( stamina < 50 && randomNum > weight ) // if stamina is low this has a higher chance of making the Blobisaur sleep
        {
            state = State.SLEEP;
        }
        else
        {
            randomNum = randomNum % 3;

            if ( randomNum == 0 )
            {
                state = State.WANDER;
                SetDestination();
            }

            else if( randomNum == 1)
                state = State.PLAY;
            else       
                state = State.IDLE;
            
        }

    }
    // respond the object that cause the trigger
    private void OnTriggerEnter ( Collider other )
    {

        if ( other.tag == "Player" )
        {
            playerEntered = true;
        }

        if ( state == State.FETCH || state == State.PLAY || state == State.BABYMAKING )
            return;

        if ( PointOfInterest(other.tag) )
        {
            Debug.LogWarning("What is this?   " + other.tag);
            target = other.gameObject.transform.position;
            targetItem = other.gameObject;
            agent.SetDestination(target);
            Debug.Log("INVESTIGATE");
            state = State.INVESTIGATE;
        }


    }

    public void PlayConfusion ( )
    {
        confused.Play();
    }

    public void IsStopped(bool b)
    {
        agent.isStopped = b;
    }

    public void FallingInLove ( )
    {
        state = State.BABYMAKING;
        isInLove = true;
    }

    private void OnTriggerExit ( Collider other )
    {
        if ( other.tag == "Player" )
        {
            playerEntered = false;
        }
    }

    // will eventually contain a list of tags that denote a point of interest
    private bool PointOfInterest ( string tag )
    {
        if ( state == State.SLEEP )
            return false;
        switch ( tag )
        {
            case "Food":
                //if(stats.currentHunger > 90 )
                //{
                //    return false;
                //}
                //else
                    return true;
            case "Toy":
                return true;
        }

        return false;
    }

    // does magic
    // picks a random point from the "orgin" at with in a range of "distance"
    public Vector3 RandomNavSphere ( Vector3 origin, float distance, int layermask )
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }
}
