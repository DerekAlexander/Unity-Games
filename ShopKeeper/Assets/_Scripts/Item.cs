using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Consumable", menuName = "item")]

//scriptableobjects exist outside of the game world.
public class Item : ScriptableObject {

    public string itemName;
    public string description;
    public Sprite icon;
    public Item DefaultItem;
    public int buyValue;
    public int sellValue;
    public void Remove ( )
   {
      CurrencyController.instance.UpdateInfo();
      Inventory.instance.Remove( this );
   }
    public void InventoryAdd (Item item)
    {
        Inventory.instance.Add(item);
    }

   //CurrencyController.instance.currencyTotal = CurrencyController.instance.currencyTotal + value;

   //if an item is used. add value of used item to total of currency, update currency total, then remove item.
}
