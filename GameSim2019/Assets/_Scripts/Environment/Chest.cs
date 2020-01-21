using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public string chestName;
    public int currencyToBeGiven;
    public ParticleSystem sparkles;

    [Tooltip("Treasure the player will get when this chest is opened. Be sure to rotate the objects in this list different" +
             " from each other so the treasure can shoot out in all directions")]
    public GameObject[] treasure;
    
    public bool hasBeenOpened = false;
    bool isPlayerHere = false;


    public float force = 5.0f;

    Animator anim;

    // Start is called before the first frame update
    void Awake ( )
    {
        anim = GetComponent<Animator>();
        FindObjectOfType<TreasureManager>().RegisterChest(this);

        for ( int i = 0; i < treasure.Length; i++ )
        {
            treasure[i].GetComponent<Rigidbody>().isKinematic = true;
            treasure[i].SetActive(false); // deactivate treasure so player can't pick it up till chest is open
            // and so if the chest has already been open player can't pick it up again
        }

    }

    // Update is called once per frame
    void Update ( )
    {
        if ( isPlayerHere && Input.GetKeyDown(KeyCode.E) && !hasBeenOpened )
        {
            anim.SetBool("Open", true);
            hasBeenOpened = true;
            
        }
    }


    public void ActivateTreasure ( )
    {
        sparkles.Play();
        GameObject.FindGameObjectWithTag("Player").GetComponent<ItemInventory>().AddCurrency(currencyToBeGiven);
        for ( int i = 0; i < treasure.Length; i++ )
        {
            treasure[i].SetActive(true); // activate treasure once chest is being opened
        }
    }

    public void ShootTreasure ( )
    {
        for ( int i = 0; i < treasure.Length; i++ )
        {
            treasure[i].GetComponent<Rigidbody>().isKinematic = false;
            treasure[i].GetComponent<Rigidbody>().AddForce(Vector3.up * force ); // Shoots treasure out of chest
        }
    }


    private void OnTriggerEnter ( Collider other )
    {
        if ( other.tag == "Player" )
        {
            isPlayerHere = true;
        }
    }

    private void OnTriggerExit ( Collider other )
    {
        if ( other.tag == "Player" )
        {
            isPlayerHere = false;
        }
    }



    public void SetState ( bool state )
    {
        hasBeenOpened = state;
        
        if ( hasBeenOpened )
        {
            anim.Play("ChestStayOpen");
        }
    }


    public string GetChestName ( )
    {
        return chestName;
    }
    public bool GetState ( )
    {
        return hasBeenOpened;
    }

}
