using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Item
{

    public int foodValue;

    public bool inInventory = false;

    [Tooltip("how long food stays in world before disappearing.")]
    public Vector2 decayTimer;

    private float finalDecay;

    [Tooltip("increase speed base stat. can be positive, negative, or 0 for no change.")]
    public float speed = 0;

    [Tooltip("increase power base stat. can be positive, negative, or 0 for no change.")]
    public float power = 0;

    [Tooltip("increase glide base stat. can be positive, negative, or 0 for no change.")]
    public float glide = 0;


    // Use this for initialization
    void Start ()
    {
        tag = "Food";
        ////time to decay between 1 and 5 minutes
        finalDecay = Random.Range(decayTimer.x, decayTimer.y);

    }

    private void Awake ()
    {
        InPool();
    }
    // Update is called once per frame
    void Update ()
    {
        if(!inInventory)
        {
            finalDecay -= Time.deltaTime;
            if(finalDecay < 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void InPool()
    {
        if ( transform.parent && transform.parent.name == "pool" )
        {
            inInventory = true;
        }
        else
        {
            inInventory = false;
        }
    }

    public override void UseItem ( GameObject obj )
    {
        Use(obj);
    }

    //TODO: clean up if else hell
    public void Use ( GameObject obj )
    {

        if ( GetId() == 343 )
        {

            string str = obj.GetComponent<AIStatSheet>().GetBodyName();

            if ( str == "Base" || obj.GetComponent<AIStatSheet>().daysTillNextBaby > 0 )
                return;

            obj.GetComponent<AIFarmBehavior>().wantsBaby = true;


            inInventory = false;
            return;
        }


        inInventory = false;

        float maxSpeed, maxGlide, maxPower;
        float currentSpeed, currentGlide, currentPower;

        maxGlide = obj.GetComponent<AIStatSheet>().MaxGlide();
        maxSpeed = obj.GetComponent<AIStatSheet>().MaxSpeed();
        maxPower = obj.GetComponent<AIStatSheet>().MaxPower();

        currentSpeed = obj.GetComponent<AIStatSheet>().speed;
        currentPower = obj.GetComponent<AIStatSheet>().power;
        currentGlide = obj.GetComponent<AIStatSheet>().glide;


        //add to hunger
        obj.GetComponent<AIStatSheet>().currentHunger += foodValue;


        //from here down check if current is less than max if so add. else set current to max.
        if ( currentSpeed < maxSpeed )
        {

            if ( currentSpeed + speed < maxSpeed )
            {
                currentSpeed += speed;
            }
            else
            {
                currentSpeed = maxSpeed;
            }
            obj.GetComponent<AIStatSheet>().speed = currentSpeed;
        }

        if ( currentPower < maxPower )
        {

            if ( currentPower + power < maxPower )
            {
                currentPower += power;
            }
            else
            {
                currentPower = maxPower;
            }
            obj.GetComponent<AIStatSheet>().power = currentPower;
        }

        if ( currentGlide < maxGlide )
        {

            if ( currentGlide + glide < maxGlide )
            {
                currentGlide += glide;
            }
            else
            {
                currentGlide = maxGlide;
            }

            obj.GetComponent<AIStatSheet>().glide = currentGlide;
        }

        obj.GetComponent<AIStatSheet>().ApplyNewStats();

    }


}
