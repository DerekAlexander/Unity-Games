using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PressEPopUpChest : MonoBehaviour
{
    public GameObject PressE;
    public GameObject triggerBox;
    private Chest chest;
    private bool isInTrigger = false;
    // Start is called before the first frame update
    void Start()
    {
        chest = GameObject.Find("Chest_Final").GetComponent<Chest>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( isInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            PressE.SetActive(false);
            triggerBox.SetActive(false);
        }
    }

    private void OnTriggerEnter ( Collider other )
    {
        if ( other.tag == "Player" && chest.hasBeenOpened == false )
        {
            isInTrigger = true;
           // PressE.SetActive(true);
        }
    }

    private void OnTriggerExit ( Collider other )
    {
        if ( other.tag == "Player" )
        {
            isInTrigger = false;
           // PressE.SetActive(false);
        }
    }
}
