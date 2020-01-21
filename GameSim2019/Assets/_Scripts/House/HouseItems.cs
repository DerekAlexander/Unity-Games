using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseItems : MonoBehaviour
{


    public bool candle = false;
    public bool stool = false;
    public bool stove = false;
    public bool plantOne = false;
    public bool plantTwo = false;

    ItemInventory inventory;

    private void Awake ( )
    {
        inventory = FindObjectOfType<ItemInventory>();
    }


    public void LoadData ( HouseData items )
    {
        this.candle = items.candle;
        this.stool = items.stool;
        this.stove = items.stove;
        this.plantOne = items.plantOne;
        this.plantTwo = items.plantTwo;
    }



    ///<summary>Returning true means it can and was bought. Names of the items that can be bought, exacly this spelling:
    ///"Candle", "Stool", "Stove", "PlantOne", "PlantTwo"</summary>
    public bool CanBuyItem ( string itemName)
    {

            switch ( itemName )
            {
                case "Candle":
                    if ( !candle ) // if not bought
                    {
                        candle = true; // set to true ( bought )
                    return true; // return true~player has bought it
                }
                break;
            case "Stool":
                if ( !stool )
                {
                    stool = true;
                    return true;
                }
                break;
            case "Stove":
                if ( !stove )
                {
                    stove = true;
                    return true;
                }
                break;
            case "PlantOne":
                if ( !plantOne )
                {
                    plantOne = true;
                    return true;
                }
                break;
            case "PlantTwo":
                if ( !plantTwo )
                {
                    plantTwo = true;
                    return true;
                }
                break;
        }   

        return false; // could not buy requested item
    }






}
