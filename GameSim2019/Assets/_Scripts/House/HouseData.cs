using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HouseData 
{

    public bool candle;
    public bool stool;
    public bool stove;
    public bool plantOne;
    public bool plantTwo;


    public HouseData ( HouseItems items )
    {
        this.candle = items.candle;
        this.stool = items.stool;
        this.stove = items.stove;
        this.plantOne = items.plantOne;
        this.plantTwo = items.plantTwo;
    }



}
