        using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ItemInventory : MonoBehaviour
{
    //list of all the ui inventory buttons.
    private List<Button> buttons = new List<Button>();

    //slots list built for convience.
    [SerializeField] public List<Slot> slots = new List<Slot>();

    private GameObject inventoryPanel;
    public static ItemInventory instance;
    private bool itemPickedUp;
    public int maxStackSize;
    public int currency;
    public Text currencyText;
    

    ///I would like to build this list based on tags or something instead of inspector drag ins....
    [Tooltip("list of all item prefabs in the game. For now works by inspector drag in...")]
    [SerializeField] GameObject[] itemPrefabs;


    void Awake()
    {
        instance = this;
        inventoryPanel = GameObject.Find("InventoryPanel");
        maxStackSize = 20;
        itemPickedUp = false;


        //builds a list of all the inventory buttons under inventoryPanel. dynamic list.
        foreach ( Transform child in inventoryPanel.transform )
        {
            buttons.Add(child.GetComponent<Button>());
            slots.Add(child.GetComponent<Slot>());
        }

        // Loads the players inventory from the save file
        // If no save file exists the game will treat this as a new game and will start up fresh(no data)
        LoadInventory();


        //find all the items in player inventory when loading in then hides the gameobjects of them in the secret room.
        InstatiateToPool();

        foreach ( Transform child in inventoryPanel.transform )
        {
            if ( !child.GetComponent<Slot>() )
                return;

            child.GetComponent<Slot>().UpdateInfo();
        }

    }

    private void Start ()
    {
        AddCurrency(0);//DA this is to call the update text at start;
        //this is here instead at the end of start because currency start is called after inventory. 
        inventoryPanel.SetActive(false);
    }


    private void Update ()
    {

    }



    //this function is for when the player loads into the farm all his current items load into secret room. so no instansiating during play. 
    private void InstatiateToPool ()
    {
        GameObject pool = GameObject.Find("pool");
        if ( pool == null )
        {
            Debug.LogError("there is no 'pool' object please add one in. >:(");
        }

        foreach ( Transform child in inventoryPanel.transform )
        {
            if(!child.GetComponent<Slot>())
                return;
            

            int slotId = child.GetComponent<Slot>().id;

            //if its not an empty slot
            if ( slotId != -1 )
            {
                //loop through the prefabs list... ///IF IT CRASHES HERE CHECK THE ITEM PREFABS ARRAY INSPECTOR///
                for ( int i = 0; i < itemPrefabs.Length; i++ )
                {
                    //compare this item prefabs id to the current slots id.
                    if ( itemPrefabs[i].GetComponent<Item>().GetId() == slotId )
                    {
                        //loop through the stack size spawning however many there is. 
                        for ( int numStack = 0; numStack < child.GetComponent<Slot>().stackSize; numStack++ )
                        {
                            Instantiate(itemPrefabs[i], pool.transform.position, pool.transform.rotation, pool.transform);
                        }

                    }
                }
            }
        }
    }

    public void AddCurrency(int currencyToBeAdded)
    {
        currency += currencyToBeAdded;
        currencyText.text = "Currency: " +  currency.ToString();
    }

    //add item that loops through first to test for stacking then for first empty slot to add too.
    public bool addItem ( Item item )
    {
        
        //this loop first test if the input item already exist if so stack it.
        for ( int i = 0; i < slots.Count -3; i++ )
        {
            //if we a lready have one go to stack it
            if ( slots[i].id == item.GetId() )
            {
                //if we are max stack sized create a new stack
                if ( slots[i].stackSize < maxStackSize )
                {
                    //if we get in here we done so get out.
                    slots[i].stackSize++;
                    slots[i].UpdateInfo(item);
                    itemPickedUp = true;
                    return true;
                }

            }


        }

        //if we get to this loop this looks for the next empty slot to add in.
        for ( int i = 0; i < slots.Count -3; i++ )
        {
            if ( slots[i].id == -1 )
            {
                //if we get in here we done so get out.
                slots[i].id = item.GetId();
                slots[i].stackSize++;
                slots[i].UpdateInfo(item);
                itemPickedUp = true;
                return true;
            }
        }

        return false;
    }

    //get an item back by seraching its id. just useful.
    public Item ItemIdSearchById ( int id )
    {
        for ( int i = 0; i < itemPrefabs.Length; i++ )
        {
            if ( itemPrefabs[i].GetComponent<Item>().GetId() == id )
            {
                return itemPrefabs[i].GetComponent<Item>();
            }
        }
        return null;
    }

    public bool ItemPickedUp ()
    {
        return itemPickedUp;
    }

    public void ItemPickedUp ( bool b )
    {
        itemPickedUp = b;
    }






    public void LoadInventory ()
    {
        InventoryData data = InventorySaving.LoadInventory();

        if ( data == null ) // if no save file exists
            return;

        //bool didPlace = FindObjectOfType<SceneController>().PlacePlayer(gameObject);
        //bool didPlace = false;

        // if in farm scene load position and rotation
        // else load from default position of scene
        if ( SceneController.ActiveSceneName() == "TestLayout" )
        {
            transform.position = new Vector3(data.pos[0], data.pos[1], data.pos[2]);
            transform.rotation = Quaternion.Euler(new Vector3(data.rotation[0], data.rotation[1], data.rotation[2]));
        }

        currency = data.currency;

        for ( int i = 0; i < slots.Count -3; i++ )
        {
            slots[i].id = data.id[i];
            slots[i].stackSize = data.amount[i];
        }

    }


    public void SaveInventory ( )
    {
        string scene = SceneManager.GetActiveScene().name;
        string musicState = FindObjectOfType<MusicController>().MusicState();
        InventorySaving.SaveInventory(this, scene, musicState);
    }

}
