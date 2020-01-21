using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureManager : MonoBehaviour
{

    public List<Chest> chests = new List<Chest>();


    private string[] savedChestNames = new string[0];
    private bool[] savedChestStates = new bool[0];

    // Start is called before the first frame update
    void Start()
    {
        InitializeChests();
    }

    public void RegisterChest ( Chest chest )
    {
        chests.Add(chest);
    }



    public void LoadTreasure ( TreasureData data )
    {
        savedChestNames = new string[data.chestlNames.Length];
        savedChestStates = new bool[data.chestStates.Length];

        for ( int i = 0; i < data.chestlNames.Length; i++ )
        {
            savedChestNames[i] = data.chestlNames[i];
            savedChestStates[i] = data.chestStates[i];
        }
    }


    public void InitializeChests ()
    {
        for ( int i = 0; i < savedChestNames.Length; i++ )
        {
            for ( int j = 0; j < chests.Count; j++ )
            {

                if ( savedChestNames[i] == chests[j].GetChestName() )
                {
                    chests[j].SetState(savedChestStates[i]);
                    break;
                }
            }
        }
    }




}
