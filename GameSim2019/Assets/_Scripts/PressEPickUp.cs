using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressEPickUp : MonoBehaviour
{
    public GameObject pressE;
    private int counter = 0;
    public GameObject fruitTut;
    public SphereCollider trigger;
    // Start is called before the first frame update
    void Start ()
    {
        if(!fruitTut.activeSelf)
        {
            trigger.enabled = true;
        }
        else
        {
            counter--;
        }
        
    }

    // Update is called once per frame
    void Update ()
    {
        //if(counter > 0)
        //{
        //    if(Input.GetKeyDown(KeyCode.E))
        //    {
        //        counter--;
        //        CheckCount();
        //    }
        //}
    }

    private void OnTriggerEnter ( Collider other )
    {
        if ( other.tag == "Food" )
        {
            trigger.enabled = true;           
            counter++;
            Debug.Log("enter " + counter);
            pressE.SetActive(true);
        }
    }

    private void OnTriggerExit ( Collider other )
    {
        if ( other.tag == "Food" )
        {

            Debug.Log("exit " + counter);
            counter--;
            CheckCount();

        }
    }


    private void CheckCount()
    {
        if ( counter == 0 )
        {
            pressE.SetActive(false);
        }
    }
}
