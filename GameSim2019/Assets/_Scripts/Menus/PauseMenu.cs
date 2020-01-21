using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private GameObject paused;


    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject controlsMenu;

    private void Awake ()
    {
        Time.timeScale = 1;
        if ( GameObject.Find("Paused") )
        {
            paused = GameObject.Find("Paused");
        }
    }

    public void UnPause ()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(true);
        controlsMenu.SetActive(false);
        paused.SetActive(false);
        Utils.CursorState(true);
        //Camera.main.GetComponent<ThirdPersonCamera>().enabled = true;
        Debug.Log("unpausing");
    }


    public void Save ( )
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<ItemInventory>().SaveInventory();

        GameObject sceneObj = GameObject.Find("FarmManager");

        if ( sceneObj )
        {
            sceneObj.GetComponent<FarmManager>().SaveTheBlobies();
        }
        else
        {
            Debug.LogError("Could not find FarmManager");
            // check to see if in the house and save that here
        }

    }


    public void LoadMenu ( )
    {
        FindObjectOfType<SceneController>().LoadSceneWithoutSaving("MainMenu");
        FindObjectOfType<MusicController>().ChangeToMenuMusic();
    }


}
