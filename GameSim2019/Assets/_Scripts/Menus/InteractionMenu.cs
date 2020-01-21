using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class InteractionMenu : MonoBehaviour
{
    private float speed, power, glide;
    private float vertical;
    public bool isInteracting = false;
    public GameObject interactionUI;
    private GameObject currentBlobie;
    private CMModifer cmm;
    private Player player;
    public Text textSpeed, textPower, textGlide;
    public Text stickPrompt;
    public GameObject sad, netural, happy, angry;
    public Toggle breeding;
    public InputField nameInput;
    private Animator animator;
    public ParticleSystem psPuffRightHand;

    //called in Name ui element. 

    private void Start ()
    {
        cmm = Camera.main.GetComponent<CMModifer>();
        if ( SceneController.ActiveSceneName() != "PreCompetition" )
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void SaveName ()
    {
        currentBlobie.GetComponent<AIStatSheet>().myName = nameInput.text;
    }

    public void UpdateNameOnOpen ( string myName )
    {
        nameInput.text = myName;
        isInteracting = true;
    }

    public void ShowBlobiesStatsPreComp( AIStatSheet aiStats )
    {

        currentBlobie = aiStats.gameObject;

        nameInput.text = aiStats.myName;

        HappinessState(aiStats);

        speed = aiStats.adjustedSpeed;
        power = aiStats.adjustedPower;
        glide = aiStats.adjustedGlide;

        textSpeed.text = aiStats.SpeedGrade() + ":   " + (int)speed;
        textPower.text = aiStats.PowerGrade() + ":   " + (int)power;
        textGlide.text = aiStats.GlideGrade() + ":   " + (int)glide;

    }

    //TODO: Optimize this crap
    public void ShowBlobiesStats ( AIStatSheet aiStats )
    {
       
        //breeding.gameObject.SetActive(aiStats.ReadyToBreed());
        //breeding.isOn = aiStats.GetComponent<AIFarmBehavior>().wantsBaby;
        currentBlobie = aiStats.gameObject;
        animator = currentBlobie.GetComponentInChildren<Animator>();

        if (Input.GetMouseButton(1))
        {
            cmm.FreeCamera();
        }
        else
        {
            cmm.LockCamera();
        }


        interactionUI.SetActive(true);
        Utils.CursorState(false);

        HappinessState(aiStats);

        speed = aiStats.speed;
        power = aiStats.power;
        glide = aiStats.glide;

        textSpeed.text = aiStats.SpeedGrade() + ":   " + (int)speed;
        textPower.text = aiStats.PowerGrade() + ":   " + (int)power;
        textGlide.text = aiStats.GlideGrade() + ":   " + (int)glide;

        //short circuit if the player hits enter when inputting a name... cause that makes sense to do :)
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CloseMenu();
        }

    }

    public void SetBabyState ( bool state)
    {
        if ( currentBlobie )
        {
            currentBlobie.GetComponent<AIFarmBehavior>().wantsBaby = state;
        }
    }


    //start of fetch interaction clicked in interaction menu
    public void FetchButton(string name)
    {
        Toy[] toys = FindObjectsOfType<Toy>();

        for ( int i = 0; i < toys.Length; i++ )
        {
            Debug.Log("in fetch button " + toys[i].GetName() + " " + name);
            if ( toys[i].transform.position == Vector3.zero && toys[i].GetName() == name )
            {
                toys[i].UseItem(currentBlobie);
                break;
            }
        }
        
        CloseMenu();
        currentBlobie.GetComponent<AIFarmBehavior>().state = AIFarmBehavior.State.WAIT;
        //player.GetComponent<KeyBindings>().InputState(false); // DA: why was this here??? caused the player not to be able to turn to throw...
        player.GetComponent<Animator>().SetBool("HoldingStick", true);
        psPuffRightHand.Play();

        cmm.BlobCameraFocus(player.transform, 0); //refocus the camera onto the player. 

    }

    public void CloseMenu ()
    {
        //lock mouse again
        Utils.CursorState(true);

        //reset blobie state
        currentBlobie.GetComponent<AIFarmBehavior>().PickNewState();

        //reset player inputs and movement
        player.GetComponent<KeyBindings>().InputState(true);
        player.GetComponent<KeyBindings>().isStopped = false;

        GameObject.Find("Canvas").GetComponent<InteractionMenu>().interactionUI.SetActive(false);

        //refocuses the camera on the player
        cmm.BlobCameraFocus(player.transform , 0);
        isInteracting = false;

    }

    private void HappinessState(AIStatSheet stats)
    {
        if ( stats.happiness <= 20 )
        {
            animator.Play("Angry");
            //change image 
            sad.SetActive(false);
            angry.SetActive(true);
            netural.SetActive(false);
            happy.SetActive(false);

        }
        else if ( stats.happiness <= 40 && stats.happiness > 20 )
        {
            animator.Play("Sad");
            //change image 
            sad.SetActive(true);
            angry.SetActive(false);
            netural.SetActive(false);
            happy.SetActive(false);
        }
        else if ( stats.happiness <= 80 && stats.happiness > 40 )
        {
            
            //change image 
            sad.SetActive(false);
            angry.SetActive(false);
            netural.SetActive(true);
            happy.SetActive(false);
        }

        else if ( stats.happiness > 80 )
        {
            animator.Play("Happy");
            //change image 
            sad.SetActive(false);
            angry.SetActive(false);
            netural.SetActive(false);
            happy.SetActive(true);
        }
    }


}
