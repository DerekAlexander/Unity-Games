using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    public bool safeToAdd = false;
    private int emptySlotIndex = -1;
    public GameObject inventoryPanel;
    public static Inventory instance;

    public List<Item> inventory = new List<Item>();

    void Start()
    {
        instance = this;
        UpdatePanelSlots();

    }
    private void Update()
    {
        int index = 0;
        foreach (Transform child in inventoryPanel.transform)
        {
            //updates slot[index]'s name and icon
            InventorySlotController slot = child.GetComponent<InventorySlotController>();
            if (slot.item == null)
            {
                emptySlotIndex = index;
                safeToAdd = true;
            }
            index++;
        }
    }



public void UpdatePanelSlots()
    {
        int index = 0;
        foreach (Transform child in inventoryPanel.transform)
        {
            //updates slot[index]'s name and icon
            InventorySlotController slot = child.GetComponent<InventorySlotController>();


            if (index < inventory.Count)
            {
                slot.item = inventory[index];
            }
            else
            {
                slot.item = null;
            }
            slot.UpdateInfo();
            index++;
        }           

    }

    public void Add(Item item)
    {
        safeToAdd = false;
        int savedIndex = -1;

        int index = 0;
        foreach (Transform child in inventoryPanel.transform)
        {
            //updates slot[index]'s name and icon
            InventorySlotController slot = child.GetComponent<InventorySlotController>();
            if (slot.item == null)
            {
                emptySlotIndex = index;
                safeToAdd = true;
            }
            index++;
        }

        if (inventory.Count == 20 && emptySlotIndex != -1)
        {
            inventory.RemoveAt(emptySlotIndex);
            inventory.Add(item);
            emptySlotIndex = -1;
        }

        else if(inventory.Count < 20)
        {
            inventory.Add(item);
        }
        UpdatePanelSlots();

    }

    public void Remove(Item item)
    {
        inventory.Remove(item);
        UpdatePanelSlots();
    }

}

