using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class CompeitionCameras : MonoBehaviour
{
    public CinemachineVirtualCamera[] competitionCameras;
    public GameObject[] blobies = new GameObject[10];

    public CinemachineFreeLook playerBlobieFocus;

    private SelectionManager selectionManager;

    public CinemachineBrain cmmBrain;
    public Text cameraNum;
    public Text creatureNameText;

    private string cameraName;
    private string creatureName;

    //-------------------------------------------

    // big DISCLAIMER! this function call is always for the previous camera even though it says active
    // cmmBrain.ActiveVirtualCamera.Name
    //this means having to update the naming of cameras and creatures else where and causes what seems to be unnecessary repitition. 

    //--------------------------------------------


    // Start is called before the first frame update
    void Start ()
    {

        selectionManager = FindObjectOfType<SelectionManager>();

        playerBlobieFocus.m_LookAt = selectionManager.ChosenOne().transform;
        playerBlobieFocus.m_Follow = selectionManager.ChosenOne().transform;
        competitionCameras[0].m_LookAt = selectionManager.ChosenOne().transform;

        //i starts at 1 here to skip player cam
        for(int i = 1; i < competitionCameras.Length; i++ )
        {
            competitionCameras[i].m_LookAt = 
                blobies[i].transform;
        }

        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart ()
    {
        for ( int i = 0; i < 10; i++ )
        {
            cameraName = "Player Monster Cam";
            creatureNameText.text = playerBlobieFocus.LookAt.GetComponent<AIStatSheet>().myName;
            SetCameraName();
            yield return new WaitForSeconds(1);
        }

    }

    // Update is called once per frame
    void Update ()
    {


        ////DA i realize this is super jank. but putting these gets in awake or start causes nulls. like the active camera isnt active until 2 frames go by. not 1 of course.



        //this be a bit costly.
        MatchCameraSpeeds();

        //if left arrow go to the camera left of the current active.
        if ( Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) )
        {
            
            playerBlobieFocus.m_Priority = 0; //encase we are at freelookcam set to 0.

            PlayerSwitchCameraLeft(); //move to the camera to the left of current viewed one


            //Debug.Log(cmmBrain.ActiveVirtualCamera.Name + " this is name after playerswitchleft call"); //DA: somehow this is always the previous one. no matter when u call it. what?

            SetCameraName();
        }

        //if right arrow go to the camera left of the current active.
        if ( Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) )
        {
            
            playerBlobieFocus.m_Priority = 0; //encase we are at freelookcam set to 0.

            PlayerSwitchCameraRight(); //move to the camera to the right of current viewed one

            Debug.Log(cmmBrain.ActiveVirtualCamera.Name + " this is name after playerswitchright call"); //DA: somehow this is always the previous one. no matter when u call it. what?

            SetCameraName();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerBlobieFocus.m_Priority = 20;
            cameraName = "Player Monster Cam";
            SetCameraName();
            SetCreatureName(playerBlobieFocus);
        }

    }

    public void PlayerSwitchCameraLeft ()
    {

        switch ( cmmBrain.ActiveVirtualCamera.Name )
        {
            case "playerBlobCam":
                competitionCameras[0].m_Priority = 10; //camera we are going to

                cameraName = competitionCameras[0].name;
                SetCreatureName(competitionCameras[0]);
                
                playerBlobieFocus.m_Priority = 0; //camera we are leaving
                break;

            case "cam 0":
                competitionCameras[5].m_Priority = 10; //camera we are going to
                cameraName = competitionCameras[5].name;
                SetCreatureName(competitionCameras[5]);

                competitionCameras[0].m_Priority = 0; //camera we are leaving
                break;

            case "cam 1":
                competitionCameras[0].m_Priority = 10; //camera we are going to
                cameraName = competitionCameras[0].name;
                SetCreatureName(competitionCameras[0]);
               
                competitionCameras[1].m_Priority = 0; //camera we are leaving
                break;

            case "cam 2":
                competitionCameras[1].m_Priority = 10; //camera we are going to
                cameraName = competitionCameras[1].name;
                SetCreatureName(competitionCameras[1]);
               
                competitionCameras[2].m_Priority = 0; //camera we are leaving
                break;

            case "cam 3":
                competitionCameras[2].m_Priority = 10; //camera we are going to
                cameraName = competitionCameras[2].name;
                SetCreatureName(competitionCameras[2]);

                competitionCameras[3].m_Priority = 0; //camera we are leaving
                break;

            case "cam 4":
                competitionCameras[3].m_Priority = 10; //camera we are going to
                cameraName = competitionCameras[3].name;
                SetCreatureName(competitionCameras[3]);
               
                competitionCameras[2].m_Priority = 0; //camera we are leaving
                break;

            case "cam 5":
                competitionCameras[4].m_Priority = 10; //camera we are going to
                cameraName = competitionCameras[4].name;
                SetCreatureName(competitionCameras[4]);

                competitionCameras[3].m_Priority = 0; //camera we are leaving
                break;

        }
        return;

    }

    public void PlayerSwitchCameraRight ()
    {

        switch ( cmmBrain.ActiveVirtualCamera.Name )
        {
            case "playerBlobCam":
                competitionCameras[0].m_Priority = 10; //camera we are going to
                cameraName = competitionCameras[0].name;
                SetCreatureName(competitionCameras[0]);

                playerBlobieFocus.m_Priority = 0; //camera we are leaving
                break;

            case "cam 0":
                competitionCameras[1].m_Priority = 10; //camera we are going to
                cameraName = competitionCameras[1].name;
                SetCreatureName(competitionCameras[1]);

                competitionCameras[0].m_Priority = 0; //camera we are leaving
                break;

            case "cam 1":
                competitionCameras[2].m_Priority = 10; //camera we are going to
                cameraName = competitionCameras[2].name;
                SetCreatureName(competitionCameras[2]);
               
                competitionCameras[1].m_Priority = 0; //camera we are leaving
                break;

            case "cam 2":
                competitionCameras[3].m_Priority = 10; //camera we are going to
                cameraName = competitionCameras[3].name;
                SetCreatureName(competitionCameras[3]);
              
                competitionCameras[2].m_Priority = 0; //camera we are leaving
                break;

            case "cam 3":
                competitionCameras[4].m_Priority = 10; //camera we are going to
                cameraName = competitionCameras[4].name;
                SetCreatureName(competitionCameras[4]);

                competitionCameras[3].m_Priority = 0; //camera we are leaving
                break;

            case "cam 4":
                competitionCameras[5].m_Priority = 10; //camera we are going to
                cameraName = competitionCameras[5].name;
                SetCreatureName(competitionCameras[5]);

                competitionCameras[4].m_Priority = 0; //camera we are leaving
                break;
            case "cam 5":
                competitionCameras[0].m_Priority = 10; //camera we are going to
                cameraName = competitionCameras[0].name;
                SetCreatureName(competitionCameras[0]);

                competitionCameras[5].m_Priority = 0; //camera we are leaving
                break;

        }

        return;
    }

    private void SetCameraName ()
    {
        cameraNum.text = cameraName;
    }
    
    //get any cinemachine camera and get the creature name of the creature its looking at
    private void SetCreatureName (ICinemachineCamera camera)
    {
        creatureNameText.text = camera.LookAt.gameObject.GetComponent<AIStatSheet>().myName; 
    }

    //gets the nav speed from the currently looked at ai and sets it speed to it... since this value can change alot have to do it in update.
    // because of this though the camera always match the ai speed perfectly.
    private void MatchCameraSpeeds ()
    {
        for ( int i = 0; i < competitionCameras.Length; i++ )
        {
            competitionCameras[i].m_Follow.gameObject.GetComponent<CinemachineDollyCart>().m_Speed =
            competitionCameras[i].m_LookAt.gameObject.GetComponent<AIStatSheet>().NavVelocity();
        }
    }
}
