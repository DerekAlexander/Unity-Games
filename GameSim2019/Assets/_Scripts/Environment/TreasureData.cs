using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TreasureData 
{
    public string[] chestlNames;
    public bool[] chestStates;


    public TreasureData ( TreasureManager manager )
    {
        chestlNames = new string[manager.chests.Count];
        chestStates = new bool[chestlNames.Length];

        for ( int i = 0; i < manager.chests.Count; i++ )
        {
            chestlNames[i] = manager.chests[i].GetChestName();
            chestStates[i] = manager.chests[i].GetState();
        }

    }



}
