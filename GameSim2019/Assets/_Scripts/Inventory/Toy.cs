using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toy : Item
{
    public int happinessValue;
    private float savedTime;
    private bool readyToThrow;
    private GameObject currentBlob;
    private GameObject pool;

    private void Awake ()
    {

    }

    // Use this for initialization
    void Start ()
    {
        if ( tag == "InteractionToy" )
        {
            pool = GameObject.Find("pool");
            transform.SetParent(pool.transform);
            transform.position = pool.transform.position;
        }

        savedTime = -60;
        readyToThrow = false;

    }

    // Update is called once per frame
    void Update ()
    {

        //if we have a stick this is true.
        if ( readyToThrow )
        {

            //if the player clicks it throws the stick foward. 
            if ( Input.GetMouseButton(0) )
            {
                FindObjectOfType<Movement>().PlayAnimation("Throw");

            }
        }
    }

    public bool ReadyToThrow()
    {
        return readyToThrow;
    }


    public void ThrowToy ( Vector3 newForward )
    {
        //play throw animation
        //event in animation detaches object 
        transform.parent = null;

        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<BoxCollider>().isTrigger = false;
        
        GetComponent<Rigidbody>().AddForce(newForward * 5, ForceMode.Impulse);

        currentBlob.GetComponent<AIFarmBehavior>().SetFetchState(transform);

        GameObject.Find("Player").GetComponent<KeyBindings>().InputState(true);

        readyToThrow = false;
    }


    public override void UseItem ( GameObject blob )
    {

        if ( ( Time.time - savedTime ) > 60 )
            Use(blob);

    }

    private void Use ( GameObject blob )
    {
        currentBlob = blob;

        //if its a toy that can only be used through the interaction menu
        if ( tag == "InteractionToy" )
        {
            //find the attach point on player
            GameObject holder = GameObject.FindGameObjectWithTag("Fetch");

            //make it not fight with the players body.
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<BoxCollider>().isTrigger = true;

            //attach toy to player attach point
            transform.SetParent(holder.transform);
            transform.position = transform.parent.position;

            //this pushes the function into update where there is constant checking for input.
            readyToThrow = true;
        }

        //a normal toy
        else
        {
            currentBlob.GetComponent<AIStatSheet>().happiness += happinessValue;
            currentBlob.transform.GetChild(0).GetComponent<Animator>().Play(GetName() + "creature");
            savedTime = Time.time;
        }

    }

    public void KickBall(Vector3 foward)
    {
        GetComponent<Rigidbody>().AddForce(foward * 1000);

    }

    public void ReturnToPool()
    {
        Invoke("DelayPoolReturn", 2f);
    }
    private void DelayPoolReturn()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        transform.SetParent(pool.transform);
        transform.position = pool.transform.position;
    }

}
