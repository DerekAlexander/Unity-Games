using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Restock : MonoBehaviour {

    public GameObject Content;
    public GameObject scrollView;

    public static Restock instance;

    public List<Item> list = new List<Item>();

    // Use this for initialization
    void Start ()
    {
        instance = this;
        UpdatePanelSlots();
	}


    public void UpdatePanelSlots()
    {
        int index = 0;
        foreach (Transform child in Content.transform)
        {
            //updates slot[index]'s name and icon
            RestockSlotController slot = child.GetComponent<RestockSlotController>();


            if (index < list.Count)
            {
                slot.item = list[index];
            }
            else
            {
                slot.item = null;
            }
            slot.UpdateInfo();
            index++;
        }

    }

}
