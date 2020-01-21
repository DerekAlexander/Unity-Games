using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedToy : Item
{

    public override void UseItem ( GameObject blob )
    {
        Use(transform, blob);
    }

    public void Use ( Transform t, GameObject blob )
    {
        blob.GetComponent<AIFarmBehavior>().SetFetchState(transform);
    }

}
