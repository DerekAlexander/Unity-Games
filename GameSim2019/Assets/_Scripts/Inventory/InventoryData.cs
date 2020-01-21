using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData 
{

    public float[] pos = new float[3];
    public float[] rotation = new float[3];

    public int[] id;
    public int[] amount;

    public int numberOfSlots;
    public int currency;

    public string sceneName;
    public string musicState;
    

    public InventoryData ( ItemInventory inventory, string sceneName, string musicState )
    {


        // If the player is in the farm scene save their position and rotation
        // If player is in the house don't save location and load from default position

        pos[0] = inventory.transform.position.x;
        pos[1] = inventory.transform.position.y;
        pos[2] = inventory.transform.position.z;

        rotation[0] = inventory.transform.rotation.eulerAngles.x;
        rotation[1] = inventory.transform.rotation.eulerAngles.y;
        rotation[2] = inventory.transform.rotation.eulerAngles.z;

        numberOfSlots = inventory.slots.Count;
        id = new int[numberOfSlots];
        amount = new int[numberOfSlots];
        this.sceneName = sceneName;
        this.musicState = musicState;

        currency = inventory.currency;

        AssignSlotData(inventory);

    }

    private void AssignSlotData ( ItemInventory inventory )
    {
        for ( int i = 0; i < numberOfSlots - 3; i++ )
        {
            id[i] = inventory.slots[i].id;
            amount[i] = inventory.slots[i].stackSize;
        }
    }


}
