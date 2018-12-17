using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcLogic : MonoBehaviour
{
    private GameObject exit;
    public GameObject TalkText;
   public float timerToBuy;
    private GameObject[] gos = new GameObject[6];
    private PlaceTrigger[] pts = new PlaceTrigger[5];



    private string[] npcSayings = new string[8];
    private string[] itemList = new string[13];
    public string desiredItem;  //public so i can debug what they are wanting in editor.

    public bool desireFulfilled = false;
    // Use this for initialization
    void Start()
    {
      timerToBuy = 50;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Npc"), LayerMask.NameToLayer("Npc"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerWall"), LayerMask.NameToLayer("Npc"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Npc"));



        
        exit = GameObject.Find("Exit");

        gos = GameObject.FindGameObjectsWithTag("SellSpot");

        //putting game objects to the script of placetrigger
        for (int i = 0; i <=4; i++)
        {
            pts[i] = gos[i].GetComponent<PlaceTrigger>();

        }
        for (int i = 0; i <= 4; i++)
        {
            pts[i] = gos[i].GetComponent<PlaceTrigger>();

        }

        //list of items.. currently hand written :c
        itemList[0] = "HealthPotion";
        itemList[1] = "ManaPotion";
        itemList[2] = "SpaceBeer";
        itemList[3] = "GemStone";
        itemList[4] = "PosionBottle";
        itemList[5] = "Skull";
        itemList[6] = "Pipe";
        itemList[7] = "StrangeOldHelmet";
        itemList[8] = "AlienEgg";
        itemList[9] = "TreasureMap";
        itemList[10] = "Rock";
        itemList[11] = "PointyWizardHat";
        itemList[12] = "Missle";




        //random range gen to pick a desired item for the npc
        int rand;
        rand = Random.Range(0, itemList.Length);
        desiredItem = itemList[rand];

        //if they dont get their desired item within timeToBuy they are asked to leave kindly


        SayDesiredItem();
        //if somehow they are still around and have not left kill unkindly
        Destroy(gameObject, 120f);

    }

    // Update is called once per frame
    void Update()
    {
      timerToBuy -= Time.deltaTime;
      if (timerToBuy <= 0)
      {
         this.GetComponent<NpcMovementController>().TargetGo( exit.transform );
      }
      else if (desireFulfilled == false)
        {
            FindDesiredItem();
        }
        else
        {
            
            this.GetComponent<NpcMovementController>().TargetGo(exit.transform);

        }


    }
    
    private void SayDesiredItem()
    {

        npcSayings[0] = "hey got any " + desiredItem + "s ?";
        npcSayings[1] = "I want a " + desiredItem + " !!!!!";
        npcSayings[2] = "Gimmie a " + desiredItem;
        npcSayings[3] = "I want a... something.";
        npcSayings[4] = "do you have any " + desiredItem + " ?";
        npcSayings[5] = "just give me a " + desiredItem + " !!!";
        npcSayings[6] = "hmm... nice shop";
        npcSayings[7] = "back in my day we used to get " + desiredItem + " for half that!";


        int num = Random.Range(0, npcSayings.Length );

       TalkText.GetComponent<TextMesh>().text = npcSayings[num];

    }



    
        private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.tag == "PatrolPoint" && GetComponent<NpcMovementController>().hasPatroled == false)
        {
            GetComponent<NpcMovementController>().hasPatroled = true;
            GetComponent<NpcMovementController>().target = GetComponent<NpcMovementController>().originalTarget;
        }

        //BUG:doesnt exist.
        if (other.gameObject.tag == "SellSpot" && other.GetComponent<PlaceTrigger>().currentItem != null)
        {
            if (desiredItem == other.GetComponent<PlaceTrigger>().currentItem.itemName &&
                desireFulfilled == false)
            {

                CurrencyController.instance.currencyTotal = CurrencyController.instance.currencyTotal + other.GetComponent<PlaceTrigger>().currentItem.sellValue;

                CurrencyController.instance.UpdateInfo();
                GetComponent<AudioSource>().Play();

                //BUG: doesnt work but other lines cause issues when npcs run into once bought itemslots. at line 77
                //other.GetComponent<PlaceTrigger>().currentItem = other.GetComponent<PlaceTrigger>().currentItem.DefaultItem;

                //other.GetComponent<SpriteRenderer>().sprite = other.GetComponent<PlaceTrigger>().currentItem.DefaultItem.icon;

                other.GetComponent<PlaceTrigger>().currentItem = null;

                other.GetComponent<SpriteRenderer>().sprite = null;

                desireFulfilled = true;
            }
            else
            {

            }
        }
    }

    private void FindDesiredItem()
    {
        //BUG: if no items place before npcs spawn causes error. no pts found
        for (int i = 0; i <= 4; i++)
        {
            int result;

            if (pts[i].currentItem)
            {
                result = string.Compare(desiredItem, pts[i].currentItem.itemName);

                if (result == 0)
                {
                    this.GetComponent<NpcMovementController>().TargetGo(gos[i].transform);

                }
            }
        }
    }


}
