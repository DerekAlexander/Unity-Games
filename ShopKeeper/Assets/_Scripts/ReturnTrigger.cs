using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnTrigger : MonoBehaviour {

    public void ReturnItem(Item item)
    {
        if (item)
        {
            CurrencyController.instance.currencyTotal += (int)(item.buyValue * .75);
        }
    }
}
