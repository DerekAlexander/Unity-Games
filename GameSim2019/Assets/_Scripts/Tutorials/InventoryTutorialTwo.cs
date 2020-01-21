using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTutorialTwo : Tutorial
{

    public GameObject initialTutorial;
    public GameObject inventoryUi;

    private bool hasDisplayed = false;

    // Update is called once per frame
    void Update ( )
    {
        if ( !initialTutorial.activeSelf && !hasDisplayed && inventoryUi.activeSelf )
        {
            DisplayUITutorialsWithoutFreezing();
            hasDisplayed = true;
        }
        if ( ( Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab) ) && hasDisplayed )
        {
            FindObjectOfType<TutorialManager>().ButtonResponse();
        }
    }
}
