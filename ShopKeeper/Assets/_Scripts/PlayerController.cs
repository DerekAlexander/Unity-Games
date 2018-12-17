using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 10f;

    private bool isFacingRight = true;

    private Rigidbody2D myRigidBody2D;
    private Animator myAnimator;
    private PlaceTrigger pt;
    private ReturnTrigger rt;
    public LayerMask layerMask;

    void Awake()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();


        List<int> test = new List<int>();
        test.Add(0);
        test.Add(1);
        test.Add(2);
        test.Insert(0, 3);
        for (int i = 0; i < test.Count; i++)
        {
            print(test[i]);
        }


    }

    void Update()
    {
        Menu();

        float h;
        float v;

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        myRigidBody2D.velocity = new Vector2(h * maxSpeed, v * maxSpeed);

        myAnimator.SetFloat("Speed", Mathf.Abs(h));
        myAnimator.SetFloat("vspeed", Mathf.Abs(v));

        ////Flipping logic
        if (h > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (h < 0 && isFacingRight)
        {
            Flip();
        }


    }

    void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
    }

    void Menu()
    {

        if (Input.GetKeyDown(KeyCode.I))
        {
            GameObject inventoryPanel = Inventory.instance.inventoryPanel;

            if (!inventoryPanel.activeSelf)
            {
                inventoryPanel.SetActive(true);
            }
            else
            {
                inventoryPanel.SetActive(false);
            }

        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GameObject restockPanel = Restock.instance.scrollView;

            if (!restockPanel.activeSelf)
            {
                restockPanel.SetActive(true);
            }
            else
            {
                restockPanel.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.BackQuote))    //for dev use
        {
            CurrencyController.instance.currencyTotal = 1000000;
            CurrencyController.instance.UpdateInfo();
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "SellSpot")
        {
            rt = null;
            pt = other.gameObject.GetComponent<PlaceTrigger>();

        }

        else if (other.gameObject.tag == "ReturnBox")
        {
            pt = null;
            rt = other.gameObject.GetComponent<ReturnTrigger>();
        }

    }

    public PlaceTrigger GetPlaceTrigger()
    {
        return pt;
    }
    public ReturnTrigger GetReturnTrigger()
    {
        return rt;
    }
}
