using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] string tutorialName;
    private bool isComplete = false;

    public bool readyToBeSeen = false;
    public bool shouldGivePlayerBackControl = true;
    public bool shouldDeactivateGameObject = true;



    protected TutorialManager manager;
    protected KeyBindings keyBindings;

    public Sprite sprite;

    CMModifer cmm;


    private AudioSource audioSource;
    public AudioClip audioClip;

    private void Awake ()
    {
        manager = FindObjectOfType<TutorialManager>();
        manager.RegisterTutorial(this);

        keyBindings = FindObjectOfType<KeyBindings>();
        cmm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CMModifer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start ( )
    {

    }

    // Update is called once per frame
    void Update ( )
    {

    }

    protected void DisplayUITutorial ( )
    {
        if ( isComplete )
        {
            gameObject.SetActive(false);
            return;
        }
        else
        {
            if ( manager.IsDisplaying() )// if a tutorial is already being displayed return and try again next time
                return;

            cmm.StopScroll();

            ChangeUserState(false);

            if ( audioSource && audioClip)
            {
                audioSource.PlayOneShot(audioClip);
            }

            manager.DisplayImage(sprite, this, true);
        }   
    }


    protected void DisplayUITutorialsWithoutFreezing ( )
    {
        if ( isComplete )
        {
            //FinishTutorial();
            gameObject.SetActive(false);
            return;
        }
        else
        {
            if ( manager.IsDisplaying() ) // if a tutorial is already being displayed return and try again next time
                return;

            cmm.StopScroll();

            if ( audioSource && audioClip )
            {
                audioSource.PlayOneShot(audioClip);
            }
            manager.DisplayImage(sprite, this, true);
        }
    }

    public void FinishTutorial ( )
    {
        isComplete = true;

        if ( shouldGivePlayerBackControl )//|| Time.timeSinceLevelLoad < 1)
            ChangeUserState(true);

        cmm.ResumeScroll();
        gameObject.SetActive(false);
    }


    ///<summary>put false to stop player movement, input, and free the mouse, put true for the opposite</summary>
    protected void ChangeUserState ( bool isInTutorial )
    {
        keyBindings.InputState(isInTutorial);
        keyBindings.isStopped = !isInTutorial;
        Utils.CursorState(isInTutorial);
        if ( isInTutorial )
        {
            cmm.FreeCamera();
        }
        else
        {
            cmm.LockCamera();
        }
    }


    public bool GetState ( )
    {
        return isComplete;
    }
    public void SetState ( bool state )
    {
        isComplete = state;

    }

    public string getTutorialName ( )
    {
        return tutorialName;
    }


    public void Disable ()
    {
        if ( shouldDeactivateGameObject )
            gameObject.SetActive(false);
        else
            this.enabled = false;
    }

}
