using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestockSlotController : MonoBehaviour {

    public Item item;
    public GameObject slot;
    public GameObject description;


    // Use this for initialization
    void Start ()
    {
        UpdateInfo();
    }

    public void UpdateInfo()
    {
        Text displayText = transform.Find("Text").GetComponent<Text>();
        Image displayImage = transform.Find("Image").GetComponent<Image>();


        if (item)
        {
            displayText.text = item.itemName + ": " + item.buyValue;
            displayImage.sprite = item.icon;
            displayImage.color = Color.white;
        }
        else
        {
            displayText.text = "";
            displayImage.sprite = null;
            displayImage.color = Color.clear;
        }
    }
    public void OnPointerEnter()
    {
        if (item)
        {
            description.SetActive(true);
            description.GetComponentInChildren<Text>().text = item.description;
        }
    }
    public void OnPointerExit()
    {
        description.SetActive(false);
        description.GetComponentInChildren<Text>().text = "";
    }

    public void Clicked()
    {
        if (item && item.buyValue <= CurrencyController.instance.currencyTotal && Inventory.instance.safeToAdd == true)
        {
            GetComponentInParent<AudioSource>().Play();

            CurrencyController.instance.currencyTotal = CurrencyController.instance.currencyTotal - item.buyValue;
            CurrencyController.instance.UpdateInfo();

            Inventory.instance.Add(item);
            Inventory.instance.UpdatePanelSlots();

        }
    }
}
