using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Journal : MonoBehaviour
{


    public GameObject pageOne;
    public GameObject pauseScren;

    public string lookAtCreditsText = "Press E to read Grandpa's Journal";


    bool isPlayerHere = false;
    bool showingJournal = false;


    // Start is called before the first frame update
    void Start ()
    {

    }



    public void ActivateJournal ()
    {
        Utils.CursorState(false);
        pageOne.SetActive(true);
        FindObjectOfType<KeyBindings>().InputState(false);
        FindObjectOfType<MovementHouse>().InputState(false);
    }

    public void DeactivateJournal ()
    {
        Utils.CursorState(true);
        FindObjectOfType<KeyBindings>().InputState(true);
        FindObjectOfType<MovementHouse>().InputState(true);
    }


    // Update is called once per frame
    void Update ()
    {
        if ( Input.GetKeyDown(KeyCode.E) && isPlayerHere && !showingJournal && !pauseScren.activeSelf )
        {
            ActivateJournal();
        }
    }


    private void OnTriggerEnter ( Collider other )
    {
        if ( other.tag == "Player" )
        {
            FindObjectOfType<HelperText>().DisplayTest(lookAtCreditsText);
            isPlayerHere = true;
        }
    }

    private void OnTriggerExit ( Collider other )
    {
        if ( other.tag == "Player" )
        {
            FindObjectOfType<HelperText>().DisableText();
            isPlayerHere = false;
        }
    }

}
