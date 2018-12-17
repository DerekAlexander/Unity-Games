using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotController : MonoBehaviour  {

   public Item item;
   public GameObject slot;
   public GameObject description;

   private GameObject player;
   private PlayerController playerScript;
    private PlaceTrigger pt;
    private ReturnTrigger rt;

    public void Start ( )
   {
      UpdateInfo();
      player = GameObject.Find( "Player" );
      playerScript = player.GetComponent<PlayerController>();

   }

   public void UpdateInfo()
    {
        Text displayText = transform.Find("Text").GetComponent<Text>();
        Image displayImage = transform.Find("Image").GetComponent<Image>();

        //if there is an item in the slot update its info
        if (item)
        {
            displayText.text = item.itemName;
            displayImage.sprite = item.icon;
            displayImage.color = Color.white;
        }

        //if no item than clear out the slot
        else
        {
            displayText.text = "";
            displayImage.sprite = null;
            displayImage.color = Color.clear;
        }
    }

    public void Use ( )
   {
        pt = playerScript.GetPlaceTrigger();
        rt = playerScript.GetReturnTrigger();


        //if we clicked an item and in a trigger box
        if (item && pt)
        {
            //if its not the same item
            if (!pt.currentItem)
            {
                //place the item in the trigger box. and remove from our inventory.
                pt.PlaceItem(item);
                item.Remove();
            }

            pt = null;


        }
        //if we have an item and we are in the return trigger box.
        if(item && rt)
        {
            rt.ReturnItem(item);
            item.Remove();
            rt = null;
        }
   }
    public void OnPointerEnter()
    {
        //if there is an item show its description.
        if (item)
        {
            description.SetActive(true);
            description.GetComponentInChildren<Text>().text = item.description;
        }
    }
    public void OnPointerExit()
    {
        description.GetComponentInChildren<Text>().text = "";
        description.SetActive(false);
    }

}
