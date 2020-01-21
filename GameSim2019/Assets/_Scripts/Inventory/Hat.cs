using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hat : Item
{

    // Use this for initialization
    void Start ()
    {
        tag = "Hat";
    }

    // Update is called once per frame
    void Update ()
    {

    }

    public void Use (GameObject obj)
    {
        UseItem( obj );
    }

    public override void UseItem (GameObject obj)
    {
    }

}
