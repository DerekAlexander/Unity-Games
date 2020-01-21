using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneController : MonoBehaviour
{

    private string lastScene;
    public string currentScene;

    [SerializeField] string loadSceneName = "Loading_Scene";

    Slider loadingBar;


    bool canLoad = false;

    private void Awake ( )
    {

        int objs = FindObjectsOfType<SceneController>().Length;
        if ( objs > 1 )
            Destroy(gameObject); // makes this a singleton


        DontDestroyOnLoad(this);

        //TODO: Fix this once we have a working splash screen
        if ( SceneManager.GetActiveScene().name == "Splash_Screen")
            SceneManager.LoadScene("MainMenu");

        currentScene = SceneManager.GetActiveScene().name;
    }

    public void LoadScene ( string sceneName )
    {
        SaveScene();

        currentScene = sceneName; // set the current scene to the scene about to be laoded

        if ( SceneManager.GetActiveScene().name != loadSceneName) // if current scene is not the load scene
            lastScene = SceneManager.GetActiveScene().name; // save the name of the last scene to the current scene before loading
        

        StartCoroutine(LoadAsynchronously(sceneName));
    }


    public void LoadSceneWithoutSaving ( string sceneName )
    {
        currentScene = sceneName; // set the current scene to the scene about to be laoded

        if ( SceneManager.GetActiveScene().name != loadSceneName ) // if current scene is not the load scene
            lastScene = SceneManager.GetActiveScene().name; // save the name of the last scene to the current scene before loading
        

        StartCoroutine(LoadAsynchronously(sceneName));
    }

    private void SaveScene ()
    {

        switch ( currentScene )
        {
            case "TestLayout":
                FindObjectOfType<FarmManager>().SaveTheBlobies();
                FindObjectOfType<ItemInventory>().SaveInventory();
                break;

            case "HouseInterior":
                FindObjectOfType<ItemInventory>().SaveInventory();
                break;

            case "Competition":

                break;

            default:
                // in MainMenu. Do nothing.
                break;
        }
    }

    IEnumerator LoadAsynchronously ( string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(loadSceneName); // load into the LoadingScene

        while ( !operation.isDone )
        {
            yield return null;
        }

        //yield return new WaitForSeconds(2);

        loadingBar = FindObjectOfType<Slider>(); // grab loading bar from the LoadingScene

        operation = SceneManager.LoadSceneAsync(sceneName); // load into the new Scene
        operation.allowSceneActivation = false;

        GameObject button = GameObject.Find("LoadLevel");
        button.SetActive(false);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f); // Do math to calculate how much has been loaded
            loadingBar.value = progress; // update the LoadingBar

            if ( progress == 1 )
                button.SetActive(true);

            if ( canLoad )
            {
                operation.allowSceneActivation = true;
                canLoad = false;
            }

            yield return null;
        }

    }

    public void LetLoad ( )
    {
        canLoad = true;
    }

    public bool PlacePlayer ( GameObject player )
    {
        switch ( lastScene )
        {
            case "Island":
                if ( currentScene == "House")
                {

                }
                return true;

            case "House":


                return true;

            case "Competition":


                return true;

            default:
                // in MainMenu. Do nothing.
                return false;
        }
    }


    public static string ActiveSceneName ( )
    {
        return SceneManager.GetActiveScene().name;
    }

}
