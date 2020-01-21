using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggItem : Item
{
    public BabyMaker babyMaker;
    public char speed, glide, power;
    // Use this for initialization
    void Start ()
    {
        tag = "Egg";

    }

    public override void UseItem ( GameObject obj )
    {
        Use(obj);
    }

    public void Use ( GameObject obj )
    {
        babyMaker = FindObjectOfType<BabyMaker>();

        if ( babyMaker.SpawnBaby(speed, glide, power) == 0 )
        {
            Debug.Log("bought egg");

        }



        else
        {
        }
    }
}
