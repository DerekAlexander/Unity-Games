using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanUp : MonoBehaviour
{
    public GameObject areaOne;
    public GameObject areaTwo;
    public GameObject areaThree;


    public bool isAreaOneClean = false;
    public bool isAreaTwoClean = false;
    public bool isAreaThreeClean = false;


    public bool BuyAreaOne ( int value )
    {
        if ( !isAreaOneClean )
        {
            isAreaOneClean = true;
            areaOne.SetActive(false);
            FindObjectOfType<ItemInventory>().AddCurrency(-value);
            return true;
        }
        return false;
    }


    public bool BuyAreaTwo ( int value )
    {
        if ( !isAreaTwoClean )
        {
            isAreaTwoClean = true;
            areaTwo.SetActive(false);
            FindObjectOfType<ItemInventory>().AddCurrency(-value);
            return true;
        }
        return false;
    }

    public bool BuyAreaThree ( int value )
    {
        if ( !isAreaThreeClean )
        {
            isAreaThreeClean = true;
            areaThree.SetActive(false);
            FindObjectOfType<ItemInventory>().AddCurrency(-value);
            return true;
        }
        return false;
    }






    public void LoadData ( CleanUpData clean )
    {
        this.isAreaOneClean = clean.isAreaOneClean;
        this.isAreaTwoClean = clean.isAreaTwoClean;
        this.isAreaThreeClean = clean.isAreaThreeClean;

        areaOne.SetActive(!isAreaOneClean);
        areaTwo.SetActive(!isAreaTwoClean);
        areaThree.SetActive(!isAreaThreeClean);


    }



}
