using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject playMenu;
    public GameObject warningMenu;
    private bool existingFile = false;

    public string sceneToLoad;


    // Use this for initialization
    void Awake ()
    {
        if ( InventorySaving.SaveFileCheck() )
        {
            existingFile = true;
        }

        //timescale is here for when playing game -> go to menu -> start -> then cant move. without this game stays 0 time scale.
        Time.timeScale = 1;

    }


    // Update is called once per frame
    void Update ()
    {

    }


    public void Play()
    {
        if(!existingFile)
        {
            FindObjectOfType<MusicController>().ChangeToIslandMusic();
            FindObjectOfType<SceneController>().LoadScene(sceneToLoad);
        }
        else
        {
            playMenu.SetActive(true);
            mainMenu.SetActive(false);
        }
    }

    public void LoadButtonClicked ()
    {
        if ( InventorySaving.SaveFileCheck() )
        {

            string scene = InventorySaving.LoadInventory().sceneName;
            string musicState = InventorySaving.LoadInventory().musicState;
            FindObjectOfType<MusicController>().ChangeToMusic(musicState);
            FindObjectOfType<SceneController>().LoadScene(scene);
        }

        else
        {
        }
        //error here: no save found;
    }

    public void NewGameButtonClicked ()
    {
        //if a save file exist.. are you sure u want a new game and delete the old one???
        if ( InventorySaving.SaveFileCheck() )
            warningMenu.SetActive(true);

        else
        {
            FindObjectOfType<MusicController>().ChangeToIslandMusic();
            FindObjectOfType<SceneController>().LoadScene(sceneToLoad);
        }
    }

    public void DeleteSaveButtonClicked ()
    {
        InventorySaving.DeleteSaveFile();
        FarmSaving.DeleteSaveFile();
        FindObjectOfType<MusicController>().ChangeToIslandMusic();
        FindObjectOfType<SceneController>().LoadScene(sceneToLoad);
    }

    public void Exit ()
    {
        Application.Quit();
    }

}
