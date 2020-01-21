using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressEToShowTutorial : MonoBehaviour
{
    public GameObject tutorial;
    public GameObject PressE;
    public GameObject triggerBox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PressE.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            tutorial.SetActive(!tutorial.activeSelf);
            PressE.SetActive(false);
        }
    }

    private void OnTriggerEnter ( Collider other )
    {
        if ( other.tag == "Player" )
        {
            PressE.SetActive(true);
        }
    }

    private void OnTriggerExit ( Collider other )
    {
        if ( other.tag == "Player" )
        {
            tutorial.SetActive(false);
            PressE.SetActive(false);
        }
    }
}
