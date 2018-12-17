using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlaceTrigger : MonoBehaviour  {

    public Item currentItem;
    public Item backUpItem;

   public void PlaceItem( Item item)
   {
        if(currentItem)
        {
            backUpItem = currentItem;
        }
        currentItem = item;
        this.GetComponent<SpriteRenderer>().sprite = item.icon;
    }

}
