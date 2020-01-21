using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseManager : MonoBehaviour
{

    public GameObject candle;
    public GameObject stool;
    public GameObject stove;
    public GameObject plantOne;
    public GameObject plantTwo;


    // Start is called before the first frame update
    void Start ( )
    {
        Utils.CursorState(true);

        FarmData data = FarmSaving.LoadFarm();
        InitScene(data.houseItems);
    }



    private void InitScene ( HouseData data )
    {
        candle.SetActive(data.candle);
        stool.SetActive(data.stool);
        stove.SetActive(data.stove);
        plantOne.SetActive(data.plantOne);
        plantTwo.SetActive(data.plantTwo);
    }

}
