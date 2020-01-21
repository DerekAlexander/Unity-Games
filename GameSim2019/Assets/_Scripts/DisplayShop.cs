using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayShop : MonoBehaviour
{
    private bool isOn = false;
    public GameObject shopUi;
    private CMModifer cmm;
    // Start is called before the first frame update
    void Start()
    {
        cmm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CMModifer>();
//#if UNITY_EDITOR

//        StartCoroutine(Utils.Rave());
//#endif
    }

    // Update is called once per frame
    void Update()
    {
        if ( isOn && Input.GetKeyDown(KeyCode.E) && shopUi.activeSelf )
        {
            Utils.CursorState(true);
            cmm.ResumeScroll();
            cmm.FreeCamera();
            shopUi.SetActive(false);
            FindObjectOfType<HelperText>().DisplayTest("Press E to open the shop!");
        }
        else if (isOn && Input.GetKeyDown(KeyCode.E) && !shopUi.activeSelf  )
        {
            Utils.CursorState(false);
            cmm.StopScroll();
            cmm.LockCamera();
            shopUi.SetActive(true);
            FindObjectOfType<HelperText>().DisplayTest("Open you inventory and click to sell");
        }

    }

    private void OnTriggerEnter ( Collider other )
    {
        if ( other.tag == "Player" )
        {
            isOn = true;
            FindObjectOfType<HelperText>().DisplayTest("Press E to open the shop!");
        }
    }

    private void OnTriggerExit ( Collider other )
    {
        if ( other.tag == "Player" )
        {
            isOn = false;
            Utils.CursorState(true);
            cmm.ResumeScroll();
            cmm.FreeCamera();
            shopUi.SetActive(false);
            FindObjectOfType<HelperText>().DisableText();
        }
    }
}
