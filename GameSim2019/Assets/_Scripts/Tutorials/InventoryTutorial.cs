using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTutorial : Tutorial
{
    private bool isOpen = false;
    public GameObject inventoryUi;
    // Update is called once per frame
    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab) )
        {
            StartCoroutine(WaitCheck());
        }

    }


    IEnumerator WaitCheck ( )
    {
        yield return null;
        if (  isOpen )
        {
            FindObjectOfType<TutorialManager>().ButtonResponse();
        }
        else if (  !isOpen && inventoryUi.activeSelf == true )
        {
            isOpen = true;
            DisplayUITutorialsWithoutFreezing();
        }
    }

}
