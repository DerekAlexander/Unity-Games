using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestockSlotController : MonoBehaviour
{

    [Tooltip("for non Item classed buyable things. If it has an item component leave empty.")]
    public GameObject gameObjectItem;
    public int gameobjectBuyValue = 0;
    public Sprite gameObjectImage;
    public string HouseItemName;

    public GameObject EggCongrats;
    public Image EggImage;

    public Item item;
    public GameObject slot;
    public GameObject description;
    private GameObject pool;


    // Use this for initialization
    void Start ()
    {
        pool = GameObject.Find("pool");
        UpdateInfo();


    }

    public void UpdateInfo ()
    {
        Text displayText = transform.Find("Text").GetComponent<Text>();
        Image displayImage = GetComponentInChildren<Image>();


        if ( item )
        {
            Debug.Log("in item " + item.GetName() + " actual text " + displayText.text);
            displayText.text = item.GetName() + ": " + item.buyValue;
            //displayImage.sprite = item.icon;
            //displayImage.color = Color.clear;
        }
        else if ( gameObjectItem )
        {
            Debug.Log("in go " + gameObjectItem.name + " actual text " + displayText.text);
            displayText.text = gameObjectItem.name + ": " + gameobjectBuyValue;
            //displayImage.sprite = gameObjectImage;
            //displayImage.color = Color.clear;

        }
        else if(HouseItemName != "")
        {
            Debug.Log("in house" + HouseItemName + " actual text " + displayText.text);
            displayText.text = HouseItemName + ": " + gameobjectBuyValue;
            //displayImage.sprite = gameObjectImage;
            //displayImage.color = Color.clear;
        }
        else
        {
            displayText.text = "";
            displayImage.sprite = null;
            displayImage.color = Color.clear;
        }
    }
    public void OnPointerEnter ()
    {
        if ( item )
        {
            description.SetActive(true);
            description.GetComponentInChildren<Text>().text = item.description;
        }
    }
    public void OnPointerExit ()
    {
        description.SetActive(false);
        description.GetComponentInChildren<Text>().text = "";
    }

    public void Clicked ()
    {
        if ( item && item.buyValue <= ItemInventory.instance.currency && item.tag == "Food" )
        {
            GetComponentInParent<AudioSource>().Play();
            if ( ItemInventory.instance.addItem(item) )
            {
                ItemInventory.instance.AddCurrency(item.buyValue * -1);
                Instantiate(item, pool.transform);
                item.transform.position = pool.transform.position;
                ItemInventory.instance.ItemPickedUp(false);
            }
            else
            {
                //tell player no space.
            }


        }
        if ( item && item.buyValue <= ItemInventory.instance.currency && item.tag == "Egg" )
        {
            if ( FindObjectOfType<BabyMaker>().FindSpawnPoint() != -1 )
            {
                GetComponentInParent<AudioSource>().Play();
                ItemInventory.instance.AddCurrency(item.buyValue * -1);
                item.GetComponent<EggItem>().UseItem(item.gameObject);
                EggImage.sprite = item.icon;
                EggCongrats.SetActive(true);
                StartCoroutine(CloseUiAfterSeconds());
            }
        }

        if ( HouseItemName != "" && gameobjectBuyValue != 0 && gameobjectBuyValue <= ItemInventory.instance.currency )
        {
            if( GameObject.Find("Canvas").GetComponent<HouseItems>().CanBuyItem(HouseItemName) )
            {
                GetComponentInParent<AudioSource>().Play();
                ItemInventory.instance.AddCurrency(gameobjectBuyValue * -1);
            }
        }
    }

    IEnumerator CloseUiAfterSeconds()
    {
        yield return new WaitForSeconds(5f);
        EggCongrats.SetActive(false);


    }

}

