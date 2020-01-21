using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBindings : MonoBehaviour
{

    public GameObject paused;
    public GameObject pausedMenu;
    public GameObject controlsMenu;
    public GameObject InventoryPanel;
    public CMModifer cmm;
    public bool isStopped;
    private bool myInput = true;

    // Use this for initialization
    void Awake ()
    {
        cmm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CMModifer>();
    }

    // Update is called once per frame
    void Update ()
    {
        if ( myInput )
        {
            InputKeys();
        }
    }

    private void InputKeys()
    {
        if ( Input.GetKeyDown(KeyCode.Escape) && !InventoryPanel.activeSelf )
        {
            controlsMenu.SetActive(false);
            pausedMenu.SetActive(true);
            paused.SetActive(!paused.activeSelf);
            
            Pause();

        }

        if ( ( Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab) ) && SceneController.ActiveSceneName() != "HouseInterior" && paused.activeSelf == false )
        {
            InventoryPanel.SetActive(!InventoryPanel.activeSelf);
            if ( InventoryPanel.activeSelf )
                GetComponent<Animator>().Play("CheckInventory");
            else
                GetComponent<Animator>().Play("Blend Tree");
            //StartCoroutine(Utils.Rave());

            Inventory();
        }

    }
    public void InputState(bool b)
    {
        myInput = b;
    }

    void Pause ()
    {

        if ( paused.activeSelf == true && InventoryPanel.activeSelf == true )
        {
            Utils.CursorState(false);
            InventoryPanel.SetActive(false);
            if ( SceneController.ActiveSceneName() != "HouseInterior")
                cmm.FreeCamera();
            isStopped = ! isStopped;
            Time.timeScale = 0;
        }
        else if( paused.activeSelf == true )
        {

            Utils.CursorState(false);
            Time.timeScale = 0;

        }
        else
        {
            Utils.CursorState(true);
            Time.timeScale = 1;

        }


    }
    void Inventory ()
    {
        if ( InventoryPanel.activeSelf == true )
        {
            if ( SceneController.ActiveSceneName() != "HouseInterior" )
                cmm.LockCamera();
            Utils.CursorState(false);
            isStopped = true;
        }
        else
        {
            if ( SceneController.ActiveSceneName() != "HouseInterior" )
                cmm.FreeCamera();
            Utils.CursorState(true);
            isStopped = false;
        }


    }

}
