using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{


    [SerializeField] int itemId;
    [Tooltip("item name! have fun with it.")]
    [SerializeField] string myName;
    [Tooltip("this icon is displayed in the ui inventory.")]
    public Sprite icon;
    public int buyValue;
    public string description;

    // inventory ->   id items
    //                 ^       ^
    //                food    hats

    //items have an id. this id is used to associate its values, inventory icons, and gameobject appearance. 
    //when picking up an item. only the item id is passed to the inventory. at which it finds the associated icon for that id then puts it in the first empty slot.
    //when clicking an item in inventory it should look at the item id find the item in the world then instaniate a copy of that item at the players pos. 
    // this way would require an area in the level with every item active. 
    // at start of game all items would need to be found and get the id associated with it and save it. then we could instatisate its gameobject by using the item id list we build.


    // Use this for initialization
    void Start ()
    {


    }

    // Update is called once per frame
    void Update ()
    {

    }

    public virtual void UseItem ( GameObject obj)
    {
        //intended to return the inheriting classes useItem(). often this 0 wont be used.
        //return 0;
    }

    public int GetId ()
    {
        return itemId;
    }

    public string GetName()
    {
        if ( myName == "" )
        {
            Debug.LogError(this.name + " name is empty! FIX IT!");
        }
        return myName;
    }
}
