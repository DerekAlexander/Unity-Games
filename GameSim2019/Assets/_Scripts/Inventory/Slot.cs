using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{

    public int id = -1;
    public int stackSize;
    public GameObject player;
    public GameObject description;
    private Item currentItem;

    private void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        UpdateInfo();
    }
    // Use this for initialization
    void Awake ()
    {
  
    }

    //these updates are done only when an item is added.. no need to update all the time in the Update loop.
    public void UpdateInfo ( Item item )
    {
        currentItem = item;
        Text displayText = transform.Find("Text").GetComponent<Text>();
        Text stackText = transform.Find("stackSize").GetComponent<Text>();
        Image displayImage = GetComponent<Image>();

        displayText.text = item.GetName();
        stackText.text = stackSize.ToString();
        displayImage.color = new Color(255, 255, 255, .75f);
        displayImage.sprite = item.icon;

    }

    public void UpdateInfo ()
    {
        currentItem = ItemInventory.instance.ItemIdSearchById(id);

        Text displayText = transform.Find("Text").GetComponent<Text>();
        Text stackText = transform.Find("stackSize").GetComponent<Text>();
        Image displayImage = GetComponent<Image>();
        
        if ( id != -1 && currentItem )
        {
            Debug.Log("id: " + id + "    Stack trace:     " + StackTraceUtility.ExtractStackTrace() + "     " +
                " Do I have a myItem:   " + ( currentItem == true));
            
            displayText.text = currentItem.GetName();
            stackText.text = stackSize.ToString();
            displayImage.color = new Color(255, 255, 255, 1);
            displayImage.sprite = currentItem.icon;
        }
        else
        {
            id = -1;
            displayText.text = "";
            stackText.text = "";
            stackSize = 0;
            displayImage.color = new Color(0, 0, 0, 0);
        }
    }
    public void OnPointerEnter ()
    {
        Debug.Log("mouse enter");
        if ( currentItem )
        {
            description.SetActive(true);
            description.GetComponentInChildren<Text>().text = currentItem.description;
        }
    }

    public void OnPointerExit ()
    {
        Debug.Log("mouse exit");
        description.SetActive(false);
        description.GetComponentInChildren<Text>().text = "";
    }
    

    /// i feel like this would make more sense in itemInventory since its the manager of the slots...
    public void RemoveItem ()
    {
        //if u clicked an empty slot. whats wrong with u? do nothing.
        if ( id == -1 )
        {
            return;
        }



        GameObject pool = GameObject.Find("pool");
        
        //for each child compare the id of the item clicked to the childs id.
        foreach ( Transform child in pool.transform )
        {
            Debug.Log("remove item start");
            if ( GameObject.Find("ShopView") && child.GetComponent<Item>().GetId() == id )
            {
                if ( IsStacked() )  
                {
                    stackSize--;
                }
                else
                {
                    id = -1;
                }
                Debug.Log("in shopview");
                player.GetComponent<ItemInventory>().AddCurrency(child.GetComponent<Item>().buyValue);
                Debug.Log("player currency" + player.GetComponent<ItemInventory>().currency);
                Debug.Log(" fruit worth" + child.GetComponent<Item>().buyValue);
                Debug.Log(" child hello? " + child.GetComponent<Item>());
                Debug.Log(player);
                UpdateInfo();
                return;
            }

            //if the same id move the gameobject in front of the player.
            if ( child.GetComponent<Item>().GetId() == id )
            {
                //move the item from the pool area to the player pos.
                child.transform.position = player.transform.position;

                //rotate the item to match the players forward.
                child.transform.rotation = player.transform.rotation;

                //translate the item forward and up.
                child.transform.Translate(new Vector3(0, 1, 1));

                ///this removes it form the child list... might be better parent it under a new parent.
                child.transform.parent = null;

                //if its stacked remove only one. else empty out the slot.
                if ( IsStacked() )
                {
                    stackSize--;
                }
                else
                {
                    id = -1;
                }

                UpdateInfo();

                return;
            }
        }

    }

    private bool IsStacked ()
    {
        if ( stackSize > 1 )
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
